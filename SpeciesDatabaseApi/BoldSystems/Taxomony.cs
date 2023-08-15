using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class Taxomony : IEquatable<Taxomony>
{
    [JsonPropertyName("identification_provided_by")]
    public string IdentificationProvidedBy { get; set; } = string.Empty;

    [JsonPropertyName("phylum")]
    public TaxomonyRank? Phylum { get; set; }

    [JsonPropertyName("class")]
    public TaxomonyRank? Class { get; set; }

    [JsonPropertyName("order")]
    public TaxomonyRank? Order { get; set; }

    [JsonPropertyName("family")]
    public TaxomonyRank? Family { get; set; }

    [JsonPropertyName("genus")]
    public TaxomonyRank? Genus { get; set; }

    [JsonPropertyName("species")]
    public TaxomonyRank? Species { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(IdentificationProvidedBy)}: {IdentificationProvidedBy}, {nameof(Phylum)}: {Phylum}, {nameof(Class)}: {Class}, {nameof(Order)}: {Order}, {nameof(Family)}: {Family}, {nameof(Genus)}: {Genus}, {nameof(Species)}: {Species}";
    }

    public bool Equals(Taxomony? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return IdentificationProvidedBy == other.IdentificationProvidedBy && Equals(Phylum, other.Phylum) && Equals(Class, other.Class) && Equals(Order, other.Order) && Equals(Family, other.Family) && Equals(Genus, other.Genus) && Equals(Species, other.Species);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Taxomony)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(IdentificationProvidedBy, Phylum, Class, Order, Family, Genus, Species);
    }

    public static bool operator ==(Taxomony? left, Taxomony? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Taxomony? left, Taxomony? right)
    {
        return !Equals(left, right);
    }
}