using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("eu-listing")]
public class EuListing : IEquatable<EuListing>
{
    /// <summary>
    /// The record may cascade from higher taxonomic level, so this value is inherited and the same record may be returned in different contexts.
    /// </summary>
    [JsonPropertyName("id")]
    [XmlElement("id")]
    public int? Id { get; set; }

    [JsonPropertyName("taxon_concept_id")]
    [XmlElement("taxon-concept-id")]
    public int? TaxonConceptId { get; set; }

    /// <summary>
    /// flag indicating whether listing chane is current
    /// </summary>
    [JsonPropertyName("is_current")]
    [XmlElement("is-current")]
    public bool IsCurrent { get; set; }

    /// <summary>
    /// EU annex, one of A, B, C, D [max 255 characters]
    /// </summary>
    [JsonPropertyName("annex")]
    [XmlElement("annex")]
    public string Annex { get; set; } = string.Empty;

    /// <summary>
    /// type of listing change, one of:<br/>
    /// +: inclusion in annex<br/>
    /// -: removal from annex
    /// </summary>
    [JsonPropertyName("change_type")]
    [XmlElement("change-type")]
    public string ChangeType { get; set; } = string.Empty;

    /// <summary>
    /// Date when listing change came into effect, YYYY-MM-DD
    /// </summary>
    [JsonPropertyName("effective_at")]
    [XmlElement("effective-at")]
    public string EffectiveAt { get; set; } = string.Empty;

    /// <summary>
    /// Text of annotation (translated based on locale)
    /// </summary>
    [JsonPropertyName("annotation")]
    [XmlElement("annotation")]
    public string? Annotation { get; set; }

    /// <summary>
    /// Where applicable, CITES party involved in the listing change. See description of geo_entity object below.
    /// </summary>
    [JsonPropertyName("party")]
    [XmlElement("party")]
    public GeoEntity? Party { get; set; }

    /// <summary>
    /// # annotation (plants)
    /// </summary>
    [JsonPropertyName("hash_annotation")]
    [XmlElement("hash-annotation")]
    public Annotation? HashAnnotation { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(TaxonConceptId)}: {TaxonConceptId}, {nameof(IsCurrent)}: {IsCurrent}, {nameof(Annex)}: {Annex}, {nameof(ChangeType)}: {ChangeType}, {nameof(EffectiveAt)}: {EffectiveAt}, {nameof(Annotation)}: {Annotation}, {nameof(Party)}: {Party}, {nameof(HashAnnotation)}: {HashAnnotation}";
    }

    public bool Equals(EuListing? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && TaxonConceptId == other.TaxonConceptId && IsCurrent == other.IsCurrent && Annex == other.Annex && ChangeType == other.ChangeType && EffectiveAt == other.EffectiveAt && Annotation == other.Annotation && Equals(Party, other.Party) && Equals(HashAnnotation, other.HashAnnotation);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EuListing)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(TaxonConceptId);
        hashCode.Add(IsCurrent);
        hashCode.Add(Annex);
        hashCode.Add(ChangeType);
        hashCode.Add(EffectiveAt);
        hashCode.Add(Annotation);
        hashCode.Add(Party);
        hashCode.Add(HashAnnotation);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(EuListing? left, EuListing? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EuListing? left, EuListing? right)
    {
        return !Equals(left, right);
    }
}