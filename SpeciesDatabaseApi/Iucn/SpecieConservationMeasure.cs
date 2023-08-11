using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieConservationMeasure : IEquatable<SpecieConservationMeasure>
{
    [JsonPropertyName("code")]
    [XmlElement("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    [XmlElement("title")]
    public string Title { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Code)}: {Code}, {nameof(Title)}: {Title}";
    }

    public bool Equals(SpecieConservationMeasure? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Code == other.Code && Title == other.Title;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SpecieConservationMeasure)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Title);
    }

    public static bool operator ==(SpecieConservationMeasure? left, SpecieConservationMeasure? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SpecieConservationMeasure? left, SpecieConservationMeasure? right)
    {
        return !Equals(left, right);
    }
}