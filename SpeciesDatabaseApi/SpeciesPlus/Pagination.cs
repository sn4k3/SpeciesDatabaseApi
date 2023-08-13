using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

/// <summary>
/// Where more than 500 taxon concepts are returned, the request is paginated, showing 500 objects (or less by passing in an optional 'per_page' parameter) at a time.<br/>
/// To fetch the remaining objects, you will need to make a new request and pass the optional ‘page’ parameter
/// </summary>
[XmlRoot("pagination")]
public class Pagination : IEquatable<Pagination>
{
    [JsonPropertyName("current_page")]
    [XmlElement("current-page")]
    public int CurrentPage { get; set; } = 1;

    [JsonPropertyName("per_page")]
    [XmlElement("per-page")]
    public int PerPage { get; set; } = 500;

    [JsonPropertyName("total_entries")]
    [XmlElement("total-entries")]
    public int TotalEntries { get; set; } = 1;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(CurrentPage)}: {CurrentPage}, {nameof(PerPage)}: {PerPage}, {nameof(TotalEntries)}: {TotalEntries}";
    }

    public bool Equals(Pagination? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return CurrentPage == other.CurrentPage && PerPage == other.PerPage && TotalEntries == other.TotalEntries;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Pagination)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(CurrentPage, PerPage, TotalEntries);
    }

    public static bool operator ==(Pagination? left, Pagination? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Pagination? left, Pagination? right)
    {
        return !Equals(left, right);
    }
}