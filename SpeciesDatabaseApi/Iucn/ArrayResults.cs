using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;


public class ArrayResults<T> : BaseResponse, IEquatable<ArrayResults<T>>
{
    private int _count;

    [JsonPropertyName("count")]
    [XmlElement("count")]
    public int Count
    {
        get => _count == 0 ? Results.Length : _count;
        set => _count = value;
    }

    [JsonPropertyName("results")]
    [XmlElement("results")]
    public T[] Results { get; set; } = Array.Empty<T>();

    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder($"{base.ToString()}, {nameof(Count)}: {Count}, {nameof(Results)}: {Results.Length}");
        sb.AppendLine();
        for (var i = 0; i < Results.Length; i++)
        {
            sb.AppendLine($"## Result[{i}]:");
            sb.AppendLine(Results[i]?.ToString());
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)Results).GetEnumerator();
    }

    public bool Equals(ArrayResults<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Results.Equals(other.Results);
    }
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ArrayResults<T>)obj);
    }

    public static bool operator ==(ArrayResults<T>? left, ArrayResults<T>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ArrayResults<T>? left, ArrayResults<T>? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Results);
    }

    /// <inheritdoc />
    public T this[int index] => Results[index];
}