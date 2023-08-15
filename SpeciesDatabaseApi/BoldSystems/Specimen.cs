using System;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

[XmlRoot("specimen")]
public class Specimen : IEquatable<Specimen>
{
	[XmlElement("url")]
	public string? Url { get; set; }

	[XmlElement("collectionlocation")]
	CollectionLocation? CollectionLocation { get; set; }

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Url)}: {Url}, {nameof(CollectionLocation)}: {CollectionLocation}";
	}

	public bool Equals(Specimen? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Equals(Url, other.Url) && Equals(CollectionLocation, other.CollectionLocation);
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Specimen)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Url, CollectionLocation);
	}

	public static bool operator ==(Specimen? left, Specimen? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(Specimen? left, Specimen? right)
	{
		return !Equals(left, right);
	}
}