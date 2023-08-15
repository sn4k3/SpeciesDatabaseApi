using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TaxonSearch : IEquatable<TaxonSearch>
{
    [JsonPropertyName("top_matched_names")]
    public TaxonData[] TopMatchedNames { get; set; } = Array.Empty<TaxonData>();

    [JsonPropertyName("total_matched_names")]
    public int TotalMatchedNames { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(TopMatchedNames)}: {TopMatchedNames}, {nameof(TotalMatchedNames)}: {TotalMatchedNames}";
    }

    public bool Equals(TaxonSearch? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TopMatchedNames.Equals(other.TopMatchedNames) && TotalMatchedNames == other.TotalMatchedNames;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaxonSearch)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(TopMatchedNames, TotalMatchedNames);
    }

    public static bool operator ==(TaxonSearch? left, TaxonSearch? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaxonSearch? left, TaxonSearch? right)
    {
        return !Equals(left, right);
    }
}