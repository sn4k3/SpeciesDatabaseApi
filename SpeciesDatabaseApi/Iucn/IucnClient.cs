using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SpeciesDatabaseApi.Iucn;

/// <summary>
/// The client for https://iucn.org API
/// </summary>
public class IucnClient : BaseClient
{
    #region Static objects
    /// <summary>
    /// The client full name/provider
    /// </summary>
    public const string FullName = "International Union for Conservation of Nature";

    /// <summary>
    /// The Api default address
    /// </summary>
    public static readonly Uri DefaultApiAddress = new("http://apiv3.iucnredlist.org/api/v3");
    #endregion

    #region Properties
    /// <inheritdoc />
    public override int Version => 3;

    /// <inheritdoc />
    public override string ClientFullName => FullName;

    #endregion

    #region Constructor

    public IucnClient(string apiToken, HttpClient? httpClient = null) : base(DefaultApiAddress, httpClient)
    {
        ApiToken.Set("token", apiToken, ApiTokenPlacement.Get);
    }

    #endregion

    #region Attributes
    /// <summary>
    /// Check what version of the IUCN Red List is driving the API
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IucnVersion?> GetVersion(CancellationToken token = default)
    {
        return GetJsonAsync<IucnVersion>("version", token);
    }

    /// <summary>
    /// Gets a list of countries
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ArrayResults<CountryItem>?> GetCountries(CancellationToken token = default)
    {
        return GetJsonAsync<ArrayResults<CountryItem>>("country/list", token);
    }

    /// <summary>
    /// Gets a list of species by a country code
    /// </summary>
    /// <param name="countryCode">The country iso code to search</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<CountrySpecies?> GetCountrySpecies(string countryCode, CancellationToken token = default)
    {
        if (countryCode.Length != 2)
        {
            throw new ArgumentException($"{nameof(countryCode)} must have only 2 chars", nameof(countryCode));
        }
        return GetJsonAsync<CountrySpecies>($"country/getspecies/{EscapeDataString(countryCode)}", token);
    }

    /// <summary>
    /// Gets a list of regions
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ArrayResults<Region>?> GetRegions(CancellationToken token = default)
    {
        return GetJsonAsync<ArrayResults<Region>>("region/list", token);
    }

    /// <summary>
    /// Gets a list of all the species published, as well as the Red List category:
    /// </summary>
    /// <param name="page">Page number, each page can contain 10,000 records</param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <remarks>Returns 10,000 records per call, so you need to send multiple requests, based on pages.</remarks>
    public Task<Species?> GetSpecies(int page = 0, CancellationToken token = default)
    {
        return GetJsonAsync<Species>($"species/page/{page}", token);
    }

    /// <summary>
    /// Gets a list of all the species published, as well as the Red List category
    /// </summary>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="page">Page number, each page can contain 10,000 records</param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <remarks>Returns 10,000 records per call, so you need to send multiple requests, based on pages.</remarks>
    public Task<Species?> GetSpecies(string regionIdentifier, int page = 0, CancellationToken token = default)
    {
        return GetJsonAsync<Species>($"species/region/{EscapeDataString(regionIdentifier)}/page/{page}", token);
    }

    /// <summary>
    /// Gets total Species count
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<SpeciesCount?> GetSpeciesCount(CancellationToken token = default)
    {
        return GetJsonAsync<SpeciesCount>("speciescount", token);
    }

    /// <summary>
    /// Gets total Species count
    /// </summary>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<SpeciesCount?> GetSpeciesCount(string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<SpeciesCount>($"speciescount/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets the citation for a given specie assessment
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<Citation>?> GetSpecieCitation(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<Citation>>($"species/citation/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets the citation for a given species assessment
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<Citation>?> GetSpecieCitation(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<Citation>>($"species/citation/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets the citation for a given species assessment
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<Citation>?> GetSpecieCitation(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<Citation>>($"species/citation/id/{specieId}", token);
    }

    /// <summary>
    /// Gets the citation for a given species assessment
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<Citation>?> GetSpecieCitation(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<Citation>>($"species/citation/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of species by category
    /// </summary>
    /// <param name="category">The category</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<SpeciesByCategory?> GetSpeciesByCategory(IuncQueryClassification category, CancellationToken token = default)
    {
        return GetJsonAsync<SpeciesByCategory>($"species/category/{category}", token);
    }

    /// <summary>
    /// Gets information about individual specie
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<IndividualSpecie>?> GetSpecie(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<IndividualSpecie>>($"species/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets information about individual specie
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<IndividualSpecie>?> GetSpecie(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<IndividualSpecie>>($"species/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets information about individual specie
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<IndividualSpecie>?> GetSpecie(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<IndividualSpecie>>($"species/id/{specieId}", token);
    }

    /// <summary>
    /// Gets information about individual specie
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<IndividualSpecie>?> GetSpecie(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<IndividualSpecie>>($"species/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }


    /// <summary>
    /// Gets the narrative information about individual species. Please be aware that the text contains HTML markup in some places for formatting purposes.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieNarrative>?> GetSpecieNarrative(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieNarrative>>($"species/narrative/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets the narrative information about individual species. Please be aware that the text contains HTML markup in some places for formatting purposes.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieNarrative>?> GetSpecieNarrative(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieNarrative>>($"species/narrative/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    
    /// <summary>
    /// Gets the narrative information about individual species. Please be aware that the text contains HTML markup in some places for formatting purposes.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieNarrative>?> GetSpecieNarrative(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieNarrative>>($"species/narrative/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets the narrative information about individual species. Please be aware that the text contains HTML markup in some places for formatting purposes.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieNarrative>?> GetSpecieNarrative(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieNarrative>>($"species/narrative/id/{specieId}", token);
    }

    /// <summary>
    /// Can be used to either gain information about synonyms via an accepted species name, or vice versa i.e. this call tells you if there are synonyms for the species name, or whether it's a synonym of an accepted name
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieSynonym>?> GetSpecieSynonyms(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieSynonym>>($"species/synonym/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of common names per species
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieCommonName>?> GetSpecieCommonNames(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieCommonName>>($"species/common_names/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of countries of occurrence by species name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieNarrative>?> GetSpecieCountries(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieNarrative>>($"species/countries/name/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of countries of occurrence by species name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieCountry>?> GetSpecieCountries(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieCountry>>($"species/countries/name/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of countries of occurrence by species id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieNarrative>?> GetSpecieCountries(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieNarrative>>($"species/countries/id/{specieId}", token);
    }

    /// <summary>
    /// Gets a list of countries of occurrence by species id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieCountry>?> GetSpecieCountries(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieCountry>>($"species/countries/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of historical assessments by specie name (including the current listing).
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieHistory>?> GetSpecieHistory(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieHistory>>($"species/history/name/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of historical assessments by specie name (including the current listing).
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieHistory>?> GetSpecieHistory(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieHistory>>($"species/history/name/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of historical assessments by specie id (including the current listing).
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieHistory>?> GetSpecieHistory(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieHistory>>($"species/history/id/{specieId}", token);
    }

    /// <summary>
    /// Gets a list of historical assessments by specie id (including the current listing).
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieHistory>?> GetSpecieHistory(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieHistory>>($"species/history/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of threats by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieThreat>?> GetSpecieThreats(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieThreat>>($"threats/species/name/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of threats by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieThreat>?> GetSpecieThreats(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieThreat>>($"threats/species/name/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of threats by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IdArrayResult<SpecieThreat>?> GetSpecieThreats(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<IdArrayResult<SpecieThreat>>($"threats/species/id/{specieId}", token);
    }

    /// <summary>
    /// Gets a list of threats by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalIdArrayResult<SpecieThreat>?> GetSpecieThreats(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalIdArrayResult<SpecieThreat>>($"threats/species/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of habitats by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieHabitat>?> GetSpecieHabitats(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieHabitat>>($"habitats/species/name/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of habitats by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieHabitat>?> GetSpecieHabitats(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieHabitat>>($"habitats/species/name/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of habitats by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IdArrayResult<SpecieHabitat>?> GetSpecieHabitats(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<IdArrayResult<SpecieHabitat>>($"habitats/species/id/{specieId}", token);
    }

    /// <summary>
    /// Gets a list of habitats by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalIdArrayResult<SpecieHabitat>?> GetSpecieHabitats(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalIdArrayResult<SpecieHabitat>>($"habitats/species/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of conservation actions by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<SpecieConservationMeasure>?> GetSpecieConservationMeasures(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<SpecieConservationMeasure>>($"measures/species/name/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of conservation actions by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<SpecieConservationMeasure>?> GetSpecieConservationMeasures(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<SpecieConservationMeasure>>($"measures/species/name/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of conservation actions by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IdArrayResult<SpecieConservationMeasure>?> GetSpecieConservationMeasures(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<IdArrayResult<SpecieConservationMeasure>>($"measures/species/id/{specieId}", token);
    }

    /// <summary>
    /// Gets a list of conservation actions by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalIdArrayResult<SpecieConservationMeasure>?> GetSpecieConservationMeasures(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalIdArrayResult<SpecieConservationMeasure>>($"measures/species/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of plant growth forms by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<NamedArrayResult<PlantGrowthForm>?> GetPlantGrowthForms(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<NamedArrayResult<PlantGrowthForm>>($"growth_forms/species/name/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets a list of plant growth forms by specie name.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalNamedArrayResult<PlantGrowthForm>?> GetPlantGrowthForms(string specieName, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalNamedArrayResult<PlantGrowthForm>>($"growth_forms/species/name/{EscapeDataString(specieName)}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of plant growth forms by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IdArrayResult<PlantGrowthForm>?> GetPlantGrowthForms(int specieId, CancellationToken token = default)
    {
        return GetJsonAsync<IdArrayResult<PlantGrowthForm>>($"growth_forms/species/id/{specieId}", token);
    }

    /// <summary>
    /// Gets a list of plant growth forms by specie id.
    /// </summary>
    /// <param name="specieId"></param>
    /// <param name="regionIdentifier">Region identifier, use global if need to fetch all</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<RegionalIdArrayResult<PlantGrowthForm>?> GetPlantGrowthForms(int specieId, string regionIdentifier, CancellationToken token = default)
    {
        return GetJsonAsync<RegionalIdArrayResult<PlantGrowthForm>>($"growth_forms/species/id/{specieId}/region/{EscapeDataString(regionIdentifier)}", token);
    }

    /// <summary>
    /// Gets a list of comprehensive groups.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ArrayResult<ComprehensiveGroup>?> GetComprehensiveGroups(CancellationToken token = default)
    {
        return GetJsonAsync<ArrayResult<ComprehensiveGroup>>("comp-group/list", token);
    }

    /// <summary>
    /// Gets a list of species by comprehensive group.
    /// </summary>
    /// <param name="comprehensiveGroup"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<ArrayResult<CountrySpecie>?> GetSpeciesByComprehensiveGroup(string comprehensiveGroup, CancellationToken token = default)
    {
        return GetJsonAsync<ArrayResult<CountrySpecie>>($"comp-group/getspecies/{EscapeDataString(comprehensiveGroup)}", token);
    }

    /// <summary>
    /// Gets the direct link of the species on the Red List website.<br/>
    /// Please don't use this link in a document/report/publication as the species ID might change, and therefore will invalidate the link if used as such.
    /// </summary>
    /// <param name="specieName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<SpecieWeblink?> GetSpecieWeblink(string specieName, CancellationToken token = default)
    {
        return GetJsonAsync<SpecieWeblink>($"weblink/{EscapeDataString(specieName)}", token);
    }

    /// <summary>
    /// Gets an link which will redirect to the selected specie by name
    /// </summary>
    /// <param name="specieName"></param>
    /// <returns>Url</returns>
    public string GetSpecieRedirectLink(string specieName)
    {
        return GetRawRequestUrl($"website/{EscapeDataString(specieName)}");
    }

    /// <summary>
    /// Gets an link which will redirect to the selected specie by taxonId and/or region
    /// </summary>
    /// <param name="taxonId">The taxonId of the specie</param>
    /// <param name="regionIdentifier">The region identifier, use null or global for the global link</param>
    /// <returns>Url</returns>
    public string GetSpecieRedirectLink(int taxonId, string? regionIdentifier = null)
    {
        return string.IsNullOrWhiteSpace(regionIdentifier)
            ? GetRawRequestUrl($"taxonredirect/{taxonId}")
            : GetRawRequestUrl($"taxonredirect/{taxonId}/{EscapeDataString(regionIdentifier)}");

    }

    #endregion
}