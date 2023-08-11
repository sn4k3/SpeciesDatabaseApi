using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpeciesByCategory : ArrayResult<CategorySpecie>, IEquatable<SpeciesByCategory>
{
	[JsonPropertyName("category")]
	[XmlElement("category")]
	public string Category { get; set; } = "NE";

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Category)}: {Category}, {base.ToString()}";
	}

	public bool Equals(SpeciesByCategory? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return base.Equals(other) && Category == other.Category;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpeciesByCategory)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), Category);
	}

	public static bool operator ==(SpeciesByCategory? left, SpeciesByCategory? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpeciesByCategory? left, SpeciesByCategory? right)
	{
		return !Equals(left, right);
	}
}