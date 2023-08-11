using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieWeblink : IEquatable<SpecieWeblink>
{
	[JsonPropertyName("rlurl")]
	[XmlElement("rlurl")]
	public Uri RlUrl { get; set; } = null!;

	[JsonPropertyName("species")]
	[XmlElement("species")]
	public string Species { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(RlUrl)}: {RlUrl}, {nameof(Species)}: {Species}";
	}

	public bool Equals(SpecieWeblink? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return RlUrl.Equals(other.RlUrl) && Species == other.Species;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpecieWeblink)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(RlUrl, Species);
	}

	public static bool operator ==(SpecieWeblink? left, SpecieWeblink? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpecieWeblink? left, SpecieWeblink? right)
	{
		return !Equals(left, right);
	}
}