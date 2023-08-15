using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

/// <summary>
/// Query parameters for the public API
/// </summary>
public class PublicApiParameters : IEquatable<PublicApiParameters>
{
    /// <summary>
    /// Returns all records containing matching taxa, defined in a pipe delimited list.<br/>
    /// Taxa includes scientific names at phylum, class, order, family, subfamily, genus, and species levels.
    /// </summary>
    /// <example>
    /// Bos taurus returns all records matching the taxon Bos taurus.<br/>
    /// Aves|Reptilia returns all records matching the taxa Aves or Reptilia
    /// </example>
    [JsonPropertyName("taxon")]
    public string? Taxon { get; set; }

    /// <summary>
    /// Returns all records containing matching IDs, defined in a pipe delimited list.
    /// IDs include Sample IDs, Process IDs, Museum IDs and Field IDs.
    /// </summary>
    /// <example>
    /// ids=ACRJP618-11|ACRJP619-11 returns records matching these Process IDs.<br/>
    /// ids=Example 10|Example 11|Example 12 returns records matching these Sample IDs.
    /// </example>
    [JsonPropertyName("ids")]
    public string? Ids { get; set; }

    /// <summary>
    /// Returns all records contained in matching BINs, defined in a pipe delimited list.<br/>
    /// A BIN is defined by a Barcode Index Number URI.
    /// </summary>
    /// <example>
    /// bin=BOLD:AAA5125|BOLD:AAA5126 returns records matching these BIN URIs.
    /// </example>
    [JsonPropertyName("bin")]
    public string? Bin { get; set; }

    /// <summary>
    /// Returns all records contained in matching projects or datasets, in a pipe delimited list.<br/>
    /// Containers include project codes and dataset codes.
    /// </summary>
    /// <example>
    ///container=SSBAA|SSBAB returns records contained within matching projects.<br/>
    /// container=DS-EZROM returns records contained within the matching dataset.
    /// </example>
    [JsonPropertyName("container")]
    public string? Container { get; set; }

    /// <summary>
    /// Returns all records stored in matching institutions, defined in a pipe delimited list.<br/>
    /// Institutions are the Specimen Storing Site.
    /// </summary>
    /// <example>institutions=Biodiversity Institute of Ontario|York University returns records for specimens stored within matching institutions.</example>
    [JsonPropertyName("institutions")]
    public string? Institutions { get; set; }

    /// <summary>
    /// Returns all records containing matching researcher names, defined in a pipe delimited list.<br/>
    /// Researchers include collectors and specimen identifiers.
    /// </summary>
    [JsonPropertyName("researchers")]
    public string? Researchers { get; set; }

    /// <summary>
    /// Returns all records collected in matching geographic sites, defined in a pipe delimited list.<br/>
    /// Geographic sites includes countries and province/states.
    /// </summary>
    /// <example>geo=Canada|Alaska returns records for specimens collected in the matching geographic sites.</example>
    [JsonPropertyName("geo")]
    public string? Geo { get; set; }

    /// <summary>
    /// Returns all specimen records containing matching marker codes defined in a pipe delimited list.<br/>
    /// All markers for a specimen matching the search string will be returned. ie. A record with COI-5P and ITS will return sequence data for both markers even if only COI-5P was specified.
    /// </summary>
    /// <example>
    ///marker=matK|rbcL<br/>
    /// marker=COI-5P
    /// </example>
    [JsonPropertyName("marker")]
    public string? Marker { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Taxon)}: {Taxon}, {nameof(Ids)}: {Ids}, {nameof(Bin)}: {Bin}, {nameof(Container)}: {Container}, {nameof(Institutions)}: {Institutions}, {nameof(Researchers)}: {Researchers}, {nameof(Geo)}: {Geo}, {nameof(Marker)}: {Marker}";
    }

    public bool Equals(PublicApiParameters? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Taxon == other.Taxon && Ids == other.Ids && Bin == other.Bin && Container == other.Container && Institutions == other.Institutions && Researchers == other.Researchers && Geo == other.Geo && Marker == other.Marker;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PublicApiParameters)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Taxon, Ids, Bin, Container, Institutions, Researchers, Geo, Marker);
    }

    public static bool operator ==(PublicApiParameters? left, PublicApiParameters? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(PublicApiParameters? left, PublicApiParameters? right)
    {
        return !Equals(left, right);
    }
}