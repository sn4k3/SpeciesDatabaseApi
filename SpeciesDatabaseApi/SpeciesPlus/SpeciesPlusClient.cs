using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SpeciesDatabaseApi.Extensions;

namespace SpeciesDatabaseApi.SpeciesPlus;

/// <summary>
/// The client for https://iucn.org API
/// </summary>
public class SpeciesPlusClient : BaseClient
{
    #region Static objects
    /// <summary>
    /// The client full name/provider
    /// </summary>
    public const string FullName = "Species+/CITES";

    /// <summary>
    /// The Api default address
    /// </summary>
    public static readonly Uri DefaultApiAddress = new("https://api.speciesplus.net/api/v1");
    #endregion

    #region Properties
    /// <inheritdoc />
    public override decimal Version => 1;

    /// <inheritdoc />
    public override string ClientAcronym => "Species+";

    /// <inheritdoc />
    public override string ClientFullName => FullName;

    #endregion

    #region Constructor

    public SpeciesPlusClient(string apiToken, HttpClient? httpClient = null) : base(DefaultApiAddress, httpClient)
    {
        ApiToken.Set("X-Authentication-Token", apiToken, ApiTokenPlacement.Header);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Lists current CITES appendix listings and reservations, CITES quotas, and CITES suspensions for a given taxon concept
    /// </summary>
    /// <param name="taxonConceptId">Taxon Concept ID</param>
    /// <param name="scope">Time scope of legislation. Select all, current or historic. Defaults to current.</param>
    /// <param name="language">Select language for the text of legislation notes. Select en, fr, or es. Defaults to en.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<CitesLegislation?> GetCitesLegislation(int taxonConceptId, ScopeEnum scope = ScopeEnum.Current, string language = "en", CancellationToken token = default)
    {
        var parameters = new Dictionary<string, object?>
        {
            {"scope", StringExtensions.PrependSpaceByUpperChar(scope.ToString())},
            {"language", language}
        };
        return GetJsonAsync<CitesLegislation>($"taxon_concepts/{taxonConceptId}/cites_legislation", parameters, token);
    }

    /// <summary>
    /// Lists current EU annex listings, SRG opinions, and EU suspensions for a given taxon concept
    /// </summary>
    /// <param name="taxonConceptId">Taxon Concept ID</param>
    /// <param name="scope">Time scope of legislation. Select all, current or historic. Defaults to current.</param>
    /// <param name="language">Select language for the text of legislation notes. Select en, fr, or es. Defaults to en.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<EuLegislation?> GetEuLegislation(int taxonConceptId, ScopeEnum scope = ScopeEnum.Current, string language = "en", CancellationToken token = default)
    {
        var parameters = new Dictionary<string, object?>
        {
            {"scope", StringExtensions.PrependSpaceByUpperChar(scope.ToString())},
            {"language", language}
        };
        return GetJsonAsync<EuLegislation>($"taxon_concepts/{taxonConceptId}/eu_legislation", parameters, token);
    }

    /// <summary>
    /// Lists distributions for a given taxon concept
    /// </summary>
    /// <param name="taxonConceptId">Taxon Concept ID</param>
    /// <param name="language">Select language for the names of distributions.. Select en, fr, or es. Defaults to en.</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Distribution[]?> GetDistributions(int taxonConceptId, string language = "en", CancellationToken token = default)
    {
        var parameters = new KeyValuePair<string, object?>("language", language);
        return GetJsonAsync<Distribution[]>($"taxon_concepts/{taxonConceptId}/distributions", parameters, token);
    }

    /// <summary>
    /// Lists references for a given taxon concept
    /// </summary>
    /// <param name="taxonConceptId">Taxon Concept ID</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Reference[]?> GetReferences(int taxonConceptId, CancellationToken token = default)
    {
        return GetJsonAsync<Reference[]>($"taxon_concepts/{taxonConceptId}/references", token);
	}

	/// <summary>
	/// Lists taxon concepts
	/// </summary>
	/// <param name="name">Filter taxon concepts by name</param>
	/// <param name="taxonomy">Filter taxon concepts by taxonomy, accepts either CITES or CMS as its value. Defaults to CITES if no value is specified</param>
	/// <param name="withDescendants">Broadens the above search by name to include higher taxa.</param>
	/// <param name="withEuListings">Include EU listing data</param>
	/// <param name="updatedSince">Pull only objects updated after (and including) the specified timestamp in ISO8601 format (UTC time)</param>
	/// <param name="language">Filter languages returned for common names. Value should be a single country code or a comma separated string of country codes (e.g. language=EN,PL,IT). Defaults to showing all available languages if no language parameter is specified</param>
	/// <param name="page">Page number for paginated responses</param>
	/// <param name="perPage">Limit for how many objects returned per page for paginated responses. If not specificed it will default to the maximum value of 500</param>
	/// <param name="token"></param>
	/// <returns></returns>
	public Task<TaxonConcepts?> GetTaxonConcepts(string? name = null, TaxomonyEnum taxonomy = TaxomonyEnum.CITES, bool withDescendants = false, bool withEuListings = false, 
		DateTime? updatedSince = null, string language = "en", int page = 1, int perPage = 500,  CancellationToken token = default)
	{
		var parameters = new Dictionary<string, object?>
		{
			{"name", name},
			{"taxonomy", taxonomy},
			{"with_descendants", withDescendants},
			{"with_eu_listings", withEuListings},
			{"updated_since", updatedSince?.ToUniversalTime().ToString("O")},
			{"language", language},
			{"page", page},
			{"per_page", perPage},
		};
		return GetJsonAsync<TaxonConcepts>("taxon_concepts", parameters, token);
	}

	#endregion
}