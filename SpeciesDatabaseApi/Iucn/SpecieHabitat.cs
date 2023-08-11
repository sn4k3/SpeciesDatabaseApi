using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieHabitat : IEquatable<SpecieHabitat>
{
    [JsonPropertyName("code")]
    [XmlElement("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("habitat")]
    [XmlElement("habitat")]
    public string Habitat { get; set; } = string.Empty;

    [JsonPropertyName("suitability")]
    [XmlElement("suitability")]
    public string Suitability { get; set; } = string.Empty;

    [JsonPropertyName("season")]
    [XmlElement("season")]
    public string? Season { get; set; }

    [JsonPropertyName("majorimportance")]
    [XmlElement("majorimportance")]
    public string? MajorImportance { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Code)}: {Code}, {nameof(Habitat)}: {Habitat}, {nameof(Suitability)}: {Suitability}, {nameof(Season)}: {Season}, {nameof(MajorImportance)}: {MajorImportance}";
    }

    public bool Equals(SpecieHabitat? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Code == other.Code && Habitat == other.Habitat && Suitability == other.Suitability && Season == other.Season && MajorImportance == other.MajorImportance;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SpecieHabitat)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Habitat, Suitability, Season, MajorImportance);
    }

    public static bool operator ==(SpecieHabitat? left, SpecieHabitat? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SpecieHabitat? left, SpecieHabitat? right)
    {
        return !Equals(left, right);
    }
}