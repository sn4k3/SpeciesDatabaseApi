using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieHistory : IEquatable<SpecieHistory>
{
	[JsonPropertyName("year")]
	[XmlElement("year")]
	public int Year { get; set; }

	[JsonPropertyName("assess_year")]
	[XmlElement("assess_year")]
	public int AssessYear { get; set; }

	[JsonPropertyName("code")]
	[XmlElement("code")]
	public string Code { get; set; } = string.Empty;

	[JsonPropertyName("category")]
	[XmlElement("category")]
	public string Category { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Year)}: {Year}, {nameof(AssessYear)}: {AssessYear}, {nameof(Code)}: {Code}, {nameof(Category)}: {Category}";
	}

	public bool Equals(SpecieHistory? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Year == other.Year && AssessYear == other.AssessYear && Code == other.Code && Category == other.Category;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpecieHistory)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Year, AssessYear, Code, Category);
	}

	public static bool operator ==(SpecieHistory? left, SpecieHistory? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpecieHistory? left, SpecieHistory? right)
	{
		return !Equals(left, right);
	}
}