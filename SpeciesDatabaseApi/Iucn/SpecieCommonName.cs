using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieCommonName : IEquatable<SpecieCommonName>
{
	[JsonPropertyName("taxonname")]
	[XmlElement("taxonname")]
	public string TaxonName { get; set; } = string.Empty;

	[JsonPropertyName("primary")]
	[XmlElement("primary")]
	public bool Primary { get; set; }

	[JsonPropertyName("language")]
	[XmlElement("language")]
	public string Language { get; set; } = string.Empty;

	public override string ToString()
	{
		return $"{nameof(TaxonName)}: {TaxonName}, {nameof(Primary)}: {Primary}, {nameof(Language)}: {Language}";
	}

	public bool Equals(SpecieCommonName? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return TaxonName == other.TaxonName && Primary == other.Primary && Language == other.Language;
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpecieCommonName)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(TaxonName, Primary, Language);
	}

	public static bool operator ==(SpecieCommonName? left, SpecieCommonName? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpecieCommonName? left, SpecieCommonName? right)
	{
		return !Equals(left, right);
	}
}