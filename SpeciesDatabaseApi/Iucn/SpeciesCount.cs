using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpeciesCount : IEquatable<SpeciesCount>
{
    [JsonPropertyName("count")]
    [XmlElement("count")]
    public int Count { get; set; }

    [JsonPropertyName("note1")]
    [XmlElement("note1")]
    public string Note1 { get; set; } = string.Empty;

    [JsonPropertyName("note2")]
    [XmlElement("note2")]
    public string Note2 { get; set; } = string.Empty;

    [JsonPropertyName("speciescount")]
    [XmlElement("speciescount")]
    public int Species { get; set; }

    [JsonPropertyName("region_identifier")]
    [XmlElement("region_identifier")]
    public string RegionIdentifier { get; set; } = "global";

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Count)}: {Count}, {nameof(Note1)}: {Note1}, {nameof(Note2)}: {Note2}, {nameof(Species)}: {Species}, {nameof(RegionIdentifier)}: {RegionIdentifier}";
    }

    public bool Equals(SpeciesCount? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Count == other.Count && Note1 == other.Note1 && Note2 == other.Note2 && Species == other.Species && RegionIdentifier == other.RegionIdentifier;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SpeciesCount)obj);
    }

    public static bool operator ==(SpeciesCount? left, SpeciesCount? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SpeciesCount? left, SpeciesCount? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Count, Note1, Note2, Species, RegionIdentifier);
    }
}