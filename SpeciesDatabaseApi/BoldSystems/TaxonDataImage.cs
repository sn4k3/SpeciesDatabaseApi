using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TaxonDataImage : IEquatable<TaxonDataImage>
{

    [JsonPropertyName("copyright_institution")]
    public string? CopyrightInstitution { get; set; }

    [JsonPropertyName("specimenid")]
    public int? SpecimenId { get; set; }

    [JsonPropertyName("copyright")]
    public string? Copyright { get; set; }

    [JsonPropertyName("imagequality")]
    public int Imagequality { get; set; }

    [JsonPropertyName("photographer")]
    public string? Photographer { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("fieldnum")]
    public string? FieldNum { get; set; }

    [JsonPropertyName("sampleid")]
    public string? SampleId { get; set; }

    [JsonPropertyName("mam_uri")]
    public string? MamUri { get; set; }

    [JsonPropertyName("copyright_license")]
    public string? CopyrightLicense { get; set; }

    [JsonPropertyName("meta")]
    public string? Meta { get; set; }

    [JsonPropertyName("copyright_holder")]
    public string? CopyrightHolder { get; set; }

    [JsonPropertyName("catalognum")]
    public string? CatalogNum { get; set; }

    [JsonPropertyName("copyright_contact")]
    public string? CopyrightContact { get; set; }

    [JsonPropertyName("copyright_year")]
    public int? CopyrightYear { get; set; }

    [JsonPropertyName("taxonrep")]
    public string? TaxonRep { get; set; }

    [JsonPropertyName("aspectratio")]
    public decimal AspectRatio { get; set; }

    [JsonPropertyName("original")]
    public bool Original { get; set; }

    [JsonPropertyName("external")]
    public string? External { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(CopyrightInstitution)}: {CopyrightInstitution}, {nameof(SpecimenId)}: {SpecimenId}, {nameof(Copyright)}: {Copyright}, {nameof(Imagequality)}: {Imagequality}, {nameof(Photographer)}: {Photographer}, {nameof(Image)}: {Image}, {nameof(FieldNum)}: {FieldNum}, {nameof(SampleId)}: {SampleId}, {nameof(MamUri)}: {MamUri}, {nameof(CopyrightLicense)}: {CopyrightLicense}, {nameof(Meta)}: {Meta}, {nameof(CopyrightHolder)}: {CopyrightHolder}, {nameof(CatalogNum)}: {CatalogNum}, {nameof(CopyrightContact)}: {CopyrightContact}, {nameof(CopyrightYear)}: {CopyrightYear}, {nameof(TaxonRep)}: {TaxonRep}, {nameof(AspectRatio)}: {AspectRatio}, {nameof(Original)}: {Original}, {nameof(External)}: {External}";
    }

    public bool Equals(TaxonDataImage? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return CopyrightInstitution == other.CopyrightInstitution && SpecimenId == other.SpecimenId && Copyright == other.Copyright && Imagequality == other.Imagequality && Photographer == other.Photographer && Image == other.Image && FieldNum == other.FieldNum && SampleId == other.SampleId && MamUri == other.MamUri && CopyrightLicense == other.CopyrightLicense && Meta == other.Meta && CopyrightHolder == other.CopyrightHolder && CatalogNum == other.CatalogNum && CopyrightContact == other.CopyrightContact && CopyrightYear == other.CopyrightYear && TaxonRep == other.TaxonRep && AspectRatio == other.AspectRatio && Original == other.Original && External == other.External;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TaxonDataImage)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(CopyrightInstitution);
        hashCode.Add(SpecimenId);
        hashCode.Add(Copyright);
        hashCode.Add(Imagequality);
        hashCode.Add(Photographer);
        hashCode.Add(Image);
        hashCode.Add(FieldNum);
        hashCode.Add(SampleId);
        hashCode.Add(MamUri);
        hashCode.Add(CopyrightLicense);
        hashCode.Add(Meta);
        hashCode.Add(CopyrightHolder);
        hashCode.Add(CatalogNum);
        hashCode.Add(CopyrightContact);
        hashCode.Add(CopyrightYear);
        hashCode.Add(TaxonRep);
        hashCode.Add(AspectRatio);
        hashCode.Add(Original);
        hashCode.Add(External);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(TaxonDataImage? left, TaxonDataImage? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TaxonDataImage? left, TaxonDataImage? right)
    {
        return !Equals(left, right);
    }
}