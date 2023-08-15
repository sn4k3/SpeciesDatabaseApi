using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TaxonDataBasic : IEquatable<TaxonDataBasic>
{
    [JsonPropertyName("taxid")]
    public int? TaxId { get; set; }

    [JsonPropertyName("taxon")]
    public string? Taxon { get; set; }

    [JsonPropertyName("tax_rank")]
    public string? TaxRank { get; set; }

    [JsonPropertyName("tax_division")]
    public string? TaxDivision { get; set; }

    [JsonPropertyName("parentid")]
    public int? ParentId { get; set; }

    [JsonPropertyName("parentname")]
    public string? ParentName { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
	    return $"{nameof(TaxId)}: {TaxId}, {nameof(Taxon)}: {Taxon}, {nameof(TaxRank)}: {TaxRank}, {nameof(TaxDivision)}: {TaxDivision}, {nameof(ParentId)}: {ParentId}, {nameof(ParentName)}: {ParentName}";
    }

    public bool Equals(TaxonDataBasic? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return TaxId == other.TaxId && Taxon == other.Taxon && TaxRank == other.TaxRank && TaxDivision == other.TaxDivision && ParentId == other.ParentId && ParentName == other.ParentName;
    }

    /// <inheritdoc />
	public override bool Equals(object? obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != this.GetType()) return false;
	    return Equals((TaxonDataBasic)obj);
    }

    /// <inheritdoc />
	public override int GetHashCode()
    {
	    return HashCode.Combine(TaxId, Taxon, TaxRank, TaxDivision, ParentId, ParentName);
    }

    public static bool operator ==(TaxonDataBasic? left, TaxonDataBasic? right)
    {
	    return Equals(left, right);
    }

    public static bool operator !=(TaxonDataBasic? left, TaxonDataBasic? right)
    {
	    return !Equals(left, right);
    }
}