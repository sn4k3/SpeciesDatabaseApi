using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.MarineSpecies;

public class AphiaAttributeSet : IEquatable<AphiaAttributeSet>
{
    /// <summary>
    /// Unique and persistent identifier within WoRMS. Primary key in the database
    /// </summary>
    [JsonPropertyName("AphiaID")]
    [XmlElement("AphiaID")]
    public int AphiaId { get; set; }

    [JsonPropertyName("Attributes")]
    [XmlElement("Attributes")]
    public Attribute[] Attributes { get; set; } = Array.Empty<Attribute>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(AphiaId)}: {AphiaId}, Attributes: {Attributes.Length}";
    }

    public bool Equals(AphiaAttributeSet? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return AphiaId == other.AphiaId && Attributes.Equals(other.Attributes);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AphiaAttributeSet)obj);
    }

    public static bool operator ==(AphiaAttributeSet? left, AphiaAttributeSet? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AphiaAttributeSet? left, AphiaAttributeSet? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(AphiaId, Attributes);
    }
}