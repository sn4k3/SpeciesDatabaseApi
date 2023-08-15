using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class Taxon : IEquatable<Taxon>
{
	[JsonPropertyName("taxID")]
	public int TaxId { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(TaxId)}: {TaxId}, {nameof(Name)}: {Name}";
	}

	public bool Equals(Taxon? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return TaxId == other.TaxId && Name == other.Name;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Taxon)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(TaxId, Name);
	}

	public static bool operator ==(Taxon? left, Taxon? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(Taxon? left, Taxon? right)
	{
		return !Equals(left, right);
	}
}