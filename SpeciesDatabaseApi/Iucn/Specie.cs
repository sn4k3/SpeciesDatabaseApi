using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class Specie : IEquatable<Specie>
{
    [JsonPropertyName("taxonid")]
    [XmlElement("taxonid")]
    public int TaxonId { get; set; }

    [JsonPropertyName("kingdom_name")]
    [XmlElement("kingdom_name")]
    public string KingdomName { get; set; } = string.Empty;

    [JsonPropertyName("phylum_name")]
    [XmlElement("phylum_name")]
    public string PhylumName { get; set; } = string.Empty;

    [JsonPropertyName("class_name")]
    [XmlElement("class_name")]
    public string ClassName { get; set; } = string.Empty;

    [JsonPropertyName("order_name")]
    [XmlElement("order_name")]
    public string OrderName { get; set; } = string.Empty;
    
    [JsonPropertyName("family_name")]
    [XmlElement("family_name")]
    public string FamilyName { get; set; } = string.Empty;

    [JsonPropertyName("genus_name")]
    [XmlElement("genus_name")]
    public string GenusName { get; set; } = string.Empty;

    [JsonPropertyName("scientific_name")]
    [XmlElement("scientific_name")]
    public string ScientificName { get; set; } = string.Empty;

    [JsonPropertyName("taxonomic_authority")]
    [XmlElement("taxonomic_authority")]
    public string TaxonomicAuthority { get; set; } = string.Empty;

    [JsonPropertyName("infra_rank")]
    [XmlElement("infra_rank")]
    public string? InfraRank { get; set; }

    [JsonPropertyName("population")]
    [XmlElement("population")]
    public string? Population { get; set; }

    [JsonPropertyName("category")]
    [XmlElement("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("main_common_name")]
    [XmlElement("main_common_name")]
    public string? MainCommonName { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(TaxonId)}: {TaxonId}, {nameof(KingdomName)}: {KingdomName}, {nameof(PhylumName)}: {PhylumName}, {nameof(ClassName)}: {ClassName}, {nameof(OrderName)}: {OrderName}, {nameof(FamilyName)}: {FamilyName}, {nameof(GenusName)}: {GenusName}, {nameof(ScientificName)}: {ScientificName}, {nameof(TaxonomicAuthority)}: {TaxonomicAuthority}, {nameof(InfraRank)}: {InfraRank}, {nameof(Population)}: {Population}, {nameof(Category)}: {Category}, {nameof(MainCommonName)}: {MainCommonName}";
    }

    public bool Equals(Specie? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TaxonId == other.TaxonId && KingdomName == other.KingdomName && PhylumName == other.PhylumName && ClassName == other.ClassName && OrderName == other.OrderName && FamilyName == other.FamilyName && GenusName == other.GenusName && ScientificName == other.ScientificName && TaxonomicAuthority == other.TaxonomicAuthority && InfraRank == other.InfraRank && Population == other.Population && Category == other.Category && MainCommonName == other.MainCommonName;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Specie)obj);
    }

    public static bool operator ==(Specie? left, Specie? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Specie? left, Specie? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(TaxonId);
        hashCode.Add(KingdomName);
        hashCode.Add(PhylumName);
        hashCode.Add(ClassName);
        hashCode.Add(OrderName);
        hashCode.Add(FamilyName);
        hashCode.Add(GenusName);
        hashCode.Add(ScientificName);
        hashCode.Add(TaxonomicAuthority);
        hashCode.Add(InfraRank);
        hashCode.Add(Population);
        hashCode.Add(Category);
        hashCode.Add(MainCommonName);
        return hashCode.ToHashCode();
    }
}