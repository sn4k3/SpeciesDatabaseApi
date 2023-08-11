using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class IndividualSpecie : IEquatable<IndividualSpecie>
{
    [JsonPropertyName("taxonid")]
    [XmlElement("taxonid")]
    public int TaxonId { get; set; }

    [JsonPropertyName("scientific_name")]
    [XmlElement("scientific_name")]
    public string ScientificName { get; set; } = string.Empty;

    [JsonPropertyName("kingdom")]
    [XmlElement("kingdom")]
    public string Kingdom { get; set; } = string.Empty;

    [JsonPropertyName("phylum")]
    [XmlElement("phylum")]
    public string Phylum { get; set; } = string.Empty;

    [JsonPropertyName("class")]
    [XmlElement("class")]
    public string Class { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    [XmlElement("order")]
    public string Order { get; set; } = string.Empty;
    
    [JsonPropertyName("family")]
    [XmlElement("family")]
    public string Family { get; set; } = string.Empty;

    [JsonPropertyName("genus")]
    [XmlElement("genus")]
    public string Genus { get; set; } = string.Empty;

    [JsonPropertyName("main_common_name")]
    [XmlElement("main_common_name")]
    public string? MainCommonName { get; set; }

    [JsonPropertyName("authority")]
    [XmlElement("authority")]
    public string Authority { get; set; } = string.Empty;

    [JsonPropertyName("published_year")]
    [XmlElement("published_year")]
    public int PublishedYear { get; set; }

    [JsonPropertyName("assessment_date")]
    [XmlElement("assessment_date")]
    public string AssessmentDate { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    [XmlElement("category")]
    public string Category { get; set; } = string.Empty;

    [JsonPropertyName("criteria")]
    [XmlElement("criteria")]
    public string Criteria { get; set; } = string.Empty;

    [JsonPropertyName("population_trend")]
    [XmlElement("population_trend")]
    public string PopulationTrend { get; set; } = string.Empty;

    [JsonPropertyName("marine_system")]
    [XmlElement("marine_system")]
    public bool MarineSystem { get; set; }

    [JsonPropertyName("freshwater_system")]
    [XmlElement("freshwater_system")]
    public bool FreshWaterSystem { get; set; }

    [JsonPropertyName("terrestrial_system")]
    [XmlElement("terrestrial_system")]
    public bool TerrestrialSystem { get; set; }

    [JsonPropertyName("assessor")]
    [XmlElement("assessor")]
    public string Assessor { get; set; } = string.Empty;

    [JsonPropertyName("reviewer")]
    [XmlElement("reviewer")]
    public string Reviewer { get; set; } = string.Empty;

    [JsonPropertyName("aoo_km2")]
    [XmlElement("aoo_km2")]
    public string AooKm2 { get; set; } = string.Empty;

    [JsonPropertyName("eoo_km2")]
    [XmlElement("eoo_km2")]
    public string EooKm2 { get; set; } = string.Empty;

    [JsonPropertyName("elevation_upper")]
    [XmlElement("elevation_upper")]
    public int? ElevationUpper { get; set; }

    [JsonPropertyName("elevation_lower")]
    [XmlElement("elevation_lower")]
    public int? ElevationLower { get; set; }

    [JsonPropertyName("depth_upper")]
    [XmlElement("depth_upper")]
    public int? DepthUpper { get; set; }

    [JsonPropertyName("depth_lower")]
    [XmlElement("depth_lower")]
    public int? DepthLower { get; set; }

    [JsonPropertyName("errata_flag")]
    [XmlElement("errata_flag")]
    public bool? ErrataFlag { get; set; }

    [JsonPropertyName("errata_reason")]
    [XmlElement("errata_reason")]
    public string? ErrataReason { get; set; }

    [JsonPropertyName("amended_flag")]
    [XmlElement("amended_flag")]
    public bool? AmendedFlag { get; set; }

    [JsonPropertyName("amended_reason")]
    [XmlElement("amended_reason")]
    public string? AmendedReason { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(TaxonId)}: {TaxonId}, {nameof(ScientificName)}: {ScientificName}, {nameof(Kingdom)}: {Kingdom}, {nameof(Phylum)}: {Phylum}, {nameof(Class)}: {Class}, {nameof(Order)}: {Order}, {nameof(Family)}: {Family}, {nameof(Genus)}: {Genus}, {nameof(MainCommonName)}: {MainCommonName}, {nameof(Authority)}: {Authority}, {nameof(PublishedYear)}: {PublishedYear}, {nameof(AssessmentDate)}: {AssessmentDate}, {nameof(Category)}: {Category}, {nameof(Criteria)}: {Criteria}, {nameof(PopulationTrend)}: {PopulationTrend}, {nameof(MarineSystem)}: {MarineSystem}, {nameof(FreshWaterSystem)}: {FreshWaterSystem}, {nameof(TerrestrialSystem)}: {TerrestrialSystem}, {nameof(Assessor)}: {Assessor}, {nameof(Reviewer)}: {Reviewer}, {nameof(AooKm2)}: {AooKm2}, {nameof(EooKm2)}: {EooKm2}, {nameof(ElevationUpper)}: {ElevationUpper}, {nameof(ElevationLower)}: {ElevationLower}, {nameof(DepthUpper)}: {DepthUpper}, {nameof(DepthLower)}: {DepthLower}, {nameof(ErrataFlag)}: {ErrataFlag}, {nameof(ErrataReason)}: {ErrataReason}, {nameof(AmendedFlag)}: {AmendedFlag}, {nameof(AmendedReason)}: {AmendedReason}";
    }

    public bool Equals(IndividualSpecie? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TaxonId == other.TaxonId && ScientificName == other.ScientificName && Kingdom == other.Kingdom && Phylum == other.Phylum && Class == other.Class && Order == other.Order && Family == other.Family && Genus == other.Genus && MainCommonName == other.MainCommonName && Authority == other.Authority && PublishedYear == other.PublishedYear && AssessmentDate == other.AssessmentDate && Category == other.Category && Criteria == other.Criteria && PopulationTrend == other.PopulationTrend && MarineSystem == other.MarineSystem && FreshWaterSystem == other.FreshWaterSystem && TerrestrialSystem == other.TerrestrialSystem && Assessor == other.Assessor && Reviewer == other.Reviewer && AooKm2 == other.AooKm2 && EooKm2 == other.EooKm2 && ElevationUpper == other.ElevationUpper && ElevationLower == other.ElevationLower && DepthUpper == other.DepthUpper && DepthLower == other.DepthLower && ErrataFlag == other.ErrataFlag && ErrataReason == other.ErrataReason && AmendedFlag == other.AmendedFlag && AmendedReason == other.AmendedReason;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((IndividualSpecie)obj);
    }

    public static bool operator ==(IndividualSpecie? left, IndividualSpecie? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(IndividualSpecie? left, IndividualSpecie? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(TaxonId);
        hashCode.Add(ScientificName);
        hashCode.Add(Kingdom);
        hashCode.Add(Phylum);
        hashCode.Add(Class);
        hashCode.Add(Order);
        hashCode.Add(Family);
        hashCode.Add(Genus);
        hashCode.Add(MainCommonName);
        hashCode.Add(Authority);
        hashCode.Add(PublishedYear);
        hashCode.Add(AssessmentDate);
        hashCode.Add(Category);
        hashCode.Add(Criteria);
        hashCode.Add(PopulationTrend);
        hashCode.Add(MarineSystem);
        hashCode.Add(FreshWaterSystem);
        hashCode.Add(TerrestrialSystem);
        hashCode.Add(Assessor);
        hashCode.Add(Reviewer);
        hashCode.Add(AooKm2);
        hashCode.Add(EooKm2);
        hashCode.Add(ElevationUpper);
        hashCode.Add(ElevationLower);
        hashCode.Add(DepthUpper);
        hashCode.Add(DepthLower);
        hashCode.Add(ErrataFlag);
        hashCode.Add(ErrataReason);
        hashCode.Add(AmendedFlag);
        hashCode.Add(AmendedReason);
        return hashCode.ToHashCode();
    }
}