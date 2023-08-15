using System;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

[XmlRoot("collectionlocation")]
public class CollectionLocation : IEquatable<CollectionLocation>
{
    [XmlElement("country")]
    public string? Country { get; set; }

    [XmlElement("coord")]
    public Coordinate? Coordinate { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Country)}: {Country}, {nameof(Coordinate)}: {Coordinate}";
    }

    public bool Equals(CollectionLocation? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Country == other.Country && Equals(Coordinate, other.Coordinate);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CollectionLocation)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Country, Coordinate);
    }

    public static bool operator ==(CollectionLocation? left, CollectionLocation? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CollectionLocation? left, CollectionLocation? right)
    {
        return !Equals(left, right);
    }
}