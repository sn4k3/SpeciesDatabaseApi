using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("cites-quota")]
public class CitesQuota : IEquatable<CitesQuota>
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

    [JsonPropertyName("quota")]
    [XmlElement("quota")]
    public decimal Quota { get; set; }

    /// <summary>
    /// Date when quota was published, YYYY-MM-DD
    /// </summary>
    [JsonPropertyName("publication_date")]
    [XmlElement("publication-date")]
    public string PublicationDate { get; set; } = string.Empty;

    /// <summary>
    /// Comments [unlimited length]
    /// </summary>
    [JsonPropertyName("notes")]
    [XmlElement("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// URL of original document
    /// </summary>
    [JsonPropertyName("url")]
    [XmlElement("url")]
    public Uri? Url { get; set; }

    /// <summary>
    /// Flag indicating whether quota is current
    /// </summary>
    [JsonPropertyName("is_current")]
    [XmlElement("is-current")]
    public bool IsCurrent { get; set; }

    /// <summary>
    /// Quota unit
    /// </summary>
    [JsonPropertyName("unit")]
    [XmlElement("unit")]
    public TradeCode? Unit { get; set; }

    /// <summary>
    /// Geographic location to which the quota applies
    /// </summary>
    [JsonPropertyName("geo_entity")]
    [XmlElement("geo-entity")]
    public GeoEntity? GeoEntity { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(TaxonConceptId)}: {TaxonConceptId}, {nameof(Quota)}: {Quota}, {nameof(PublicationDate)}: {PublicationDate}, {nameof(Notes)}: {Notes}, {nameof(Url)}: {Url}, {nameof(IsCurrent)}: {IsCurrent}, {nameof(Unit)}: {Unit}, {nameof(GeoEntity)}: {GeoEntity}";
    }

    public bool Equals(CitesQuota? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && TaxonConceptId == other.TaxonConceptId && Quota == other.Quota && PublicationDate == other.PublicationDate && Notes == other.Notes && Equals(Url, other.Url) && IsCurrent == other.IsCurrent && Equals(Unit, other.Unit) && Equals(GeoEntity, other.GeoEntity);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CitesQuota)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(TaxonConceptId);
        hashCode.Add(Quota);
        hashCode.Add(PublicationDate);
        hashCode.Add(Notes);
        hashCode.Add(Url);
        hashCode.Add(IsCurrent);
        hashCode.Add(Unit);
        hashCode.Add(GeoEntity);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(CitesQuota? left, CitesQuota? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CitesQuota? left, CitesQuota? right)
    {
        return !Equals(left, right);
    }
}