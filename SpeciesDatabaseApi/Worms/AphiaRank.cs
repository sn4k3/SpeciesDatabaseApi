using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Worms;

public class AphiaRank : IEquatable<AphiaRank>
{
    /// <summary>
    /// A taxonomic rank identifier
    /// </summary>
    [JsonPropertyName("taxonRankID")]
    [XmlElement("taxonRankID")]
    public int TaxonRankId { get; set; }

    /// <summary>
    /// A taxonomic rank name
    /// </summary>
    [JsonPropertyName("taxonRank")]
    [XmlElement("taxonRank")]
    public string TaxonRank { get; set; } = string.Empty;

    /// <summary>
    /// AphiaID of the kingdom
    /// </summary>
    [JsonPropertyName("AphiaID")]
    [XmlElement("AphiaID")]
    public int AphiaId { get; set; }

    /// <summary>
    /// The name of a taxonomic kingdom the rank is used in
    /// </summary>
    [JsonPropertyName("kingdom")]
    [XmlElement("kingdom")]
    public string Kingdom { get; set; } = "Animalia";

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(TaxonRankId)}: {TaxonRankId}, {nameof(TaxonRank)}: {TaxonRank}, {nameof(AphiaId)}: {AphiaId}, {nameof(Kingdom)}: {Kingdom}";
    }

    public bool Equals(AphiaRank? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TaxonRankId == other.TaxonRankId && TaxonRank == other.TaxonRank && AphiaId == other.AphiaId && Kingdom == other.Kingdom;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AphiaRank)obj);
    }

    public static bool operator ==(AphiaRank? left, AphiaRank? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AphiaRank? left, AphiaRank? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(TaxonRankId, TaxonRank, AphiaId, Kingdom);
    }
}