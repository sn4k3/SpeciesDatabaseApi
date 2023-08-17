using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi;

public abstract class BaseClient : BindableBase, IDisposable
{
    #region Constants

    private static readonly Lazy<HttpClient> HttpClientShared = new(() =>
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", AboutLibrary.SoftwareWithVersion);
        return httpClient;
    });

    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new(JsonSerializerDefaults.Web)
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.AllowNamedFloatingPointLiterals,
        Converters = { new JsonStringEnumConverter() }
    };

    #endregion

    #region Members
    /// <summary>
    /// Indicates whatever the class has been disposed or not
    /// </summary>
    private bool _disposed;

    /// <summary>
    /// Gets an mutex object usable for lock operations shared across only this object
    /// </summary>
    public readonly object Mutex = new();

    /// <summary>
    /// The timer to reset request limits
    /// </summary>
    private readonly System.Timers.Timer _resetRequestsTimer = new(1000)
    {
        AutoReset = false,
    };

    protected readonly HttpClient _httpClient;
    private ulong _totalRequests;
    private int _maximumRequestsPerSecond = -1;
    private int _requestsInCurrentSecond;
    private bool _autoWaitForRequestLimit;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the version of the used Api
    /// </summary>
    public abstract decimal Version { get; }

    /// <summary>
    /// Gets or sets the Api address for the calls.
    /// </summary>
    public Uri ApiAddress { get; set; }

    /// <summary>
    /// Gets or sets the Api key and value for the calls<br/>
    /// The key represents the name of the token to use<br/>
    /// The value represents the value of the token to use
    /// </summary>
    public ApiToken ApiToken { get; } = new();

    /// <summary>
    /// Gets or sets the auth token required to make requests
    /// </summary>
    public ApiToken AuthToken { get; } = new();

    /// <summary>
    /// Gets this client name/provider acronym
    /// </summary>
    public virtual string ClientAcronym => GetType().Name[..^6];

    /// <summary>
    /// Gets this client full name/provider
    /// </summary>
    public abstract string ClientFullName { get; }


    /// <summary>
    /// Gets the main website url for this client
    /// </summary>
    public virtual string WebsiteUrl
    {
        get
        {
            var domain = ApiAddress.GetLeftPart(UriPartial.Authority);
            return Regex.Replace(domain, @"\/\/(.*api([a-zA-Z0-9_-]+)?|www)[.]", "//www.");
        }
    }

    /// <summary>
    /// Gets the total number of requests made with this client
    /// </summary>
    public ulong TotalRequests => _totalRequests;

    /// <summary>
    /// Gets or sets the maximum number of requests per second the client can handle before error.<br/>
    ///  &lt;= 0 = Unlimited
    /// </summary>
    public int MaximumRequestsPerSecond
    {
        get => _maximumRequestsPerSecond;
        set => SetAndRaisePropertyIfChanged(ref _maximumRequestsPerSecond, value);
    }

    /// <summary>
    /// Gets the requests made in the current second.<br/>
    /// It resets every second
    /// </summary>
    public int RequestsInCurrentSecond => _requestsInCurrentSecond;

    /// <summary>
    /// True if the requests hit the limit in the actual second
    /// </summary>
    public bool RequestsHitLimit => _maximumRequestsPerSecond > 0 && _requestsInCurrentSecond >= _maximumRequestsPerSecond;

    /// <summary>
    /// Gets or sets if the request should be delayed if limits are hit.
    /// <remarks>There's no guarantee that the request will be made inside limits.</remarks>
    /// </summary>
    public bool AutoWaitForRequestLimit
    {
        get => _autoWaitForRequestLimit;
        set
        {
            _autoWaitForRequestLimit = value;
            _requestsInCurrentSecond = 0;
        }
    }

    /// <summary>
    /// Gets or sets the product info header to send with the requests
    /// </summary>
    public ProductInfoHeaderValue? ProductInfoHeader { get; set; }

    /// <summary>
    /// Gets or sets if it should throw an exception when the request code is other than success.<br/>
    /// If false it will return a null object
    /// </summary>
    public bool ThrowExceptionIfRequestStatusCodeFails { get; set; } = true;

    #endregion

    #region Constructors

    protected BaseClient(Uri apiAddress, HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? HttpClientShared.Value;
        ApiAddress = apiAddress;
        _resetRequestsTimer.Elapsed += ResetRequestsTimerOnElapsed;
    }

    private void ResetRequestsTimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        Interlocked.Exchange(ref _requestsInCurrentSecond, 0);
    }

    #endregion

    #region Overrides
    public override string ToString()
    {
        return $"{nameof(Version)}: {Version}, {nameof(ApiAddress)}: {ApiAddress}, {nameof(ApiToken)}: {ApiToken}, {nameof(AuthToken)}: {AuthToken}, {nameof(ClientAcronym)}: {ClientAcronym}, {nameof(ClientFullName)}: {ClientFullName}, {nameof(WebsiteUrl)}: {WebsiteUrl}, {nameof(TotalRequests)}: {TotalRequests}, {nameof(MaximumRequestsPerSecond)}: {MaximumRequestsPerSecond}, {nameof(RequestsInCurrentSecond)}: {RequestsInCurrentSecond}, {nameof(RequestsHitLimit)}: {RequestsHitLimit}, {nameof(AutoWaitForRequestLimit)}: {AutoWaitForRequestLimit}, {nameof(ProductInfoHeader)}: {ProductInfoHeader}";
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _resetRequestsTimer.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Methods

    #region Query parameters
    /// <summary>
    /// Escapes the data query to be safe in the url
    /// </summary>
    /// <param name="query">Query string to escape</param>
    /// <returns>Escaped query string</returns>
    public static string EscapeDataString(string query)
    {
        return Uri.EscapeDataString(query);
    }

    /// <summary>
    /// Gets the raw absolute url for an request to the Api without any GET parameter
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string GetRawRequestUrl(string path) => $"{ApiAddress}/{path.TrimStart(' ', '/').TrimEnd()}";

    /// <summary>
    /// Gets the absolute url for an request to the Api
    /// </summary>
    /// <param name="path">Partial path without Api address</param>
    /// <param name="parameters">Parameters to send with the request</param>
    /// <returns>Absolute path for the request</returns>
    public string GetRequestUrl(string path, QueryParameters? parameters = null)
    {
        var url = GetRawRequestUrl(path);

        if (parameters is null)
        {
            if (ApiToken is { CanUse: true, Placement: ApiTokenPlacement.Get })
            {
                return $"{url}?{EscapeDataString(ApiToken.Key)}={EscapeDataString(ApiToken.Value)}";
            }

            return url;
        }

        return $"{url}{parameters.ToString(this)}";
    }

    protected HttpRequestMessage CreateHttpRequestMessage(string requestUrl, HttpMethod httpMethod, RequestContentType contentType = RequestContentType.Json)
    {
        var request = new HttpRequestMessage(httpMethod, requestUrl);
        
        switch (contentType)
        {
            case RequestContentType.Raw:
                break;
            case RequestContentType.Json:
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                break;
            case RequestContentType.Xml:
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
        }
            
        if (ProductInfoHeader is not null)
        {
            request.Headers.UserAgent.Add(ProductInfoHeader);
        }

        ApiToken.TryInject(request);
        AuthToken.TryInject(request);

        return request;
    }

    #endregion

    #region Json Requests
    public Task<T?> PostJsonAsync<T>(string requestUrl, object postData, QueryParameters urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, cancellationToken);
    }

    
    public Task<T?> PostJsonAsync<T>(string requestUrl, object postData, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> GetJsonAsync<T>(string requestUrl, QueryParameters urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Get);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> GetJsonAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Get);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> DeleteJsonAsync<T>(string requestUrl, QueryParameters urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Delete);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> DeleteJsonAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Delete);
        return SendRequestAsync<T>(request, cancellationToken);
    }
    #endregion

    #region Xml Requests
    public Task<T?> PostXmlAsync<T>(string requestUrl, object postData, QueryParameters urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, RequestContentType.Xml, cancellationToken);
    }

    public Task<T?> PostXmlAsync<T>(string requestUrl, object postData, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, RequestContentType.Xml, cancellationToken);
    }

    public Task<T?> GetXmlAsync<T>(string requestUrl, QueryParameters urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Get);
        return SendRequestAsync<T>(request, RequestContentType.Xml, cancellationToken);
    }

    public Task<T?> GetXmlAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Get);
        return SendRequestAsync<T>(request, RequestContentType.Xml, cancellationToken);
    }

    public Task<T?> DeleteXmlAsync<T>(string requestUrl, QueryParameters urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Delete);
        return SendRequestAsync<T>(request, RequestContentType.Xml, cancellationToken);
    }

    public Task<T?> DeleteXmlAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Delete);
        return SendRequestAsync<T>(request, RequestContentType.Xml, cancellationToken);
    }
    #endregion

    #region Download Requests

    public Task DownloadAsync(string requestUrl, QueryParameters urlParameters, Stream stream, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Get, RequestContentType.Raw);
        return SendDownloadRequestAsync(request, stream, cancellationToken);
    }

    public Task DownloadAsync(string requestUrl, Stream stream, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Get, RequestContentType.Raw);
        return SendDownloadRequestAsync(request, stream, cancellationToken);
    }

    #endregion

    #region  Get Response
    public Task<HttpResponseMessage> GetResponseAsync(string requestUrl, QueryParameters urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Get, RequestContentType.Raw);
        return SendRequestAsync(request, cancellationToken);
    }

    public Task<HttpResponseMessage> GetResponseAsync(string requestUrl, CancellationToken cancellationToken = default)
    {
        using var request = CreateHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Get, RequestContentType.Raw);
        return SendRequestAsync(request, cancellationToken);
    }
    #endregion

    #region Send Request
    /// <summary>
    /// Trigger before SendRequestAsync is executed.
    /// </summary>
    /// <remarks>Use this method to prepare the message request with any key/token</remarks>
    /// <param name="request">The formatted request</param>
    /// <param name="cancellationToken"></param>
    protected virtual Task OnBeforeSendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Sends a request to the Api and return the <see cref="HttpResponseMessage"/>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpResponseMessage"></exception>
    /// <remarks>Don't forget to dispose the <see cref="HttpResponseMessage"/></remarks>
    private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        await OnBeforeSendRequestAsync(request, cancellationToken).ConfigureAwait(false);

#if DEBUG
        Debug.WriteLine($"{ClientAcronym}: Sending request {request.RequestUri}");
        Console.WriteLine($"{ClientAcronym}: Sending request {request.RequestUri}");
#endif

        if (_autoWaitForRequestLimit && RequestsHitLimit)
        {
            do
            {
                var waitTime = RandomNumberGenerator.GetInt32(500, 2000);
#if DEBUG
                Debug.WriteLine($"{ClientAcronym}: Api limit hit, waiting {waitTime}ms before re-try.");
                Console.WriteLine($"{ClientAcronym}: Api limit hit, waiting {waitTime}ms before re-try.");
#endif
                await Task.Delay(waitTime, cancellationToken).ConfigureAwait(false);
            } while (RequestsHitLimit);
        }

        var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        Interlocked.Increment(ref _totalRequests);

        if (_autoWaitForRequestLimit && _maximumRequestsPerSecond > 0)
        {
            Interlocked.Increment(ref _requestsInCurrentSecond);
            _resetRequestsTimer.Start();
        }

        if (ThrowExceptionIfRequestStatusCodeFails) response.EnsureSuccessStatusCode();

        return response;
    }

    /// <summary>
    /// Sends a request to the Api and return a data model from the <see cref="contentType"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="contentType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpResponseMessage"></exception>
    private async Task<T?> SendRequestAsync<T>(HttpRequestMessage request, RequestContentType contentType = RequestContentType.Json, CancellationToken cancellationToken = default)
    {
        using var response = await SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
        if (!ThrowExceptionIfRequestStatusCodeFails && !response.IsSuccessStatusCode) return default;

        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return default;
        }

        switch (contentType)
        {
            case RequestContentType.Json:
#if DEBUG
                var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                Debug.WriteLine(json);
                Console.WriteLine(json);
                return JsonSerializer.Deserialize<T>(json, DefaultJsonSerializerOptions);
#endif
#pragma warning disable CS0162 // Unreachable code detected
                return await response.Content.ReadFromJsonAsync<T>(DefaultJsonSerializerOptions, cancellationToken).ConfigureAwait(false);
#pragma warning restore CS0162 // Unreachable code detected
            case RequestContentType.Xml:
                var xmlStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                var serializer = new XmlSerializer(typeof(T));
                var obj = serializer.Deserialize(xmlStream);
                if (obj is null) return default;
                return (T)obj;
            default:
                throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
        }
    }

    /// <summary>
    /// Sends a request to the Api and return a data model from json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpResponseMessage"></exception>
    private Task<T?> SendRequestAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
        => SendRequestAsync<T>(request, RequestContentType.Json, cancellationToken);


    /// <summary>
    /// Sends a download request to the Api and copy the content to a <see cref="Stream"/>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="stream">The stream to copy the content to</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpResponseMessage"></exception>
    private async Task<Task> SendDownloadRequestAsync(HttpRequestMessage request, Stream stream, CancellationToken cancellationToken = default)
    {
        using var response = await SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
        if (!ThrowExceptionIfRequestStatusCodeFails && !response.IsSuccessStatusCode) return Task.FromException(new HttpRequestException("Request return not success status code", null, response.StatusCode));

        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return Task.FromException(new HttpRequestException("Request return no content", null, response.StatusCode));
        }

        return response.Content.CopyToAsync(stream, cancellationToken);
    }

    #endregion

    #endregion
}