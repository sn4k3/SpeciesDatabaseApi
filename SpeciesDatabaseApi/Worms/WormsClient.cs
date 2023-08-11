using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace SpeciesDatabaseApi.Worms;

/// <summary>
/// The client for https://marinespecies.org API
/// </summary>
public class WormsClient : BaseClient
{
	#region Static objects
	/// <summary>
	/// The client full name/provider
	/// </summary>
	public const string FullName = "World Register of Marine Species";

	/// <summary>
	/// The Api default address
	/// </summary>
	public static readonly Uri DefaultApiAddress = new("https://marinespecies.org/rest");
    #endregion

    #region Properties
    /// <inheritdoc />
    public override int Version => 1;

    /// <inheritdoc />
	public override string ClientFullName => FullName;

	#endregion

	#region Constructor

	public WormsClient(HttpClient? httpClient = null) : base(DefaultApiAddress, httpClient)
    {
    }

    #endregion

    #region Attributes
    /// <summary>
    /// Get attribute definitions. To refer to root items specify ID = 0
    /// </summary>
    /// <param name="attributeId">The attribute definition id to search for</param>
    /// <param name="includeInherited">Include the tree of children. Default=true</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<AttributeKey[]?> GetAphiaAttributeKeysById(int attributeId = 0, bool includeInherited = true, CancellationToken token = default)
    {
	    var parameters = new KeyValuePair<string, object>("include_children", includeInherited);
        return GetJsonAsync<AttributeKey[]>($"AphiaAttributeKeysByID/{attributeId}", parameters, token);
    }

    /// <summary>
    /// Get a list of attributes for a given AphiaID
    /// </summary>
    /// <param name="aphiaId">The AphiaID to search for</param>
    /// <param name="includeInherited">Include attributes inherited from the taxon its parent(s). Default=false</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Attribute[]?> GetAphiaAttributesByAphiaId(int aphiaId, bool includeInherited = false, CancellationToken token = default)
    {
	    var parameters = new KeyValuePair<string, object>("include_inherited", includeInherited);
        return GetJsonAsync<Attribute[]>($"AphiaAttributesByAphiaID/{aphiaId}", parameters, token);
    }

	/// <summary>
	/// Get list values that are grouped by an CategoryID
	/// </summary>
	/// <param name="categoryId">The CategoryID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AttributeValue[]?> GetAphiaAttributeValuesByCategoryId(int categoryId = 0, CancellationToken token = default)
    {
        return GetJsonAsync<AttributeValue[]>($"AphiaAttributeValuesByCategoryID/{categoryId}", token);
    }

    /// <summary>
    /// Get a list of AphiaIDs (max 50) with attribute tree for a given attribute definition ID
    /// </summary>
    /// <param name="attributeId">The attribute definition id to search for</param>
    /// <param name="offset">Starting record number, when retrieving next chunk of (50) records. Default=1</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<AphiaAttributeSet[]?> GetAphiaIDsByAttributeKeyId(int attributeId, int offset = 1, CancellationToken token = default)
    {
	    var parameters = new KeyValuePair<string, object>("offset", offset);
        return GetJsonAsync<AphiaAttributeSet[]>($"AphiaIDsByAttributeKeyID/{attributeId}", parameters, token);
    }
	#endregion

	#region Distributions
	/// <summary>
	/// Get all distributions for a given AphiaID
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<Distribution[]?> GetAphiaDistributionsByAphiaId(int aphiaId, CancellationToken token = default)
    {
        return GetJsonAsync<Distribution[]>($"AphiaDistributionsByAphiaID/{aphiaId}", token);
	}
	#endregion

	#region External identifiers
	/// <summary>
	/// Get any external identifier(s) for a given AphiaID
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="type">Type of external identifier to return.</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<string[]?> GetAphiaExternalIdByAphiaId(int aphiaId, ExternalIdentifierType type, CancellationToken token = default)
    {
	    var parameters = new KeyValuePair<string, object>("type", type);
        return GetJsonAsync<string[]>($"AphiaExternalIDByAphiaID/{aphiaId}", parameters, token);
	}

	/// <summary>
	/// Get the Aphia Record for a given external identifier
	/// </summary>
	/// <param name="externalId">The external identifier to search for</param>
	/// <param name="type">Type of external identifier to return.</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord?> GetAphiaRecordByExternalId(string externalId, ExternalIdentifierType type, CancellationToken token = default)
    {
	    var parameters = new KeyValuePair<string, object>("type", type);
	    return GetJsonAsync<AphiaRecord>($"AphiaRecordByExternalID/{externalId}", parameters, token);
	}
    #endregion

    #region Sources
    /// <summary>
    /// Get one or more sources/references including links, for one AphiaID
    /// </summary>
    /// <param name="aphiaId">The AphiaID to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Source[]?> GetAphiaSourcesByAphiaId(int aphiaId, CancellationToken token = default)
	{
		return GetJsonAsync<Source[]>($"AphiaSourcesByAphiaID/{aphiaId}", token);
	}
	#endregion

	#region Taxonomic data

	/// <summary>
	/// Get the direct children (max. 50) for a given AphiaID
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="marineOnly">Limit to marine taxa. Default=true</param>
	/// <param name="offset">Starting record number, when retrieving next chunk of (50) records. Default=1</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord[]?> GetAphiaChildrenByAphiaId(int aphiaId, bool marineOnly = true, int offset = 1, CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object>
		{
			{"marine_only", marineOnly},
			{"offset", offset},
		};
		return GetJsonAsync<AphiaRecord[]>($"AphiaChildrenByAphiaID/{aphiaId}", parameters, token);
	}

	/// <summary>
	/// Get the complete classification for one taxon. This also includes any sub or super ranks.
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<Classification?> GetAphiaClassificationByAphiaId(int aphiaId, CancellationToken token = default)
	{
		return GetJsonAsync<Classification>($"AphiaClassificationByAphiaID/{aphiaId}", token);
	}

	/// <summary>
	/// Get the AphiaID for a given name.
	/// </summary>
	/// <param name="scientificName">Name to search for</param>
	/// <param name="marineOnly">Limit to marine taxa. Default=true</param>
	/// <param name="token"></param>
	/// <returns>NULL when no match is found; -999 when multiple matches are found; an integer(AphiaID) when one exact match was found</returns>
	public Task<int?> GetAphiaIdByName(string scientificName, bool marineOnly = true, CancellationToken token = default)
	{
		var parameters = new KeyValuePair<string, object>("marine_only", marineOnly);
		return GetJsonAsync<int?>($"AphiaIDByName/{Uri.EscapeDataString(scientificName)}", parameters, token);
	}

	/// <summary>
	/// Get the name for a given AphiaID
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<string?> GetAphiaNameByAphiaId(int aphiaId,  CancellationToken token = default)
	{
		return GetJsonAsync<string>($"AphiaNameByAphiaID/{aphiaId}", token);
	}

	/// <summary>
	/// Get the complete AphiaRecord for a given AphiaID
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord?> GetAphiaRecordByAphiaId(int aphiaId, CancellationToken token = default)
	{
		return GetJsonAsync<AphiaRecord>($"AphiaRecordByAphiaID/{aphiaId}", token);
	}

	/// <summary>
	/// Returns the linked open data implementation of an AphiaRecord for a given AphiaID.<br/>
	/// There are multiple formats available that can be chosen with the Accept header. The default format is JSON-LD but RDF, Turtle and N-triples are also supported.
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<JsonNode?> GetAphiaRecordFullByAphiaId(int aphiaId, CancellationToken token = default)
	{
		// TODO: Implement this with DTO
		return GetJsonAsync<JsonNode>($"AphiaRecordFullByAphiaID/{aphiaId}", token);
	}

	/// <summary>
	/// Get an AphiaRecord for multiple AphiaIDs in one go (max: 50)
	/// </summary>
	/// <param name="aphiaIds">The AphiaIDs to search for</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord[]?> GetAphiaRecordsByAphiaIds(IEnumerable<int> aphiaIds, CancellationToken token = default)
	{
		var parameters = aphiaIds.Select(aphiaId => new KeyValuePair<string, object>("aphiaids[]", aphiaId));
		return GetJsonAsync<AphiaRecord[]>("AphiaRecordsByAphiaIDs", parameters, token);
	}

	/// <summary>
	/// Lists all AphiaRecords (taxa) that have their last edit action (modified or added) during the specified period
	/// </summary>
	/// <param name="startDate">Start date time to lookup</param>
	/// <param name="endDate">End date time to lookup</param>
	/// <param name="marineOnly">Limit to marine taxa. Default=true</param>
	/// <param name="offset">Starting record number, when retrieving next chunk of (50) records. Default=1</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord[]?> GetAphiaRecordsByDate(DateTime startDate, DateTime endDate, bool marineOnly = true, int offset = 1, CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object>
		{
			{"startdate", startDate.ToString("O")},
			{"enddate", endDate.ToString("O")},
			{"marine_only", marineOnly},
			{"offset", offset}
		};
		return GetJsonAsync<AphiaRecord[]>("AphiaRecordsByDate", parameters, token);
	}

	/// <summary>
	/// For each given scientific name (may include authority), try to find one or more AphiaRecords, using the TAXAMATCH fuzzy matching algorithm by Tony Rees.<br/>
	/// This allows you to(fuzzy) match multiple names in one call.<br/>
	/// Limited to 50 names at once for performance reasons
	/// </summary>
	/// <param name="scientificNames">Names to search for</param>
	/// <param name="marineOnly">Limit to marine taxa. Default=true</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<List<AphiaRecord[]>?> GetAphiaRecordsByMatchNames(IEnumerable<string> scientificNames, bool marineOnly = true, CancellationToken token = default)
	{
		var parameters = scientificNames.Select(aphiaId => new KeyValuePair<string, object>("scientificnames[]", aphiaId)).ToList();
		parameters.Add(new KeyValuePair<string, object>("marine_only", marineOnly));
		return GetJsonAsync<List<AphiaRecord[]>>("AphiaRecordsByMatchNames", parameters, token);
	}

	/// <summary>
	/// Get one or more matching (max. 50) AphiaRecords for a given name
	/// </summary>
	/// <param name="scientificName">Name to search for</param>
	/// <param name="like">Add a '%'-sign added after the ScientificName (SQL LIKE function). Default=true</param>
	/// <param name="marineOnly">Limit to marine taxa. Default=true</param>
	/// <param name="offset">Starting record number, when retrieving next chunk of (50) records. Default=1</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord[]?> GetAphiaRecordsByName(string scientificName, bool like = true, bool marineOnly = true, int offset = 1, CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object>
		{
			{"like", like},
			{"marine_only", marineOnly},
			{"offset", offset},
		};
		return GetJsonAsync<AphiaRecord[]>($"AphiaRecordsByName/{Uri.EscapeDataString(scientificName)}", parameters, token);
	}

	/// <summary>
	/// For each given scientific name, try to find one or more AphiaRecords.<br/>
	/// This allows you to match multiple names in one call.<br/>
	/// Limited to 500 names at once for performance reasons.
	/// </summary>
	/// <param name="scientificNames">Names to search for</param>
	/// <param name="like">Add a '%'-sign after the ScientificName (SQL LIKE function). Default=false</param>
	/// <param name="marineOnly">Limit to marine taxa. Default=true</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<List<AphiaRecord[]>?> GetAphiaRecordsByNames(IEnumerable<string> scientificNames, bool like = false, bool marineOnly = true, CancellationToken token = default)
	{
		var parameters = scientificNames.Select(aphiaId => new KeyValuePair<string, object>("scientificnames[]", aphiaId)).ToList();
		parameters.Add(new KeyValuePair<string, object>("like", like));
		parameters.Add(new KeyValuePair<string, object>("marine_only", marineOnly));
		return GetJsonAsync<List<AphiaRecord[]>>("AphiaRecordsByNames", parameters, token);
	}

	/// <summary>
	/// Get the AphiaRecords for a given taxonRankID (max 50)
	/// </summary>
	/// <param name="taxonId">A taxonomic rank identifier</param>
	/// <param name="belongsToAphiaId">Limit the results to descendants of the given AphiaID</param>
	/// <param name="offset">Starting record number, when retrieving next chunk of (50) records. Default=1</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord[]?> GetAphiaRecordsByTaxonRankId(int taxonId, int belongsToAphiaId, int offset = 1, CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object>
		{
			{"belongsTo", belongsToAphiaId},
			{"offset", offset},
		};
		return GetJsonAsync<AphiaRecord[]>($"AphiaRecordsByTaxonRankID/{taxonId}", parameters, token);
	}

	/// <summary>
	/// Get all synonyms for a given AphiaID
	/// </summary>
	/// <param name="aphiaId">The AphiaID to search for</param>
	/// <param name="offset">Starting record number, when retrieving next chunk of (50) records. Default=1</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRecord[]?> GetAphiaSynonymsByAphiaId(int aphiaId, int offset = 1, CancellationToken token = default)
	{
		var parameter = new KeyValuePair<string, object>("offset", offset);
		return GetJsonAsync<AphiaRecord[]>($"AphiaSynonymsByAphiaID/{aphiaId}", parameter, token);
	}

	/// <summary>
	/// Get taxonomic ranks by their identifier
	/// </summary>
	/// <param name="taxonId">A taxonomic rank identifier. Default=-1</param>
	/// <param name="aphiaId">The AphiaID of the kingdom. Default=-1</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRank[]?> GetAphiaTaxonRanksById(int taxonId = -1, int aphiaId = -1, CancellationToken token = default)
	{
		var parameter = new KeyValuePair<string, object>("AphiaID", aphiaId);
		return GetJsonAsync<AphiaRank[]>($"AphiaTaxonRanksByID/{taxonId}", parameter, token);
	}

	/// <summary>
	/// Get taxonomic ranks by their name
	/// </summary>
	/// <param name="taxonRank">A taxonomic rank. Default=empty</param>
	/// <param name="aphiaId">The AphiaID of the kingdom. Default=-1</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<AphiaRank[]?> GetAphiaTaxonRanksByName(string taxonRank = "", int aphiaId = -1, CancellationToken token = default)
	{
		var parameter = new KeyValuePair<string, object>("AphiaID", aphiaId);
		return GetJsonAsync<AphiaRank[]>($"AphiaTaxonRanksByName/{Uri.EscapeDataString(taxonRank)}", parameter, token);
	}
	#endregion

	#region Vernaculars
	/// <summary>
	/// Get one or more Aphia Records (max. 50) for a given vernacular
	/// </summary>
	/// <param name="vernacular">The vernacular to find records for</param>
	/// <param name="like">Add a '%'-sign before and after the input (SQL LIKE '%vernacular%' function)</param>
	/// <param name="offset">Starting record number, when retrieving next chunk of (50) records</param>
	/// <param name="token"></param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	public Task<AphiaRecord[]?> GetAphiaRecordsByVernacular(string vernacular, bool like = false, int offset = 1, CancellationToken token = default)
    {
        var parameters = new Dictionary<string, object>
        {
            {nameof(like), like},
            {nameof(offset), offset},
        };
        return GetJsonAsync<AphiaRecord[]>($"AphiaRecordsByVernacular/{EscapeDataString(vernacular)}", parameters, token);
    }

    /// <summary>
    /// Get all vernaculars for a given AphiaID
    /// </summary>
    /// <param name="aphiaId">The AphiaID to search for</param>
    /// <param name="token"></param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<VernacularItem[]?> GetAphiaVernacularsByAphiaId(int aphiaId, CancellationToken token = default)
    {
        return GetJsonAsync<VernacularItem[]>($"AphiaVernacularsByAphiaID/{aphiaId}", token);
    }
	#endregion
}