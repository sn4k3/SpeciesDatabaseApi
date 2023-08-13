using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

public class Synonym : IEquatable<Synonym>
{
    [JsonPropertyName("id")]
    [XmlElement("id")]
    public int Id { get; set; }

    [JsonPropertyName("full_name")]
    [XmlElement("full_name")]
    public string FullName { get; set; } = string.Empty;

    [JsonPropertyName("author_year")]
    [XmlElement("author_year")]
    public string AuthorYear { get; set; } = string.Empty;

    [JsonPropertyName("rank")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [XmlElement("rank")]
    public TaxaRankEnum Rank { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(FullName)}: {FullName}, {nameof(AuthorYear)}: {AuthorYear}, {nameof(Rank)}: {Rank}";
    }

    public bool Equals(Synonym? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && FullName == other.FullName && AuthorYear == other.AuthorYear && Rank == other.Rank;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Synonym)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FullName, AuthorYear, (int)Rank);
    }

    public static bool operator ==(Synonym? left, Synonym? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Synonym? left, Synonym? right)
    {
        return !Equals(left, right);
    }
}