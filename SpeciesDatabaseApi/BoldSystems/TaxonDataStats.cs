using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TaxonDataStats : IEquatable<TaxonDataStats>
{
    [JsonPropertyName("publicspecies")]
    public int PublicSpecies { get; set; }

    [JsonPropertyName("publicbins")]
    public int PublicBins { get; set; }

    [JsonPropertyName("publicmarkersequences")]
    public Dictionary<string, int>? PublicMarkerSequences { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(PublicSpecies)}: {PublicSpecies}, {nameof(PublicBins)}: {PublicBins}, {nameof(PublicMarkerSequences)}: {PublicMarkerSequences}";
    }

    public bool Equals(TaxonDataStats? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return PublicSpecies == other.PublicSpecies && PublicBins == other.PublicBins && Equals(PublicMarkerSequences, other.PublicMarkerSequences);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaxonDataStats)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(PublicSpecies, PublicBins, PublicMarkerSequences);
    }

    public static bool operator ==(TaxonDataStats? left, TaxonDataStats? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaxonDataStats? left, TaxonDataStats? right)
    {
        return !Equals(left, right);
    }
}