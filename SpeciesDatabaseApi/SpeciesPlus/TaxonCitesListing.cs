using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("cites-listing")]
public class TaxonCitesListing : IEquatable<TaxonCitesListing>
{
    /// <summary>
    /// CITES appendix, one of I, II or III [max 255 characters]
    /// </summary>
    [JsonPropertyName("appendix")]
    [XmlElement("appendix")]
    public string Appendix { get; set; } = string.Empty;

    /// <summary>
    /// Text of annotation (translated based on locale)
    /// </summary>
    [JsonPropertyName("annotation")]
    [XmlElement("annotation")]
    public string? Annotation { get; set; }

    /// <summary>
    /// # annotation (plants)
    /// </summary>
    [JsonPropertyName("hash_annotation")]
    [XmlElement("hash-annotation")]
    public Annotation? HashAnnotation { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
	    return $"{nameof(Appendix)}: {Appendix}, {nameof(Annotation)}: {Annotation}, {nameof(HashAnnotation)}: {HashAnnotation}";
    }

    public bool Equals(TaxonCitesListing? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return Appendix == other.Appendix && Annotation == other.Annotation && Equals(HashAnnotation, other.HashAnnotation);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != this.GetType()) return false;
	    return Equals((TaxonCitesListing)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
	    return HashCode.Combine(Appendix, Annotation, HashAnnotation);
    }

    public static bool operator ==(TaxonCitesListing? left, TaxonCitesListing? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaxonCitesListing? left, TaxonCitesListing? right)
    {
        return !Equals(left, right);
    }
}