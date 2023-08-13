using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace SpeciesDatabaseApi.MarineRegions;

/// <summary>
/// The client for https://marineregions.org API
/// </summary>
public class MarineRegionsClient : BaseClient
{
    #region Static objects
    /// <summary>
    /// The client full name/provider
    /// </summary>
    public const string FullName = "Marine Regions";

    /// <summary>
    /// The Api default address
    /// </summary>
    public static readonly Uri DefaultApiAddress = new("https://marineregions.org/rest");
    #endregion

    #region Properties
    /// <inheritdoc />
    public override decimal Version => 1;

    /// <inheritdoc />
    public override string ClientFullName => FullName;

    #endregion

    #region Constructor

    public MarineRegionsClient(HttpClient? httpClient = null) : base(DefaultApiAddress, httpClient)
    {
    }

    #endregion

    #region Methods
    /// <summary>
    /// Gets one record for the given MRGID
    /// </summary>
    /// <param name="mrgId">The MRGID to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerRecord?> GetGazetteerRecord(int mrgId, CancellationToken token = default)
    {
        return GetJsonAsync<GazetteerRecord>($"getGazetteerRecordByMRGID.json/{mrgId}/", token);
    }

    /// <summary>
    /// Gets one record for the given MRGID
    /// </summary>
    /// <param name="mrgId">The MRGID to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<JsonNode?> GetFullGazetteerRecord(int mrgId, CancellationToken token = default)
    {
	    return GetJsonAsync<JsonNode>($"getGazetteerRecordByMRGID.jsonld/{mrgId}/", token);
    }

	/// <summary>
	/// Gets all geometries associated with a gazetteer record
	/// </summary>
	/// <param name="mrgId">The MRGID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<JsonNode?> GetGazetteerGeometries(int mrgId, CancellationToken token = default)
    {
        return GetJsonAsync<JsonNode>($"getGazetteerGeometries.jsonld/{mrgId}/", token);
    }

    /// <summary>
    /// Gets all possible types
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerType[]?> GetGazetteerTypes(CancellationToken token = default)
    {
        return GetJsonAsync<GazetteerType[]>("getGazetteerTypes.json/", token);
    }

    /// <summary>
    /// Gets a list of the first 100 matching records for given name
    /// </summary>
    /// <param name="name">The name to search for</param>
    /// <param name="like">Adds a '%'-sign before and after the GazetteerName (SQL LIKE function). Default=true</param>
    /// <param name="fuzzy">Uses Levenshtein query to find nearest matches. Default=false</param>
    /// <param name="typeIds">One or more placetypeIDs. See <see cref="GetGazetteerTypes"/> to retrieve a list of placetypeIDs. Default=(empty)</param>
    /// <param name="language">Language (ISO 639-1 code). Default=(empty)</param>
    /// <param name="offset">Start record number, in order to page through next batch of results. Default=0</param>
    /// <param name="count">Number of records to retrieve. Default=100; max=100</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerRecord[]?> GetGazetteerRecords(string name, bool like = true, bool fuzzy = false, IEnumerable<int>? typeIds = null, string? language = null, int offset = 0, int count = 100, CancellationToken token = default)
    {
        var parameters = new Dictionary<string, object?>
        {
            {"like", like},
            {"fuzzy", fuzzy},
            {"typeID", typeIds},
            {"language", language},
            {"offset", offset},
            {"count", count},
        };
        return GetJsonAsync<GazetteerRecord[]>($"getGazetteerRecordsByName.json/{name}/", parameters, token);
    }

    /// <summary>
    /// Gets a list of the first 100 matching records for all given names
    /// </summary>
    /// <param name="names">The name to search for</param>
    /// <param name="like">Adds a '%'-sign before and after the GazetteerName (SQL LIKE function). Default=true</param>
    /// <param name="fuzzy">Uses Levenshtein query to find nearest matches. Default=false</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<List<GazetteerRecord[]>?> GetGazetteerRecords(IEnumerable<string?> names, bool like = true, bool fuzzy = false, CancellationToken token = default)
    {
        var namesPath = new StringBuilder();
        foreach (var name in names)
        {
            if(string.IsNullOrWhiteSpace(name)) continue;
            namesPath.Append($"{name}/");
        }

        if (namesPath.Length == 0) throw new ArgumentException("It must contain at least one name", nameof(names));

        return GetJsonAsync<List<GazetteerRecord[]>>($"getGazetteerRecordsByNames.json/{like.ToString().ToLowerInvariant()}/{fuzzy.ToString().ToLowerInvariant()}/{namesPath}", token);
    }

    /// <summary>
    /// Gets the Linked Data Event Stream feed
    /// </summary>
    /// <param name="startDateTime">Feed start range date time</param>
    /// <param name="endDateTime">Feed end range date time</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<JsonNode?> GetFeed(DateTime startDateTime, DateTime endDateTime, CancellationToken token = default)
    {
		// page=2023-07-31T15%3A00%3A00Z%2F2023-07-31T16%3A00%3A00Z
		// page=2023-07-31T15:00:00Z/2023-07-31T16:00:00Z
		//      2023-07-11T22:05:53.8948950+01:00/2023-08-11T22:05:53.8962167+01:00
		//      2023-07-11T21:06:43.0750477Z/2023-08-11T21:06:43.0763630Z
		var parameters = new KeyValuePair<string, object?>("page", $"{startDateTime.ToUniversalTime():O}/{endDateTime.ToUniversalTime():O}");
		return GetJsonAsync<JsonNode>("getFeed.jsonld", parameters, token);
    }

    /// <summary>
    /// Gets the Linked Data Event Stream feed
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<JsonNode?> GetFeed(CancellationToken token = default)
    {
		return GetJsonAsync<JsonNode>("getFeed.jsonld", token);

	}

	/// <summary>
	/// Gets WMS information for the given MRGID
	/// </summary>
	/// <param name="mrgId">The MRGID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<GazetteerWms[]?> GetgetGazetteerWmSes(int mrgId, CancellationToken token = default)
    {
        return GetJsonAsync<GazetteerWms[]>($"getGazetteerWMSes.json/{mrgId}/", token);
    }

    /// <summary>
    /// Gets the first 100 related records for the given MRGID
    /// </summary>
    /// <param name="mrgId">The MRGID to search for</param>
    /// <param name="direction"></param>
    /// <param name="type"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerRecord[]?> GetGazetteerRelations(int mrgId, RelationDirection direction = RelationDirection.Upper, RelationType type = RelationType.PartOf, CancellationToken token = default)
    {
        var parameters = new Dictionary<string, object?>
        {
            {"direction", direction},
            {"type", type},
        };
        return GetJsonAsync<GazetteerRecord[]>($"getGazetteerRelationsByMRGID.json/{mrgId}/", parameters, token);
    }

    /// <summary>
    /// Gets all sources per batch of 100.
    /// </summary>
    /// <param name="offset">Provide offset to return next batch of 100 sources.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerSource[]?> GetGazetteerSources(int offset = 0, CancellationToken token = default)
    {
        var parameters = new KeyValuePair<string, object?>("offset", offset);
        return GetJsonAsync<GazetteerSource[]>("getGazetteerSources.json/", parameters, token);
    }

    /// <summary>
    /// Gets the source name corresponding to a source ID
    /// </summary>
    /// <param name="sourceId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerSourceName[]?> GetGazetteerSource(int sourceId, CancellationToken token = default)
    {
        return GetJsonAsync<GazetteerSourceName[]>($"getGazetteerSourceBySourceID.json/{sourceId}/", token);
    }

    /// <summary>
    /// Gets the first 100 names for the given MRGID
    /// </summary>
    /// <param name="mrgId">The MRGID to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<string[]?> GetGazetteerNames(int mrgId, CancellationToken token = default)
    {
        return GetJsonAsync<string[]>($"getGazetteerNamesByMRGID.json/{mrgId}/", token);
    }

    /// <summary>
    /// Gets the first 100 records for the given source
    /// </summary>
    /// <param name="sourceName">The source name to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerRecord[]?> GetGazetteerRecordsBySource(string sourceName, CancellationToken token = default)
    {
        return GetJsonAsync<GazetteerRecord[]>($"getGazetteerRecordsBySource.json/{EscapeDataString(sourceName)}/", token);
    }

    /// <summary>
    /// Gets all records for the given type, per batch of 100
    /// </summary>
    /// <param name="type">Use <see cref="GetGazetteerTypes"/> to view a complete list of types</param>
    /// <param name="offset">Provide offset to return next batch of 100 records.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<GazetteerRecord[]?> GetGazetteerRecordsByType(string type, int offset = 0, CancellationToken token = default)
    {
        var parameters = new KeyValuePair<string, object?>("offset", offset);
        return GetJsonAsync<GazetteerRecord[]>($"getGazetteerRecordsByType.json/{EscapeDataString(type)}/", parameters, token);
    }

	/// <summary>
	/// Gets all gazetteer records where the geometry intersects with the given latitude and longitude per batch of 100. Results are sorted by area, smallest to largest
	/// </summary>
	/// <param name="longitude">A decimal number which ranges from -90 to 90</param>
	/// <param name="latitude">A decimal number which ranges from -180 to +180</param>
	/// <param name="typeIds">One or more placetypeIDs. See <see cref="GetGazetteerTypes"/> to retrieve a list of placetypeIDs. Default=(empty)</param>
	/// <param name="offset">Start record number, in order to page through next batch of results. Default=0</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<GazetteerRecord[]?> GetGazetteerRecordsByLatLong(decimal latitude, decimal longitude, IEnumerable<int>? typeIds = null, int offset = 0, CancellationToken token = default)
    {
        if (latitude is < -90 or > 90) throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90");
        if (longitude is < -180 or > 180) throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -90 and 90");
        var parameters = new Dictionary<string, object?>
        {
            {"typeID", typeIds},
            {"offset", offset},
        };
        return GetJsonAsync<GazetteerRecord[]>($"getGazetteerRecordsByLatLong.json/{latitude}/{longitude}/", parameters, token);
    }
    #endregion
}