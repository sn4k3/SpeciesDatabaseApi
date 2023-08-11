using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class Species : ArrayResult<Specie>, IEquatable<Species>
{
    [JsonPropertyName("region_identifier")]
    [XmlElement("region_identifier")]
    public string RegionIdentifier { get; set; } = "global";

    [JsonPropertyName("page")]
    [XmlElement("page")]
    public int Page { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(RegionIdentifier)}: {RegionIdentifier}, {nameof(Page)}: {Page}";
    }

    public bool Equals(Species? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && RegionIdentifier == other.RegionIdentifier && Page == other.Page;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Species)obj);
    }

    public static bool operator ==(Species? left, Species? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Species? left, Species? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), RegionIdentifier, Page);
    }
}