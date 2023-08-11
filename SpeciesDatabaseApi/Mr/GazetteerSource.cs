using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Mr;

public class GazetteerSource : IEquatable<GazetteerSource>
{
    [JsonPropertyName("sourceId")]
    [XmlElement("sourceId")]
    public int SourceId { get; set; }

    [JsonPropertyName("source")]
    [XmlElement("source")]
    public string Source { get; set; } = string.Empty;

    [JsonPropertyName("sourceURL")]
    [XmlElement("sourceURL")]
    public Uri? SourceUrl { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(SourceId)}: {SourceId}, {nameof(Source)}: {Source}, {nameof(SourceUrl)}: {SourceUrl}";
    }

    public bool Equals(GazetteerSource? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SourceId == other.SourceId && Source == other.Source && Equals(SourceUrl, other.SourceUrl);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GazetteerSource)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(SourceId, Source, SourceUrl);
    }

    public static bool operator ==(GazetteerSource? left, GazetteerSource? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GazetteerSource? left, GazetteerSource? right)
    {
        return !Equals(left, right);
    }
}