using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class Stats : IEquatable<Stats>
{
    [JsonPropertyName("total_records")]
    public int TotalRecords { get; set; }

    [JsonPropertyName("records_with_species_name")]
    public int RecordsWithSpeciesName { get; set; }

    [JsonPropertyName("bins")]
    public Stat Bins { get; set; } = new();

    [JsonPropertyName("countries")]
	public Stat Countries { get; set; } = new();

    [JsonPropertyName("depositories")]
	public Stat Depositories { get; set; } = new();

    [JsonPropertyName("order")]
	public Stat Order { get; set; } = new();

    [JsonPropertyName("family")]
	public Stat Family { get; set; } = new();

    [JsonPropertyName("genus")]
	public Stat Genus { get; set; } = new();

    [JsonPropertyName("species")]
	public Stat Species { get; set; } = new();

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(TotalRecords)}: {TotalRecords}, {nameof(RecordsWithSpeciesName)}: {RecordsWithSpeciesName}, {nameof(Bins)}: {Bins}, {nameof(Countries)}: {Countries}, {nameof(Depositories)}: {Depositories}, {nameof(Order)}: {Order}, {nameof(Family)}: {Family}, {nameof(Genus)}: {Genus}, {nameof(Species)}: {Species}";
	}

	public bool Equals(Stats? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return TotalRecords == other.TotalRecords && RecordsWithSpeciesName == other.RecordsWithSpeciesName && Bins.Equals(other.Bins) && Countries.Equals(other.Countries) && Depositories.Equals(other.Depositories) && Order.Equals(other.Order) && Family.Equals(other.Family) && Genus.Equals(other.Genus) && Species.Equals(other.Species);
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Stats)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		var hashCode = new HashCode();
		hashCode.Add(TotalRecords);
		hashCode.Add(RecordsWithSpeciesName);
		hashCode.Add(Bins);
		hashCode.Add(Countries);
		hashCode.Add(Depositories);
		hashCode.Add(Order);
		hashCode.Add(Family);
		hashCode.Add(Genus);
		hashCode.Add(Species);
		return hashCode.ToHashCode();
	}

	public static bool operator ==(Stats? left, Stats? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(Stats? left, Stats? right)
	{
		return !Equals(left, right);
	}
}