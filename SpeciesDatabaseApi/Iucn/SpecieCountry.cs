using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieCountry : IEquatable<SpecieCountry>
{
	[JsonPropertyName("code")]
	[XmlElement("code")]
	public string Code { get; set; } = string.Empty;

	[JsonPropertyName("country")]
	[XmlElement("country")]
	public string Country { get; set; } = string.Empty;

	[JsonPropertyName("presence")]
	[XmlElement("presence")]
	public string Presence { get; set; } = string.Empty;

	[JsonPropertyName("origin")]
	[XmlElement("origin")]
	public string Origin { get; set; } = string.Empty;

	[JsonPropertyName("distribution_code")]
	[XmlElement("distribution_code")]
	public string DistributionCode { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Code)}: {Code}, {nameof(Country)}: {Country}, {nameof(Presence)}: {Presence}, {nameof(Origin)}: {Origin}, {nameof(DistributionCode)}: {DistributionCode}";
	}

	public bool Equals(SpecieCountry? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Code == other.Code && Country == other.Country && Presence == other.Presence && Origin == other.Origin && DistributionCode == other.DistributionCode;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpecieCountry)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Code, Country, Presence, Origin, DistributionCode);
	}

	public static bool operator ==(SpecieCountry? left, SpecieCountry? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpecieCountry? left, SpecieCountry? right)
	{
		return !Equals(left, right);
	}
}