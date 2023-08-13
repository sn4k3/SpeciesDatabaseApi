using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

/// <summary>
/// References for a given taxon concept
/// </summary>
[XmlRoot("taxon-reference")]
public class Reference : IEquatable<Reference>
{
    [JsonPropertyName("citation")]
    [XmlElement("citation")]
    public string Citation { get; set; } = string.Empty;

    [JsonPropertyName("is_standard")]
    [XmlElement("is-standard")]
    public bool IsStandard { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Citation)}: {Citation}, {nameof(IsStandard)}: {IsStandard}";
    }

    public bool Equals(Reference? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Citation == other.Citation && IsStandard == other.IsStandard;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Reference)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Citation, IsStandard);
    }

    public static bool operator ==(Reference? left, Reference? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Reference? left, Reference? right)
    {
        return !Equals(left, right);
    }
}