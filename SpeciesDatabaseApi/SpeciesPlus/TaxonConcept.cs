using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("taxon-concept")]
public class TaxonConcept : IEquatable<TaxonConcept>
{
	/// <summary>
	/// Unique identifier of a taxon concept
	/// </summary>
	[JsonPropertyName("id")]
	[XmlElement("id")]
	public int Id { get; set; }

    /// <summary>
    /// Scientific name [max 255 characters]
    /// </summary>
    [JsonPropertyName("full_name")]
    [XmlElement("full-name")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Author and year (parentheses where applicable) [max 255 characters]
    /// </summary>
    [JsonPropertyName("author_year")]
    [XmlElement("author-year")]
    public string AuthorYear { get; set; } = string.Empty;

    /// <summary>
    /// Taxon rank
    /// </summary>
    [JsonPropertyName("rank")]
    [XmlElement("rank")]
    public TaxaRankEnum Rank { get; set; }

    /// <summary>
    /// A for accepted names, S for synonyms (both types of names are taxon concepts in Species+) 
    /// </summary>
    [JsonPropertyName("name_status")]
    [XmlElement("name-status")]
    public char NameStatus { get; set; } = char.MinValue;

    /// <summary>
    /// Timestamp of last update to the taxon concept in Species+
    /// </summary>
    [JsonPropertyName("updated_at")]
    [XmlElement("updated-at")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// If false, taxon concept has been deleted
    /// </summary>
    [JsonPropertyName("active")]
    [XmlElement("active")]
    public bool Active { get; set; }

	/// <summary>
	/// List of synonyms (only for accepted names, i.e. name_status == A)
	/// </summary>
	[JsonPropertyName("synonyms")]
    [XmlElement("synonyms")]
    public Synonym[] Synonyms { get; set; } = Array.Empty<Synonym>();

	/// <summary>
	/// Object that gives scientific names of ancestors in the taxonomic tree (only for active accepted names)
	/// </summary>
	[JsonPropertyName("higher_taxa")]
    [XmlElement("higher-taxa")]
    public HigherTaxa? HigherTaxa { get; set; }

	/// <summary>
	/// List of common names (with language given by ISO 639-1 code; only for accepted names)
	/// </summary>
	[JsonPropertyName("common_names")]
    [XmlElement("common-names")]
    public CommonName[] CommonNames { get; set; } = Array.Empty<CommonName>();

	/// <summary>
	/// List of current EU listings with annotations (there will be more than one element in this list in case of split listings; only for accepted names)
	/// </summary>
	[JsonPropertyName("cites_listings")]
    [XmlElement("cites-listings")]
    public TaxonCitesListing[] CitesListings { get; set; } = Array.Empty<TaxonCitesListing>();

	/// <summary>
	/// Value of current CITES listing (as per CITES Checklist). When taxon concept is removed from appendices this becomes NC. When taxon is split listed it becomes a concatenation of appendix symbols, e.g. I/II/NC
	/// </summary>
	[JsonPropertyName("cites_listing")]
	[XmlElement("cites-listing")]
	public string? CitesListing { get; set; }

	/// <summary>
	/// List of current EU listings with annotations (there will be more than one element in this list in case of split listings; only for accepted names)
	/// </summary>
	public TaxonEuListing[] EuListings { get; set; } = Array.Empty<TaxonEuListing>();

	/// <summary>
	/// Value of current EU listing. When taxon concept is removed from annexes this becomes NC. When taxon is split listed it becomes a concatenation of annex symbols, e.g. A/B/NC (only for accepted names)
	/// </summary>
	[JsonPropertyName("eu_listing")]
	[XmlElement("eu-listing")]
	public string? EuListing { get; set; }

	/// <summary>
	/// List of accepted names (only for synonyms, i.e. name_status == S)
	/// </summary>
	[JsonPropertyName("accepted_names")]
	[XmlElement("accepted-names")]
	public Synonym[] AcceptedNames { get; set; } = Array.Empty<Synonym>();

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Id)}: {Id}, {nameof(FullName)}: {FullName}, {nameof(AuthorYear)}: {AuthorYear}, {nameof(Rank)}: {Rank}, {nameof(NameStatus)}: {NameStatus}, {nameof(UpdatedAt)}: {UpdatedAt}, {nameof(Active)}: {Active}, {nameof(Synonyms)}: {Synonyms.Length}, {nameof(HigherTaxa)}: {HigherTaxa}, {nameof(CommonNames)}: {CommonNames.Length}, {nameof(CitesListings)}: {CitesListings.Length}, {nameof(CitesListing)}: {CitesListing}, {nameof(EuListings)}: {EuListings.Length}, {nameof(EuListing)}: {EuListing}, {nameof(AcceptedNames)}: {AcceptedNames.Length}";
	}

	public bool Equals(TaxonConcept? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Id == other.Id && FullName == other.FullName && AuthorYear == other.AuthorYear && Rank == other.Rank && NameStatus == other.NameStatus && UpdatedAt.Equals(other.UpdatedAt) && Active == other.Active && Synonyms.Equals(other.Synonyms) && HigherTaxa.Equals(other.HigherTaxa) && CommonNames.Equals(other.CommonNames) && CitesListings.Equals(other.CitesListings) && CitesListing == other.CitesListing && EuListings.Equals(other.EuListings) && EuListing == other.EuListing && AcceptedNames.Equals(other.AcceptedNames);
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((TaxonConcept)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		var hashCode = new HashCode();
		hashCode.Add(Id);
		hashCode.Add(FullName);
		hashCode.Add(AuthorYear);
		hashCode.Add((int)Rank);
		hashCode.Add(NameStatus);
		hashCode.Add(UpdatedAt);
		hashCode.Add(Active);
		hashCode.Add(Synonyms);
		hashCode.Add(HigherTaxa);
		hashCode.Add(CommonNames);
		hashCode.Add(CitesListings);
		hashCode.Add(CitesListing);
		hashCode.Add(EuListings);
		hashCode.Add(EuListing);
		hashCode.Add(AcceptedNames);
		return hashCode.ToHashCode();
	}

	public static bool operator ==(TaxonConcept? left, TaxonConcept? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(TaxonConcept? left, TaxonConcept? right)
	{
		return !Equals(left, right);
	}
}