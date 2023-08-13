using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("higher-taxa")]
public class HigherTaxa : IEquatable<HigherTaxa>
{
    [JsonPropertyName("kingdom")]
    [XmlElement("kingdom")]
    public string? Kingdom { get; set; }

	[JsonPropertyName("phylum")]
    [XmlElement("phylum")]
    public string? Phylum { get; set; }

	[JsonPropertyName("class")]
    [XmlElement("class")]
    public string? Class { get; set; }

	[JsonPropertyName("order")]
    [XmlElement("order")]
    public string? Order { get; set; }

	[JsonPropertyName("family")]
    [XmlElement("family")]
    public string? Family { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Kingdom)}: {Kingdom}, {nameof(Phylum)}: {Phylum}, {nameof(Class)}: {Class}, {nameof(Order)}: {Order}, {nameof(Family)}: {Family}";
    }

    public bool Equals(HigherTaxa? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Kingdom == other.Kingdom && Phylum == other.Phylum && Class == other.Class && Order == other.Order && Family == other.Family;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((HigherTaxa)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Kingdom, Phylum, Class, Order, Family);
    }

    public static bool operator ==(HigherTaxa? left, HigherTaxa? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(HigherTaxa? left, HigherTaxa? right)
    {
        return !Equals(left, right);
    }
}