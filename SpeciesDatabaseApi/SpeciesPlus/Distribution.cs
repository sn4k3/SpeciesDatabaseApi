using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;


/// <summary>
/// Distribution for a given taxon concept
/// </summary>
[XmlRoot("distribution")]
public class Distribution : IEquatable<Distribution>
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
    public string? Name { get; set; }

    /// <summary>
    /// One of COUNTRY or TERRITORY
    /// </summary>
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [XmlElement("type")]
    public DistributionsTypeEnum Type { get; set; } = DistributionsTypeEnum.COUNTRY;

    /// <summary>
    /// Name of country / territory (translated based on locale) [max 255 characters]
    /// </summary>
    [JsonPropertyName("tags")]
    [XmlElement("tags")]
    public string[] Tags { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Array of citations [strings of unlimited length]
    /// </summary>
    [JsonPropertyName("references")]
    [XmlElement("references")]
    public string[] References { get; set; } = Array.Empty<string>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(IsoCode2)}: {IsoCode2}, {nameof(Name)}: {Name}, {nameof(Type)}: {Type}, {nameof(Tags)}: {Tags}, {nameof(References)}: {References}";
    }

    public bool Equals(Distribution? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IsoCode2 == other.IsoCode2 && Name == other.Name && Type == other.Type && Tags.Equals(other.Tags) && References.Equals(other.References);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Distribution)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(IsoCode2, Name, (int)Type, Tags, References);
    }

    public static bool operator ==(Distribution? left, Distribution? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Distribution? left, Distribution? right)
    {
        return !Equals(left, right);
    }
}