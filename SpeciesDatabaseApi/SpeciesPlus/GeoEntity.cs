using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("geo-entity")]
public class GeoEntity : IEquatable<GeoEntity>
{
    /// <summary>
    /// ISO 3166-1 alpha-2 [max 255 characters]
    /// </summary>
    [JsonPropertyName("iso_code2")]
    [XmlElement("iso-code2")]
    public string IsoCode2 { get; set; } = string.Empty;

    /// <summary>
    /// Name of country / territory (translated based on locale) [max 255 characters]
    /// </summary>
    [JsonPropertyName("name")]
    [XmlElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// One of COUNTRY or TERRITORY [max 255 characters]
    /// </summary>
    [JsonPropertyName("type")]
    [XmlElement("type")]
    public string? Type { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(IsoCode2)}: {IsoCode2}, {nameof(Name)}: {Name}, {nameof(Type)}: {Type}";
    }

    public bool Equals(GeoEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsoCode2 == other.IsoCode2 && Name == other.Name && Type == other.Type;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GeoEntity)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(IsoCode2, Name, Type);
    }

    public static bool operator ==(GeoEntity? left, GeoEntity? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GeoEntity? left, GeoEntity? right)
    {
        return !Equals(left, right);
    }
}