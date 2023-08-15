using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class SpecimenIdentifiers : IEquatable<SpecimenIdentifiers>
{
	[JsonPropertyName("sampleid")]
	public string SampleId { get; set; } = string.Empty;

	[JsonPropertyName("catalognum")]
	public string CatalogNum { get; set; } = string.Empty;

	[JsonPropertyName("fieldnum")]
	public string FieldNum { get; set; } = string.Empty;

	[JsonPropertyName("institution_storing")]
	public string InstitutionStoring { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(SampleId)}: {SampleId}, {nameof(CatalogNum)}: {CatalogNum}, {nameof(FieldNum)}: {FieldNum}, {nameof(InstitutionStoring)}: {InstitutionStoring}";
	}

	public bool Equals(SpecimenIdentifiers? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return SampleId == other.SampleId && CatalogNum == other.CatalogNum && FieldNum == other.FieldNum && InstitutionStoring == other.InstitutionStoring;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpecimenIdentifiers)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(SampleId, CatalogNum, FieldNum, InstitutionStoring);
	}

	public static bool operator ==(SpecimenIdentifiers? left, SpecimenIdentifiers? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpecimenIdentifiers? left, SpecimenIdentifiers? right)
	{
		return !Equals(left, right);
	}
}