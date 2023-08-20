using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace SpeciesDatabaseApi.MarineSpecies;

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
    public static readonly Uri DefaultApiAddress = new("https://www.marinespecies.org/rest");
    #endregion

    #region Properties
    /// <inheritdoc />
    public override decimal Version => 1;

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
    public Task<AttributeKey[]?> GetAttributeKeys(int attributeId = 0, bool includeInherited = true, CancellationToken token = default)
    {
        var parameters = new QueryParameters("include_children", includeInherited);
        return GetJsonAsync<AttributeKey[]>($"AphiaAttributeKeysByID/{attributeId}", parameters, token);
    }

    /// <summary>
    /// Get a list of attributes for a given AphiaID
    /// </summary>
    /// <param name="aphiaId">The AphiaID to search for</param>
    /// <param name="includeInherited">Include attributes inherited from the taxon its parent(s). Default=false</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Attribute[]?> GetAttributes(int aphiaId, bool includeInherited = false, CancellationToken token = default)
    {
        var parameters = new QueryParameters("include_inherited", includeInherited);
        return GetJsonAsync<Attribute[]>($"AphiaAttributesByAphiaID/{aphiaId}", parameters, token);
    }

    /// <summary>
    /// Get list values that are grouped by an CategoryID
    /// </summary>
    /// <param name="categoryId">The CategoryID to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<AttributeValue[]?> GetAttributeValues(int categoryId = 0, CancellationToken token = default)
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
    public Task<AphiaAttributeSet[]?> GetAphiaIdsAndAttributes(int attributeId, int offset = 1, CancellationToken token = default)
    {
        var parameters = new QueryParameters("offset", offset);
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
    public Task<Distribution[]?> GetDistributions(int aphiaId, CancellationToken token = default)
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
    public Task<string[]?> GetExternalId(int aphiaId, ExternalIdentifierTypeEnum type, CancellationToken token = default)
    {
        var parameters = new QueryParameters("type", type, valueCase:QueryParameterCase.Lower);
        return GetJsonAsync<string[]>($"AphiaExternalIDByAphiaID/{aphiaId}", parameters, token);
    }

    /// <summary>
    /// Get the Aphia Record for a given external identifier
    /// </summary>
    /// <param name="externalId">The external identifier to search for</param>
    /// <param name="type">Type of external identifier to return.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<AphiaRecord?> GetRecordByExternalId(string externalId, ExternalIdentifierTypeEnum type, CancellationToken token = default)
    {
        var parameters = new QueryParameters("type", type, valueCase: QueryParameterCase.Lower);
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
    public Task<Source[]?> GetSources(int aphiaId, CancellationToken token = default)
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
    public Task<AphiaRecord[]?> GetChildren(int aphiaId, bool marineOnly = true, int offset = 1, CancellationToken token = default)
    {
        var parameters = new QueryParameters
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
    public Task<Classification?> GetClassification(int aphiaId, CancellationToken token = default)
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
    public Task<int?> GetAphiaId(string scientificName, bool marineOnly = true, CancellationToken token = default)
    {
        var parameters = new QueryParameters("marine_only", marineOnly);
        return GetJsonAsync<int?>($"AphiaIDByName/{Uri.EscapeDataString(scientificName)}", parameters, token);
    }

    /// <summary>
    /// Get the name for a given AphiaID
    /// </summary>
    /// <param name="aphiaId">The AphiaID to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<string?> GetAphiaName(int aphiaId,  CancellationToken token = default)
    {
        return GetJsonAsync<string>($"AphiaNameByAphiaID/{aphiaId}", token);
    }

    /// <summary>
    /// Get the complete AphiaRecord for a given AphiaID
    /// </summary>
    /// <param name="aphiaId">The AphiaID to search for</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<AphiaRecord?> GetRecord(int aphiaId, CancellationToken token = default)
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
    public Task<JsonNode?> GetFullRecord(int aphiaId, CancellationToken token = default)
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
    public Task<AphiaRecord[]?> GetRecords(IEnumerable<int> aphiaIds, CancellationToken token = default)
    {
        var parameters = new QueryParameters("aphiaids[]", aphiaIds);
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
    public Task<AphiaRecord[]?> GetRecords(DateTime startDate, DateTime endDate, bool marineOnly = true, int offset = 1, CancellationToken token = default)
    {
        var parameters = new QueryParameters
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
    public Task<List<AphiaRecord[]>?> GetRecordsFuzzy(IEnumerable<string> scientificNames, bool marineOnly = true, CancellationToken token = default)
    {
        var parameters = new QueryParameters
        {
            {"scientificnames[]", scientificNames},
            {"marine_only", marineOnly}
        };
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
    public Task<AphiaRecord[]?> GetRecords(string scientificName, bool like = true, bool marineOnly = true, int offset = 1, CancellationToken token = default)
    {
        var parameters = new QueryParameters
        {
            {"like", like},
            {"marine_only", marineOnly},
            {"offset", offset},
        };
        return GetJsonAsync<AphiaRecord[]>($"AphiaRecordsByName/{EscapeDataString(scientificName)}", parameters, token);
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
    public Task<List<AphiaRecord[]>?> GetRecords(IEnumerable<string> scientificNames, bool like = false, bool marineOnly = true, CancellationToken token = default)
    {
        var parameters = new QueryParameters
        {
            {"scientificnames[]", scientificNames},
            {"like", like},
            {"marine_only", marineOnly}
        };
            
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
    public Task<AphiaRecord[]?> GetRecords(int taxonId, int? belongsToAphiaId, int offset = 1, CancellationToken token = default)
    {
        var parameters = new QueryParameters
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
    public Task<AphiaRecord[]?> GetSynonyms(int aphiaId, int offset = 1, CancellationToken token = default)
    {
        var parameters = new QueryParameters("offset", offset);
        return GetJsonAsync<AphiaRecord[]>($"AphiaSynonymsByAphiaID/{aphiaId}", parameters, token);
    }

    /// <summary>
    /// Get taxonomic ranks by their identifier
    /// </summary>
    /// <param name="taxonId">A taxonomic rank identifier. Default=-1</param>
    /// <param name="aphiaId">The AphiaID of the kingdom. Default=-1</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<AphiaRank[]?> GetTaxonRanks(int taxonId = -1, int aphiaId = -1, CancellationToken token = default)
    {
        var parameters = new QueryParameters("AphiaID", aphiaId);
        return GetJsonAsync<AphiaRank[]>($"AphiaTaxonRanksByID/{taxonId}", parameters, token);
    }

    /// <summary>
    /// Get taxonomic ranks by their name
    /// </summary>
    /// <param name="taxonRank">A taxonomic rank. Default=empty</param>
    /// <param name="aphiaId">The AphiaID of the kingdom. Default=-1</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<AphiaRank[]?> GetTaxonRanks(string taxonRank = "", int aphiaId = -1, CancellationToken token = default)
    {
        var parameters = new QueryParameters("AphiaID", aphiaId);
        return GetJsonAsync<AphiaRank[]>($"AphiaTaxonRanksByName/{Uri.EscapeDataString(taxonRank)}", parameters, token);
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
    public Task<AphiaRecord[]?> GetRecordsByVernacular(string vernacular, bool like = false, int offset = 1, CancellationToken token = default)
    {
        var parameters = new QueryParameters
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
    public Task<VernacularItem[]?> GetVernaculars(int aphiaId, CancellationToken token = default)
    {
        return GetJsonAsync<VernacularItem[]>($"AphiaVernacularsByAphiaID/{aphiaId}", token);
    }
	#endregion

	#region Static Methods

	/// <summary>
	/// Gets the link for an aphia based on aphia name<br/>
	/// (This assumes that you have no idea if the taxon exists, or you do not want to look up the AphiaID)
	/// </summary>
	/// <param name="aphiaName">The aphia name to look for</param>
	/// <returns></returns>
	public static string GetAphiaLink(string aphiaName)
    {
	    return $"https://www.marinespecies.org/aphia.php?p=taxlist&tName={EscapeDataString(aphiaName)}";
    }

	/// <summary>
	/// Gets the link for an aphia based on aphiaId
	/// </summary>
	/// <param name="aphiaId">The aphiaId to look for</param>
	/// <returns></returns>
	public static string GetAphiaLink(int aphiaId)
    {
	    return $"https://www.marinespecies.org/aphia.php?p=taxdetails&id={aphiaId}";
    }

	#endregion
}