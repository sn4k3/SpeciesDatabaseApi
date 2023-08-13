using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("eu-decision")]
public class EuDecision : IEquatable<EuDecision>
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
    /// Date when decision came into effect, YYYY-MM-DD
    /// </summary>
    [JsonPropertyName("start_date")]
    [XmlElement("start-date")]
    public string StartDate { get; set; } = string.Empty;

    /// <summary>
    /// Flag indicating whether decision is current
    /// </summary>
    [JsonPropertyName("is_current")]
    [XmlElement("is-current")]
    public bool IsCurrent { get; set; }

    /// <summary>
    /// Term to which decision applies
    /// </summary>
    [JsonPropertyName("eu_decision_type")]
    [XmlElement("eu-decision-type")]
    public EuDecisionType? DecisionType { get; set; }

    /// <summary>
    /// Geographic location to which the suspension applies
    /// </summary>
    [JsonPropertyName("geo_entity")]
    [XmlElement("geo-entity")]
    public GeoEntity? GeoEntity { get; set; }

    /// <summary>
    /// Event that started the suspension
    /// </summary>
    [JsonPropertyName("start_event")]
    [XmlElement("start-event")]
    public Event? StartEvent { get; set; }

    /// <summary>
    /// Event that ended the suspension
    /// </summary>
    [JsonPropertyName("end_event")]
    [XmlElement("end-event")]
    public Event? EndEvent { get; set; }

    /// <summary>
    /// Source to which decision applies.
    /// </summary>
    [JsonPropertyName("source")]
    [XmlElement("source")]
    public TradeCode? Source { get; set; }

    /// <summary>
    /// Term to which decision applies
    /// </summary>
    [JsonPropertyName("term")]
    [XmlElement("term")]
    public TradeCode? Term { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(TaxonConceptId)}: {TaxonConceptId}, {nameof(Notes)}: {Notes}, {nameof(StartDate)}: {StartDate}, {nameof(IsCurrent)}: {IsCurrent}, {nameof(DecisionType)}: {DecisionType}, {nameof(GeoEntity)}: {GeoEntity}, {nameof(StartEvent)}: {StartEvent}, {nameof(EndEvent)}: {EndEvent}, {nameof(Source)}: {Source}, {nameof(Term)}: {Term}";
    }

    public bool Equals(EuDecision? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && TaxonConceptId == other.TaxonConceptId && Notes == other.Notes && StartDate == other.StartDate && IsCurrent == other.IsCurrent && Equals(DecisionType, other.DecisionType) && Equals(GeoEntity, other.GeoEntity) && Equals(StartEvent, other.StartEvent) && Equals(EndEvent, other.EndEvent) && Equals(Source, other.Source) && Equals(Term, other.Term);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EuDecision)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Id);
        hashCode.Add(TaxonConceptId);
        hashCode.Add(Notes);
        hashCode.Add(StartDate);
        hashCode.Add(IsCurrent);
        hashCode.Add(DecisionType);
        hashCode.Add(GeoEntity);
        hashCode.Add(StartEvent);
        hashCode.Add(EndEvent);
        hashCode.Add(Source);
        hashCode.Add(Term);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(EuDecision? left, EuDecision? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EuDecision? left, EuDecision? right)
    {
        return !Equals(left, right);
    }
}