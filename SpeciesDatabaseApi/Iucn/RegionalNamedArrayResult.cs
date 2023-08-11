using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System;

namespace SpeciesDatabaseApi.Iucn;

public class RegionalNamedArrayResult<T> : NamedArrayResult<T>, IEquatable<RegionalNamedArrayResult<T>>
{
    [JsonPropertyName("region_identifier")]
    [XmlElement("region_identifier")]
    public string RegionIdentifier { get; set; } = "global";

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(RegionIdentifier)}: {RegionIdentifier}, {base.ToString()}";
    }

    public bool Equals(RegionalNamedArrayResult<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && RegionIdentifier == other.RegionIdentifier;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((RegionalNamedArrayResult<T>)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), RegionIdentifier);
    }

    public static bool operator ==(RegionalNamedArrayResult<T>? left, RegionalNamedArrayResult<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(RegionalNamedArrayResult<T>? left, RegionalNamedArrayResult<T>? right)
    {
        return !Equals(left, right);
    }
}