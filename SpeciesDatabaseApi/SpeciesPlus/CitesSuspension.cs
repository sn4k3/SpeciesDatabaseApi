using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("cites-suspension")]
public class CitesSuspension : IEquatable<CitesSuspension>
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
    /// Comments [unlimited length]
    /// </summary>
    [JsonPropertyName("notes")]
    [XmlElement("notes")]
    public string? Notes { get; set; }

    /// <summary>
    /// Date when suspension came into effect, YYYY-MM-DD
    /// </summary>
    [JsonPropertyName("start_date")]
    [XmlElement("start-date")]
    public string StartDate { get; set; } = string.Empty;

    /// <summary>
    /// Flag indicating whether suspension is current
    /// </summary>
    [JsonPropertyName("is_current")]
    [XmlElement("is-current")]
    public bool IsCurrent { get; set; }

    /// <summary>
    /// Geographic location to which the suspension applies
    /// </summary>
    [JsonPropertyName("geo_entity")]
    [XmlElement("geo-entity")]
    public GeoEntity? GeoEntity { get; set; }

    /// <summary>
    /// Flag which indcates whether suspension applies to import into the specified geographic location (applies to export by default)
    /// </summary>
    [JsonPropertyName("applies_to_import")]
    [XmlElement("applies-to-import")]
    public bool AppliesToImport { get; set; }

    /// <summary>
    /// Suspension Notification document
    /// </summary>
    [JsonPropertyName("start_notification")]
    [XmlElement("start-notification")]
    public Event? StartNotification { get; set; }


    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(TaxonConceptId)}: {TaxonConceptId}, {nameof(Notes)}: {Notes}, {nameof(StartDate)}: {StartDate}, {nameof(IsCurrent)}: {IsCurrent}, {nameof(GeoEntity)}: {GeoEntity}, {nameof(AppliesToImport)}: {AppliesToImport}, {nameof(StartNotification)}: {StartNotification}";
    }

    public bool Equals(CitesSuspension? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && TaxonConceptId == other.TaxonConceptId && Notes == other.Notes && StartDate == other.StartDate && IsCurrent == other.IsCurrent && Equals(GeoEntity, other.GeoEntity) && AppliesToImport == other.AppliesToImport && Equals(StartNotification, other.StartNotification);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CitesSuspension)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, TaxonConceptId, Notes, StartDate, IsCurrent, GeoEntity, AppliesToImport, StartNotification);
    }

    public static bool operator ==(CitesSuspension? left, CitesSuspension? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CitesSuspension? left, CitesSuspension? right)
    {
        return !Equals(left, right);
    }
}