using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System;

namespace SpeciesDatabaseApi.Iucn;

public class NamedArrayResult<T> : ArrayResult<T>, IEquatable<NamedArrayResult<T>>
{
	[JsonPropertyName("name")]
	[XmlElement("name")]
	public string Name { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Name)}: {Name}, {base.ToString()}";
	}

	/// <inheritdoc />
	public bool Equals(NamedArrayResult<T>? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return base.Equals(other) && Name == other.Name;
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((NamedArrayResult<T>)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), Name);
	}

	public static bool operator ==(NamedArrayResult<T>? left, NamedArrayResult<T>? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(NamedArrayResult<T>? left, NamedArrayResult<T>? right)
	{
		return !Equals(left, right);
	}
}