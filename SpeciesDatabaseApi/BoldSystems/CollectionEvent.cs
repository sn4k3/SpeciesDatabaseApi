using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class CollectionEvent : IEquatable<CollectionEvent>
{
    [JsonPropertyName("collectors")]
    public string Collectors { get; set; } = string.Empty;

    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    [JsonPropertyName("province_state")]
    public string? ProvinceState { get; set; }

    [JsonPropertyName("sector")]
    public string? Sector { get; set; }

    [JsonPropertyName("exactsite")]
    public string? ExactSite { get; set; }

    [JsonPropertyName("coordinates")]
    public Coordinate? Coordinates { get; set; }

    [JsonPropertyName("elev")]
    public string? Elev { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Collectors)}: {Collectors}, {nameof(Country)}: {Country}, {nameof(ProvinceState)}: {ProvinceState}, {nameof(Sector)}: {Sector}, {nameof(ExactSite)}: {ExactSite}, {nameof(Coordinates)}: {Coordinates}, {nameof(Elev)}: {Elev}";
    }

    public bool Equals(CollectionEvent? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Collectors == other.Collectors && Country == other.Country && ProvinceState == other.ProvinceState && Sector == other.Sector && ExactSite == other.ExactSite && Equals(Coordinates, other.Coordinates) && Elev == other.Elev;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CollectionEvent)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Collectors, Country, ProvinceState, Sector, ExactSite, Coordinates, Elev);
    }

    public static bool operator ==(CollectionEvent? left, CollectionEvent? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CollectionEvent? left, CollectionEvent? right)
    {
        return !Equals(left, right);
    }
}