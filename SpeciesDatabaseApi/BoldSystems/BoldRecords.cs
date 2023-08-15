using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class BoldRecords : IEquatable<BoldRecords>
{
    [JsonPropertyName("records")]
    public Dictionary<string, SpecimenRecord> Records { get; set; } = new();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Records)}: {Records}";
    }

    /// <inheritdoc />
    public bool Equals(BoldRecords? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Records.Equals(other.Records);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BoldRecords)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Records.GetHashCode();
    }

    public static bool operator ==(BoldRecords? left, BoldRecords? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(BoldRecords? left, BoldRecords? right)
    {
        return !Equals(left, right);
    }
}