using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

[XmlRoot("coord")]
public class Coordinate : IEquatable<Coordinate>
{
    [XmlElement("lat")]
    public decimal? Latitude { get; set; }

    [XmlElement("lon")]
    public decimal? Longitude { get; set; }

    [JsonPropertyName("coord_source")]
    public string? CoordSource { get; set; }

    [JsonPropertyName("coord_accuracy")]
    public string? CoordAccuracy { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}, {nameof(CoordSource)}: {CoordSource}, {nameof(CoordAccuracy)}: {CoordAccuracy}";
    }

    public bool Equals(Coordinate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Latitude == other.Latitude && Longitude == other.Longitude && CoordSource == other.CoordSource && CoordAccuracy == other.CoordAccuracy;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Coordinate)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude, CoordSource, CoordAccuracy);
    }

    public static bool operator ==(Coordinate? left, Coordinate? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Coordinate? left, Coordinate? right)
    {
        return !Equals(left, right);
    }
}