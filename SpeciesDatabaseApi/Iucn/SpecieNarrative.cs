using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieNarrative : IEquatable<SpecieNarrative>
{
	[JsonPropertyName("species_id")]
	[XmlElement("species_id")]
	public int SpeciesId { get; set; }

	[JsonPropertyName("taxonomicnotes")]
	[XmlElement("taxonomicnotes")]
	public string? TaxonomicNotes { get; set; }

	[JsonPropertyName("rationale")]
	[XmlElement("rationale")]
	public string? Rationale { get; set; }

	[JsonPropertyName("geographicrange")]
	[XmlElement("geographicrange")]
	public string? GeographicRange { get; set; }

	[JsonPropertyName("population")]
	[XmlElement("population")]
	public string? Population { get; set; }

	[JsonPropertyName("populationtrend")]
	[XmlElement("populationtrend")]
	public string? PopulationTrend { get; set; }

	[JsonPropertyName("habitat")]
	[XmlElement("habitat")]
	public string? Habitat { get; set; }

	[JsonPropertyName("threats")]
	[XmlElement("threats")]
	public string? Threats { get; set; }

	[JsonPropertyName("conservationmeasures")]
	[XmlElement("conservationmeasures")]
	public string? ConservationMeasures { get; set; }

	[JsonPropertyName("usetrade")]
	[XmlElement("usetrade")]
	public string? UseTrade { get; set; }


	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(SpeciesId)}: {SpeciesId}, {nameof(TaxonomicNotes)}: {TaxonomicNotes}, {nameof(Rationale)}: {Rationale}, {nameof(GeographicRange)}: {GeographicRange}, {nameof(Population)}: {Population}, {nameof(PopulationTrend)}: {PopulationTrend}, {nameof(Habitat)}: {Habitat}, {nameof(Threats)}: {Threats}, {nameof(ConservationMeasures)}: {ConservationMeasures}, {nameof(UseTrade)}: {UseTrade}";
	}

	public bool Equals(SpecieNarrative? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return SpeciesId == other.SpeciesId && TaxonomicNotes == other.TaxonomicNotes && Rationale == other.Rationale && GeographicRange == other.GeographicRange && Population == other.Population && PopulationTrend == other.PopulationTrend && Habitat == other.Habitat && Threats == other.Threats && ConservationMeasures == other.ConservationMeasures && UseTrade == other.UseTrade;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpecieNarrative)obj);
	}

	public static bool operator ==(SpecieNarrative? left, SpecieNarrative? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpecieNarrative? left, SpecieNarrative? right)
	{
		return !Equals(left, right);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		var hashCode = new HashCode();
		hashCode.Add(SpeciesId);
		hashCode.Add(TaxonomicNotes);
		hashCode.Add(Rationale);
		hashCode.Add(GeographicRange);
		hashCode.Add(Population);
		hashCode.Add(PopulationTrend);
		hashCode.Add(Habitat);
		hashCode.Add(Threats);
		hashCode.Add(ConservationMeasures);
		hashCode.Add(UseTrade);
		return hashCode.ToHashCode();
	}
}