using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class CountryItem : IEquatable<CountryItem>
{
	[JsonPropertyName("isocode")]
	[XmlElement("isocode")]
	public string IsoCode { get; set; } = string.Empty;

	[JsonPropertyName("country")]
	[XmlElement("country")]
	public string Country { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(IsoCode)}: {IsoCode}, {nameof(Country)}: {Country}";
	}

	public bool Equals(CountryItem? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return IsoCode == other.IsoCode && Country == other.Country;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((CountryItem)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(IsoCode, Country);
	}

	public static bool operator ==(CountryItem? left, CountryItem? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(CountryItem? left, CountryItem? right)
	{
		return !Equals(left, right);
	}
}