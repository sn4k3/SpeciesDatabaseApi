using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Worms;

public class Source : IEquatable<Source>
{
    /// <summary>
    /// Unique identifier for the source within WoRMS
    /// </summary>
    [JsonPropertyName("source_id")]
    [XmlElement("source_id")]
    public int SourceId { get; set; }

    /// <summary>
    /// Usage of the source for this taxon.
    /// </summary>
    [JsonPropertyName("use")]
    [XmlElement("use")]
    public string Use { get; set; } = string.Empty;

    /// <summary>
    /// Full citation string
    /// </summary>
    [JsonPropertyName("reference")]
    [XmlElement("reference")]
    public string Reference { get; set; } = string.Empty;

    /// <summary>
    /// Page(s) where the taxon is mentioned
    /// </summary>
    [JsonPropertyName("page")]
    [XmlElement("page")]
    public string? Page { get; set; }

    /// <summary>
    /// Direct link to the source record
    /// </summary>
    [JsonPropertyName("url")]
    [XmlElement("url")]
    public Uri? Url { get; set; }

    /// <summary>
    /// External link (i.e. journal, data system, etc..)
    /// </summary>
    [JsonPropertyName("link")]
    [XmlElement("link")]
    public Uri? Link { get; set; }

    /// <summary>
    /// Full text link (PDF)
    /// </summary>
    [JsonPropertyName("fulltext")]
    [XmlElement("fulltext")]
    public string? Fulltext { get; set; }

    /// <summary>
    /// Digital Object Identifier
    /// </summary>
    [JsonPropertyName("doi")]
    [XmlElement("doi")]
    public string? Doi { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(SourceId)}: {SourceId}, {nameof(Use)}: {Use}, {nameof(Reference)}: {Reference}, {nameof(Page)}: {Page}, {nameof(Url)}: {Url}, {nameof(Link)}: {Link}, {nameof(Fulltext)}: {Fulltext}, {nameof(Doi)}: {Doi}";
    }


    public bool Equals(Source? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SourceId == other.SourceId && Use == other.Use && Reference == other.Reference && Page == other.Page && Equals(Url, other.Url) && Equals(Link, other.Link) && Fulltext == other.Fulltext && Doi == other.Doi;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Source)obj);
    }

    public static bool operator ==(Source? left, Source? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Source? left, Source? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(SourceId, Use, Reference, Page, Url, Link, Fulltext, Doi);
    }
}