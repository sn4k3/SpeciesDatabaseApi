using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TaxonDataRepresentitiveImage : IEquatable<TaxonDataRepresentitiveImage>
{
	[JsonPropertyName("image")]
	public string Image { get; set; } = string.Empty;

	[JsonPropertyName("apectratio")]
	public decimal AspectRatio { get; set; }

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Image)}: {Image}, {nameof(AspectRatio)}: {AspectRatio}";
	}


	public bool Equals(TaxonDataRepresentitiveImage? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Image == other.Image && AspectRatio == other.AspectRatio;
	}
	/// <inheritdoc />

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((TaxonDataRepresentitiveImage)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Image, AspectRatio);
	}

	public static bool operator ==(TaxonDataRepresentitiveImage? left, TaxonDataRepresentitiveImage? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(TaxonDataRepresentitiveImage? left, TaxonDataRepresentitiveImage? right)
	{
		return !Equals(left, right);
	}
}