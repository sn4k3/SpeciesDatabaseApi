using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class SpecimenResults : IEquatable<SpecimenResults>
{
    [JsonPropertyName("bold_records")]
    public BoldRecords BoldRecords { get; set; } = new();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(BoldRecords)}: {BoldRecords}";
    }

    public bool Equals(SpecimenResults? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return BoldRecords.Equals(other.BoldRecords);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SpecimenResults)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return BoldRecords.GetHashCode();
    }

    public static bool operator ==(SpecimenResults? left, SpecimenResults? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SpecimenResults? left, SpecimenResults? right)
    {
        return !Equals(left, right);
    }
}