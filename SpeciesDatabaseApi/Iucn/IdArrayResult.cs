using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System;

namespace SpeciesDatabaseApi.Iucn;

public class IdArrayResult<T> : ArrayResult<T>, IEquatable<IdArrayResult<T>>
{
    [JsonPropertyName("id")]
    [XmlElement("id")]
    public int Id { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {base.ToString()}";
    }

    /// <inheritdoc />
    public bool Equals(IdArrayResult<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((IdArrayResult<T>)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Id);
    }

    public static bool operator ==(IdArrayResult<T>? left, IdArrayResult<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(IdArrayResult<T>? left, IdArrayResult<T>? right)
    {
        return !Equals(left, right);
    }
}