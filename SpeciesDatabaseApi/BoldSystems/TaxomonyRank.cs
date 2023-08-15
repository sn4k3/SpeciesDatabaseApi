using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TaxomonyRank : IEquatable<TaxomonyRank>
{
	[JsonPropertyName("taxon")]
	public Taxon Taxon { get; set; } = new();

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Taxon)}: {Taxon}";
	}

	public bool Equals(TaxomonyRank? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Taxon.Equals(other.Taxon);
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((TaxomonyRank)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return Taxon.GetHashCode();
	}

	public static bool operator ==(TaxomonyRank? left, TaxomonyRank? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(TaxomonyRank? left, TaxomonyRank? right)
	{
		return !Equals(left, right);
	}
}