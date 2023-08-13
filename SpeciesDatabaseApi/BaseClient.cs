using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

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
            return Regex.Replace(domain, @"\/\/(.*api([a-zA-Z0-9_-]+)?|www)[.]", "//");
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
    /// Encodes the data query to be safe in the url
    /// </summary>
    /// <param name="query">Query string to encode</param>
    /// <returns>Encoded query string</returns>
    public static string UrlEncode(string query)
    {
        return HttpUtility.UrlEncode(query);
    }

    /// <summary>
    /// Gets the raw absolute url for an request to the Api without any GET parameter
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public string GetRawRequestUrl(string path) => $"{ApiAddress}/{path.TrimStart(' ', '/').TrimEnd()}";

    public string GetRequestUrl(string path)
    {
        var url = GetRawRequestUrl(path);
        if (ApiToken is { CanUse: true, Placement: ApiTokenPlacement.Get })
        {
            return $"{url}?{UrlEncode(ApiToken.Key)}={UrlEncode(ApiToken.Value)}";
        }

        return url;
    }

    /// <summary>
    /// Gets the absolute url for an request to the Api
    /// </summary>
    /// <param name="path">Partial path without Api address</param>
    /// <param name="parameters">Parameters to send with the request</param>
    /// <returns>Absolute path for the request</returns>
    public string GetRequestUrl(string path, IEnumerable<KeyValuePair<string, object?>> parameters)
    {
        return $"{GetRawRequestUrl(path)}{GetUrlParametersString(parameters)}";
    }

    /// <summary>
    /// Gets the absolute url for an request to the Api
    /// </summary>
    /// <param name="path">Partial path without Api address</param>
    /// <param name="parameters">Parameters to send with the request</param>
    /// <returns>Absolute path for the request</returns>
    public string GetRequestUrl(string path, IReadOnlyDictionary<string, object?> parameters) => GetRequestUrl(path, parameters.AsEnumerable());

    /// <summary>
    /// Gets the absolute url for an request to the Api
    /// </summary>
    /// <param name="path">Partial path without Api address</param>
    /// <param name="parameter">Parameter to send with the request</param>
    /// <returns>Absolute path for the request</returns>
    public string GetRequestUrl(string path, KeyValuePair<string, object?> parameter) => GetRequestUrl(path, new[] { parameter });

    /// <summary>
    /// Gets the absolute url for an request to the Api
    /// </summary>
    /// <param name="path">Partial path without Api address</param>
    /// <param name="classObj">Class to parse the parameters from</param>
    /// <returns>Absolute path for the request</returns>
    public string GetRequestUrl(string path, object classObj)
    {
        return $"{GetRawRequestUrl(path)}{GetUrlParametersString(classObj)}";
    }

    /// <summary>
    /// Gets the parameters string from a dictionary of key and values parameters
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns>The formatted string</returns>
    public string GetUrlParametersString(IEnumerable<KeyValuePair<string, object?>> parameters)
    {
        var builder = new StringBuilder();

        // Sort the parameters alphabetically to avoid http redirection.
        foreach (var item in parameters.OrderBy(x => x.Key))
        {
            string? value;
            switch (item.Value)
            {
                case null:
                    continue;
                case IList list:
                    if (list.Count == 0) continue;

                    var items = new List<string>(list.Count);
                    foreach (var obj in list)
                    {
                        if (obj is null) continue;
                        value = obj.ToString()?.Trim().ToLowerInvariant();
                        if (string.IsNullOrWhiteSpace(value)) continue;
                        items.Add(value);
                    }
                    value = string.Join(',', items);
                    break;
                case string str:
                    value = str.Trim().ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(value)) continue;
                    break;
                default:
                    value = item.Value.ToString()?.Trim().ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(value)) continue;
                    break;
            }

            builder.Append($"{(builder.Length == 0 ? "?" : "&")}{UrlEncode(item.Key.ToLowerInvariant())}={UrlEncode(value)}");
        }

        if (ApiToken is { CanUse: true, Placement: ApiTokenPlacement.Get })
        {
            builder.Append($"{(builder.Length == 0 ? "?" : "&")}{UrlEncode(ApiToken.Key)}={UrlEncode(ApiToken.Value)}");
        }

        return builder.ToString();
    }

    /// <summary>
    /// Gets the parameters string from a dictionary of key and values parameters
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns>The formatted string</returns>
    public string GetUrlParametersString(IReadOnlyDictionary<string, object?> parameters) => GetUrlParametersString(parameters.AsEnumerable());

    /// <summary>
    /// Gets the parameters string from a dictionary of key and values parameters
    /// </summary>
    /// <param name="keyValue"></param>
    /// <returns>The formatted string</returns>
    public string GetUrlParametersString(KeyValuePair<string, object?> keyValue) => GetUrlParametersString(new[]{ keyValue });

    /// <summary>
    /// Gets the parameters string from a class (Reflection)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>The formatted string</returns>
    public string GetUrlParametersString(object? obj)
    {
        var dict = new Dictionary<string, object?>();

        if (obj is not null)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var propertyInfo in properties)
            {
                var method = propertyInfo.GetMethod;
                if (method is null) continue;
                var value = propertyInfo.GetValue(obj);

                var attr = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
                dict.Add(attr?.Name ?? method.Name, value);
            }
        }

        return GetUrlParametersString(dict);
    }

    protected HttpRequestMessage PrepareJsonHttpRequestMessage(string requestUrl, HttpMethod httpMethod)
    {
        var request = new HttpRequestMessage(httpMethod, requestUrl);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        if (ProductInfoHeader is not null)
        {
            request.Headers.UserAgent.Add(ProductInfoHeader);
        }
         
        if (ApiToken.CanUse)
        {
            switch (ApiToken.Placement)
            {
                case ApiTokenPlacement.Header:
                    request.Headers.Add(ApiToken.Key, ApiToken.Value);
                    break;
                case ApiTokenPlacement.HeaderAuthorization:
                    request.Headers.Authorization = new AuthenticationHeaderValue(ApiToken.Key, ApiToken.Value);
                    break;
            }
        }

        return request;
    }
    public Task<T?> PostJsonAsync<T>(string requestUrl, object postData, IEnumerable<KeyValuePair<string, object?>> urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> PostJsonAsync<T>(string requestUrl, object postData, IReadOnlyDictionary<string, object?> urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> PostJsonAsync<T>(string requestUrl, object postData, KeyValuePair<string, object?> urlParameter, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameter), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> PostJsonAsync<T>(string requestUrl, object postData, object classUrlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, classUrlParameters), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> PostJsonAsync<T>(string requestUrl, object postData, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Post);

        var requestJson = JsonSerializer.Serialize(postData);
        request.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> GetJsonAsync<T>(string requestUrl, IEnumerable<KeyValuePair<string, object?>> urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Get);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> GetJsonAsync<T>(string requestUrl, IReadOnlyDictionary<string, object?> urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Get);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> GetJsonAsync<T>(string requestUrl, KeyValuePair<string, object?> urlParameter, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameter), HttpMethod.Get);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> GetJsonAsync<T>(string requestUrl, object classUrlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, classUrlParameters), HttpMethod.Get);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> GetJsonAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Get);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> DeleteJsonAsync<T>(string requestUrl, IEnumerable<KeyValuePair<string, object?>> urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Delete);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> DeleteJsonAsync<T>(string requestUrl, IReadOnlyDictionary<string, object?> urlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameters), HttpMethod.Delete);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> DeleteJsonAsync<T>(string requestUrl, KeyValuePair<string, object?> urlParameter, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, urlParameter), HttpMethod.Delete);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> DeleteJsonAsync<T>(string requestUrl, object classUrlParameters, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl, classUrlParameters), HttpMethod.Delete);
        return SendRequestAsync<T>(request, cancellationToken);
    }

    public Task<T?> DeleteJsonAsync<T>(string requestUrl, CancellationToken cancellationToken = default)
    {
        using var request = PrepareJsonHttpRequestMessage(GetRequestUrl(requestUrl), HttpMethod.Delete);
        return SendRequestAsync<T>(request, cancellationToken);
    }

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
    /// Sends a request to the Api
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="HttpResponseMessage"></exception>
    private async Task<T?> SendRequestAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        await OnBeforeSendRequestAsync(request, cancellationToken).ConfigureAwait(false);

        Debug.WriteLine($"{ClientAcronym}: Sending request {request.RequestUri}");

        if (_autoWaitForRequestLimit && RequestsHitLimit)
        {
            do
            {
                var waitTime = RandomNumberGenerator.GetInt32(500, 2000);
                Debug.WriteLine($"{ClientAcronym}: Api limit hit, waiting {waitTime}ms before re-try.");
                await Task.Delay(waitTime, cancellationToken).ConfigureAwait(false);
            } while (RequestsHitLimit);
        }

        using var response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

        Interlocked.Increment(ref _totalRequests);

        if (ThrowExceptionIfRequestStatusCodeFails) response.EnsureSuccessStatusCode();
        else if (!response.IsSuccessStatusCode) return default;

        switch (response.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return default;
        }

        if (_autoWaitForRequestLimit && _maximumRequestsPerSecond > 0)
        {
            Interlocked.Increment(ref _requestsInCurrentSecond);
            _resetRequestsTimer.Start();
        }

#if DEBUG
        var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        Debug.WriteLine(json);
        return JsonSerializer.Deserialize<T>(json, DefaultJsonSerializerOptions);
#else
        return await response.Content.ReadFromJsonAsync<T>(DefaultJsonSerializerOptions, cancellationToken).ConfigureAwait(false);
#endif
    }

#endregion
}