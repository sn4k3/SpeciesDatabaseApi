using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class Sequences : IEquatable<Sequences>
{
    [JsonPropertyName("sequence")]
    public Sequence[] Items { get; set; } = Array.Empty<Sequence>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Items)}: {Items.Length}";
    }

    /// <inheritdoc />
    public bool Equals(Sequences? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Items.Equals(other.Items);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Sequences)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Items.GetHashCode();
    }

    public static bool operator ==(Sequences? left, Sequences? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Sequences? left, Sequences? right)
    {
        return !Equals(left, right);
    }
}