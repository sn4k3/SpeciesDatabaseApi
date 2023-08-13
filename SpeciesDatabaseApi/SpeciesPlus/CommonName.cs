using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("common-name")]
public class CommonName : IEquatable<CommonName>
{
    [JsonPropertyName("name")]
    [XmlElement("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    [XmlElement("language")]
    public string Language { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Language)}: {Language}";
    }

    public bool Equals(CommonName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Language == other.Language;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CommonName)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Language);
    }

    public static bool operator ==(CommonName? left, CommonName? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CommonName? left, CommonName? right)
    {
        return !Equals(left, right);
    }
}