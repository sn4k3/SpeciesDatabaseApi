using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("eu-listing")]
public class TaxonEuListing : IEquatable<TaxonEuListing>
{
    /// <summary>
    /// The record may cascade from higher taxonomic level, so this value is inherited and the same record may be returned in different contexts.
    /// </summary>
    [JsonPropertyName("id")]
    [XmlElement("id")]
    public int? Id { get; set; }
    /// <summary>
    /// EU annex, one of A, B, C, D [max 255 characters]
    /// </summary>
    [JsonPropertyName("annex")]
    [XmlElement("annex")]
    public string Annex { get; set; } = string.Empty;

    /// <summary>
    /// Text of annotation (translated based on locale)
    /// </summary>
    [JsonPropertyName("annotation")]
    [XmlElement("annotation")]
    public string? Annotation { get; set; }

    /// <summary>
    /// # annotation (plants)
    /// </summary>
    [JsonPropertyName("hash_annotation")]
    [XmlElement("hash-annotation")]
    public Annotation? HashAnnotation { get; set; }

	/// <summary>
	/// Date when listing change came into effect, YYYY-MM-DD
	/// </summary>
	[JsonPropertyName("effective_at")]
    [XmlElement("effective-at")]
    public string EffectiveAt { get; set; } = string.Empty;
    
    /// <summary>
    /// Where applicable, CITES party involved in the listing change. See description of geo_entity object below.
    /// </summary>
    [JsonPropertyName("party")]
    [XmlElement("party")]
    public GeoEntity? Party { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
	    return $"{nameof(Id)}: {Id}, {nameof(Annex)}: {Annex}, {nameof(Annotation)}: {Annotation}, {nameof(HashAnnotation)}: {HashAnnotation}, {nameof(EffectiveAt)}: {EffectiveAt}, {nameof(Party)}: {Party}";
    }

    public bool Equals(TaxonEuListing? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return Id == other.Id && Annex == other.Annex && Annotation == other.Annotation && Equals(HashAnnotation, other.HashAnnotation) && EffectiveAt == other.EffectiveAt && Equals(Party, other.Party);
    }

    public static bool operator ==(TaxonEuListing? left, TaxonEuListing? right)
    {
	    return Equals(left, right);
    }

    public static bool operator !=(TaxonEuListing? left, TaxonEuListing? right)
    {
	    return !Equals(left, right);
    }

    public override bool Equals(object? obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != this.GetType()) return false;
	    return Equals((TaxonEuListing)obj);
    }

    public override int GetHashCode()
    {
	    return HashCode.Combine(Id, Annex, Annotation, HashAnnotation, EffectiveAt, Party);
    }
}