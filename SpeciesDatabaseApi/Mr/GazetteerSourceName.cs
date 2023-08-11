using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Mr;

public class GazetteerSourceName : IEquatable<GazetteerSourceName>
{
    [JsonPropertyName("source")]
    [XmlElement("source")]
    public string Source { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    [XmlElement("url")]
    public Uri? Url { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Source)}: {Source}, {nameof(Url)}: {Url}";
    }

    public bool Equals(GazetteerSourceName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Source == other.Source && Equals(Url, other.Url);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GazetteerSourceName)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Source, Url);
    }

    public static bool operator ==(GazetteerSourceName? left, GazetteerSourceName? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GazetteerSourceName? left, GazetteerSourceName? right)
    {
        return !Equals(left, right);
    }
}