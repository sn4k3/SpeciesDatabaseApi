using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TaxonData : TaxonDataBasic, IEquatable<TaxonData>
{
    #region Stats
    
    [JsonPropertyName("stats")]
    public TaxonDataStats? Stats { get; set; }


    [JsonPropertyName("publicrecords")]
    public int? PublicRecords { get; set; }

    [JsonPropertyName("specimenrecords")]
    public int? SpecimenRecords { get; set; }

    [JsonPropertyName("sequencedspecimens")]
    public int? SequencedSpecimens { get; set; }

    [JsonPropertyName("barcodespecimens")]
    public int? BarcodeSpecimens { get; set; }

    [JsonPropertyName("species")]
    public int? Species { get; set; }
    
    [JsonPropertyName("barcodespecies")]
    public int? BarcodeSpecies { get; set; }
    #endregion

    #region Geo

    [JsonPropertyName("country")]
    public Dictionary<string, int>? Country { get; set; }

    [JsonPropertyName("sitemap")]
    public string? Sitemap { get; set; }
    #endregion

    #region Images

    [JsonPropertyName("images")]
    public TaxonDataImage? Images { get; set; }

    [JsonPropertyName("representitive_image")]
    public TaxonDataRepresentitiveImage? RepresentitiveImage { get; set; }

    #endregion

    #region SequencingLabs

    /// <summary>
    /// sequencing labs: includes lab name, record count
    /// </summary>
    [JsonPropertyName("sequencinglabs")]
    public Dictionary<string, int>? SequencingLabs { get; set; }

    #endregion

    #region Depository 

    /// <summary>
    /// specimen depositories: includes depository name, record count
    /// </summary>
    [JsonPropertyName("depository")]
    public Dictionary<string, int>? Depository { get; set; }

    #endregion

    #region Thirdparty 

    [JsonPropertyName("wikipedia_summary")]
    public string? WikipediaSummary { get; set; }

    [JsonPropertyName("wikipedia_link")]
    public string? WikipediaLink { get; set; }

    #endregion

    /// <inheritdoc />
    public override string ToString()
    {
	    return $"{base.ToString()}, {nameof(Stats)}: {Stats}, {nameof(PublicRecords)}: {PublicRecords}, {nameof(SpecimenRecords)}: {SpecimenRecords}, {nameof(SequencedSpecimens)}: {SequencedSpecimens}, {nameof(BarcodeSpecimens)}: {BarcodeSpecimens}, {nameof(Species)}: {Species}, {nameof(BarcodeSpecies)}: {BarcodeSpecies}, {nameof(Country)}: {Country}, {nameof(Sitemap)}: {Sitemap}, {nameof(Images)}: {Images}, {nameof(RepresentitiveImage)}: {RepresentitiveImage}, {nameof(SequencingLabs)}: {SequencingLabs}, {nameof(Depository)}: {Depository}, {nameof(WikipediaSummary)}: {WikipediaSummary}, {nameof(WikipediaLink)}: {WikipediaLink}";
    }

    public bool Equals(TaxonData? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return base.Equals(other) && Equals(Stats, other.Stats) && PublicRecords == other.PublicRecords && SpecimenRecords == other.SpecimenRecords && SequencedSpecimens == other.SequencedSpecimens && BarcodeSpecimens == other.BarcodeSpecimens && Species == other.Species && BarcodeSpecies == other.BarcodeSpecies && Equals(Country, other.Country) && Sitemap == other.Sitemap && Equals(Images, other.Images) && Equals(RepresentitiveImage, other.RepresentitiveImage) && Equals(SequencingLabs, other.SequencingLabs) && Equals(Depository, other.Depository) && WikipediaSummary == other.WikipediaSummary && WikipediaLink == other.WikipediaLink;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != this.GetType()) return false;
	    return Equals((TaxonData)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
	    var hashCode = new HashCode();
	    hashCode.Add(base.GetHashCode());
	    hashCode.Add(Stats);
	    hashCode.Add(PublicRecords);
	    hashCode.Add(SpecimenRecords);
	    hashCode.Add(SequencedSpecimens);
	    hashCode.Add(BarcodeSpecimens);
	    hashCode.Add(Species);
	    hashCode.Add(BarcodeSpecies);
	    hashCode.Add(Country);
	    hashCode.Add(Sitemap);
	    hashCode.Add(Images);
	    hashCode.Add(RepresentitiveImage);
	    hashCode.Add(SequencingLabs);
	    hashCode.Add(Depository);
	    hashCode.Add(WikipediaSummary);
	    hashCode.Add(WikipediaLink);
	    return hashCode.ToHashCode();
    }

    public static bool operator ==(TaxonData? left, TaxonData? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaxonData? left, TaxonData? right)
    {
        return !Equals(left, right);
    }
}