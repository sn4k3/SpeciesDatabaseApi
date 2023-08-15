using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SpeciesDatabaseApi.Extensions;

namespace SpeciesDatabaseApi.BoldSystems;

/// <summary>
/// The client for https://www.boldsystems.org API
/// </summary>
public class BoldSystemsClient : BaseClient
{
    #region Static objects
    /// <summary>
    /// The client full name/provider
    /// </summary>
    public const string FullName = "Barcode of Life Data Sytem";

    /// <summary>
    /// The Api default address
    /// </summary>
    public static readonly Uri DefaultApiAddress = new("https://www.boldsystems.org/index.php");
    #endregion

    #region Properties
    /// <inheritdoc />
    public override decimal Version => 4;

    /// <inheritdoc />
    public override string ClientAcronym => "BoldSystems";

    /// <inheritdoc />
    public override string ClientFullName => FullName;

    #endregion

    #region Constructor

    public BoldSystemsClient(HttpClient? httpClient = null) : base(DefaultApiAddress, httpClient)
    {
    }

	#endregion

	#region Methods

	/// <summary>
	/// Users only interested in count data for a given query can now use the API to retrieve the summary information that is provided by BOLD public searches.
	/// </summary>
	/// <param name="requestParameters">Parameters for the request, null values are ignored</param>
	/// <param name="dataType">Returns all records in one of the specified formats.</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<Stats?> GetStats(PublicApiParameters requestParameters, BoldStatsDataType dataType = BoldStatsDataType.DrillDown, CancellationToken token = default)
	{
		var parameters = GetDictionaryFromClassProperties(requestParameters);
		if (parameters.Count == 0) throw new ArgumentException("The call require at least one parameter", nameof(requestParameters));
		parameters.TryAdd("dataType", StringExtensions.PrependCharByUpperChar(dataType.ToString(), '_').ToLowerInvariant());
		parameters.TryAdd("format", "json");
		return GetJsonAsync<Stats>("API_Public/stats", parameters, token);
	}

	/// <summary>
	/// Users can query the system to retrieve matching specimen data records for a combination of parameters.
	/// </summary>
	/// <param name="requestParameters">Parameters for the request, null values are ignored</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<SpecimenResults?> GetSpecimen(PublicApiParameters requestParameters, CancellationToken token = default)
	{
		var parameters = GetDictionaryFromClassProperties(requestParameters);
		if (parameters.Count == 0) throw new ArgumentException("The call require at least one parameter", nameof(requestParameters));
		parameters.TryAdd("format", "json");
		return GetJsonAsync<SpecimenResults>("API_Public/specimen", parameters, token);
	}


    /// <summary>
    /// Users can query the system to retrieve matching sequences for a combination of parameters.
    /// </summary>
    /// <param name="requestParameters">Parameters for the request, null values are ignored</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<SequenceData>> GetSequences(PublicApiParameters requestParameters, CancellationToken token = default)
    {
        var parameters = GetDictionaryFromClassProperties(requestParameters);
        if (parameters.Count == 0) throw new ArgumentException("The call require at least one parameter", nameof(requestParameters));
        using var response = await GetResponseAsync("API_Public/sequence", parameters, token).ConfigureAwait(false);

        var list = new List<SequenceData>();

        if (response.StatusCode == HttpStatusCode.NoContent) return list;

        var stream = await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
        var streamReader = new StreamReader(stream);

        while (await streamReader.ReadLineAsync().ConfigureAwait(false) is { } line1)
        {
            // Example: >FCCA006-09|Ogilbia|COI-5P|JQ841943
            if (line1.Length == 0 || line1[0] != '>') continue;
            var splitLine = line1[1..].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (splitLine.Length != 4) continue;
            if (await streamReader.ReadLineAsync().ConfigureAwait(false) is not { } line2) break;
            if (line2.Length < 10) continue;
            if (line2[0] != '-' && !char.IsUpper(line2[0])) continue;

            list.Add(new SequenceData
            {
                ProcessId = splitLine[0],
                Identification = splitLine[1],
                Marker = splitLine[2],
                Accession = splitLine[3],
                Nucleotides = line2
            });
        }

        return list;
    }

    /// <summary>
    /// Users can query the system to retrieve matching specimen data and sequence records for a combination of parameters.
    /// </summary>
    /// <param name="requestParameters">Parameters for the request, null values are ignored</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<SpecimenResults?> GetSpecimenAndSequence(PublicApiParameters requestParameters, CancellationToken token = default)
    {
        var parameters = GetDictionaryFromClassProperties(requestParameters);
        if (parameters.Count == 0) throw new ArgumentException("The call require at least one parameter", nameof(requestParameters));
        parameters.TryAdd("format", "json");
        return GetJsonAsync<SpecimenResults>("API_Public/combined", parameters, token);
    }

    /// <summary>
    /// Users can query the system to retrieve matching specimen data records for a combination of parameters
    /// </summary>
    /// <param name="requestParameters">Parameters for the request, null values are ignored</param>
    /// <param name="stream">The stream to copy the content to</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task DownloadTraceFile(PublicApiParameters requestParameters, Stream stream, CancellationToken token = default)
    {
        var parameters = GetDictionaryFromClassProperties(requestParameters);
        if (parameters.Count == 0) throw new ArgumentException("The call require at least one parameter", nameof(requestParameters));
        return DownloadAsync("API_Public/trace", parameters, stream, token);
    }

    /// <summary>
    /// Users can query the system to retrieve matching specimen data records for a combination of parameters
    /// </summary>
    /// <param name="requestParameters">Parameters for the request, null values are ignored</param>
    /// <param name="filePath">The destination file to save the content</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task DownloadTraceFile(PublicApiParameters requestParameters, string filePath, CancellationToken token = default)
    {
        var parameters = GetDictionaryFromClassProperties(requestParameters);
        if (parameters.Count == 0) throw new ArgumentException("The call require at least one parameter", nameof(requestParameters));

        await using var fs = File.Open(filePath, FileMode.Create);
		await DownloadAsync("API_Public/trace", parameters, fs, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves taxonomy information by BOLD taxonomy ID
    /// </summary>
    /// <param name="taxId">The taxID to search for.</param>
    /// <param name="dataTypes">Specifies the datatypes that will be returned.</param>
    /// <param name="token"></param>
    /// <returns>the top public matches (up to 100) can be retrieved by querying a COI sequence.</returns>
    public Task<TaxonData?> GetTaxonData(int taxId, BoldDataTypesEnum dataTypes = BoldDataTypesEnum.Basic, CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object?>
		{
			{"taxId", taxId},
			{"dataTypes", dataTypes},
			{"includeTree", false}
		};
		return GetJsonAsync<TaxonData>("API_Tax/TaxonData", parameters, token);
	}

	/// <summary>
	/// Retrieves taxonomy information by BOLD taxonomy ID and list containing information for parent taxa as well as the specified taxon
	/// </summary>
	/// <param name="taxId">The taxID to search for.</param>
	/// <param name="dataTypes">Specifies the datatypes that will be returned.</param>
	/// <param name="token"></param>
	/// <returns>the top public matches (up to 100) can be retrieved by querying a COI sequence.</returns>
	public Task<Dictionary<int, TaxonData>?> GetTaxonDataIncludeTree(int taxId, BoldDataTypesEnum dataTypes = BoldDataTypesEnum.Basic, CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object?>
		{
			{"taxId", taxId},
			{"dataTypes", dataTypes},
			{"includeTree", true}
		};
		return GetJsonAsync<Dictionary<int, TaxonData>>("API_Tax/TaxonData", parameters, token);
	}

	/// <summary>
	/// Retrieves taxonomy information by taxon name.
	/// </summary>
	/// <param name="taxName">The tax name to search for.</param>
	/// <param name="fuzzy">Specifies if the search should only find exact matches. All searches are case sensitive.</param>
	/// <param name="token"></param>
	/// <returns>the top public matches (up to 100) can be retrieved by querying a COI sequence.</returns>
	public Task<TaxonSearch?> GetTaxonData(string taxName, bool fuzzy = false, CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object?>
		{
			{"taxName", taxName},
			{"fuzzy", fuzzy},
		};
		return GetJsonAsync<TaxonSearch>("API_Tax/TaxonSearch", parameters, token);
	}

	/// <summary>
	/// Query the COI ID Engine.
	/// </summary>
	/// <param name="db">Specifies the identification database to query (db names are case sensitive).</param>
	/// <param name="sequence">Specifies the query sequence (sequences are not case sensitive).</param>
	/// <param name="token"></param>
	/// <returns>the top public matches (up to 100) can be retrieved by querying a COI sequence.</returns>
	public Task<CoiMatches?> GetCoiMatches(BoldDatabaseEnum db, string sequence, CancellationToken token = default)
    {
        var parameters = new Dictionary<string, object?>
        {
            {"db", db},
            {"sequence", sequence}
        };
        return GetXmlAsync<CoiMatches>("Ids_xml", parameters, token);
    }

    #endregion
}