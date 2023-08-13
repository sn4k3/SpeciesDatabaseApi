using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("annotation")]
public class Annotation : IEquatable<Annotation>
{
    /// <summary>
    /// Symbol of annotation [max 255 characters]
    /// </summary>
    [JsonPropertyName("symbol")]
    [XmlElement("symbol")]
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Text of annotation (translated based on locale) [unlimited length]
    /// </summary>
    [JsonPropertyName("note")]
    [XmlElement("note")]
    public string Note { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Symbol)}: {Symbol}, {nameof(Note)}: {Note}";
    }

    public bool Equals(Annotation? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Symbol == other.Symbol && Note == other.Note;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Annotation)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Symbol, Note);
    }

    public static bool operator ==(Annotation? left, Annotation? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Annotation? left, Annotation? right)
    {
        return !Equals(left, right);
    }
}