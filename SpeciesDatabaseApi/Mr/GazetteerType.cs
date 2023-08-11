using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.Mr;

public class GazetteerType : IEquatable<GazetteerType>
{
    [JsonPropertyName("typeID")]
    public int TypeId { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(TypeId)}: {TypeId}, {nameof(Type)}: {Type}, {nameof(Description)}: {Description}";
    }

    public bool Equals(GazetteerType? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TypeId == other.TypeId && Type == other.Type && Description == other.Description;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GazetteerType)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(TypeId, Type, Description);
    }

    public static bool operator ==(GazetteerType? left, GazetteerType? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GazetteerType? left, GazetteerType? right)
    {
        return !Equals(left, right);
    }
}