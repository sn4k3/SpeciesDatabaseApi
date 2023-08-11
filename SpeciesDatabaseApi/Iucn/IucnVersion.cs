using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class IucnVersion : IEquatable<IucnVersion>
{
	[JsonPropertyName("version")]
	[XmlElement("version")]
	public string Version { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Version)}: {Version}";
	}

	public bool Equals(IucnVersion? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Version == other.Version;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((IucnVersion)obj);
	}

	public static bool operator ==(IucnVersion? left, IucnVersion? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(IucnVersion? left, IucnVersion? right)
	{
		return !Equals(left, right);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return Version.GetHashCode();
	}
}