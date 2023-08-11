using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Worms;

public class AphiaRecord : IEquatable<AphiaRecord>
{
    /// <summary>
    /// Unique and persistent identifier within WoRMS. Primary key in the database
    /// </summary>
    [JsonPropertyName("AphiaID")]
    [XmlElement("AphiaID")]
    public int AphiaId { get; set; }

    /// <summary>
    /// HTTP URL to the AphiaRecord
    /// </summary>
    [JsonPropertyName("url")]
    [XmlElement("url")]
    public Uri Url { get; set; } = WormsClient.DefaultApiAddress;

    /// <summary>
    /// The full scientific name without authorship
    /// </summary>
    [JsonPropertyName("scientificname")]
    [XmlElement("scientificname")]
    public string ScientificName { get; set; } = string.Empty;

    /// <summary>
    /// The authorship information for the <see cref="ScientificName"/> formatted according to the conventions of the applicable nomenclaturalCode
    /// </summary>
    [JsonPropertyName("authority")]
    [XmlElement("authority")]
    public string Authority { get; set; } = string.Empty;

    /// <summary>
    /// The taxonomic rank identifier of the most specific name in the <see cref="ScientificName"/>
    /// </summary>
    [JsonPropertyName("taxonRankID")]
    [XmlElement("taxonRankID")]
    public int TaxonRankId { get; set; }

    /// <summary>
    /// The taxonomic rank of the most specific name in the <see cref="ScientificName"/>
    /// </summary>
    [JsonPropertyName("rank")]
    [XmlElement("rank")]
    public string Rank { get; set; } = string.Empty;

    /// <summary>
    /// The status of the use of the <see cref="ScientificName"/> (usually a taxonomic opinion).
    /// Additional technical statuses are (1) quarantined: hidden from public interface after decision from an editor and (2) deleted:
    /// AphiaID should NOT be used anymore, please replace it by the valid_AphiaID
    /// </summary>
    [JsonPropertyName("status")]
    [XmlElement("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// The reason why a <see cref="ScientificName"/> is unaccepted
    /// </summary>
    [JsonPropertyName("unacceptreason")]
    [XmlElement("unacceptreason")]
    public string? UnacceptReason { get; set; }

    /// <summary>
    /// The AphiaID (for the <see cref="ScientificName"/>) of the currently accepted taxon. NULL if there is no currently accepted taxon.
    /// </summary>
    [JsonPropertyName("valid_AphiaID")]
    [XmlElement("valid_AphiaID")]
    public int? ValidAphiaID { get; set; }

    /// <summary>
    /// The <see cref="ScientificName"/> of the currently accepted taxon
    /// </summary>
    [JsonPropertyName("valid_name")]
    [XmlElement("valid_name")]
    public string? ValidName { get; set; }

    /// <summary>
    /// The authorship information for the <see cref="ScientificName"/> of the currently accepted taxon
    /// </summary>
    [JsonPropertyName("valid_authority")]
    [XmlElement("valid_authority")]
    public string? ValidAuthority { get; set; }

    /// <summary>
    /// The AphiaID (for the <see cref="ScientificName"/>) of the direct, most proximate higher-rank parent taxon (in a classification)
    /// </summary>
    [JsonPropertyName("parentNameUsageID")]
    [XmlElement("parentNameUsageID")]
    public int? ParentNameUsageId { get; set; }

    /// <summary>
    /// The full scientific name of the kingdom in which the taxon is classified
    /// </summary>
    [JsonPropertyName("kingdom")]
    [XmlElement("kingdom")]
    public string Kingdom { get; set; } = string.Empty;

    /// <summary>
    /// The full scientific name of the phylum or division in which the taxon is classified
    /// </summary>
    [JsonPropertyName("phylum")]
    [XmlElement("phylum")]
    public string Phylum { get; set; } = string.Empty;

	/// <summary>
	/// The full scientific name of the class in which the taxon is classified
	/// </summary>
	[JsonPropertyName("class")]
    [XmlElement("class")]
    public string Class { get; set; } = string.Empty;

	/// <summary>
	/// The full scientific name of the order in which the taxon is classified
	/// </summary>
	[JsonPropertyName("order")]
    [XmlElement("order")]
    public string Order { get; set; } = string.Empty;

	/// <summary>
	/// The full scientific name of the family in which the taxon is classified
	/// </summary>
	[JsonPropertyName("family")]
    [XmlElement("family")]
    public string Family { get; set; } = string.Empty;

	/// <summary>
	/// The full scientific name of the genus in which the taxon is classified
	/// </summary>
	[JsonPropertyName("genus")]
    [XmlElement("genus")]
    public string Genus { get; set; } = string.Empty;

	/// <summary>
	/// A bibliographic reference for the resource as a statement indicating how this record should be cited (attributed) when used
	/// </summary>
	[JsonPropertyName("citation")]
    [XmlElement("citation")]
    public string Citation { get; set; } = string.Empty;

	/// <summary>
	/// LifeScience Identifier. Persistent GUID for an AphiaID
	/// </summary>
	[JsonPropertyName("lsid")]
    [XmlElement("lsid")]
    public string lsId { get; set; } = string.Empty;

	/// <summary>
	/// A flag indicating whether the taxon is a marine organism, i.e. can be found in/above sea water. Possible values: 0/1/NULL
	/// </summary>
	[JsonPropertyName("isMarine")]
    [XmlElement("isMarine")]
    public int? IsMarine { get; set; }

    /// <summary>
    /// A flag indicating whether the taxon occurrs in brackish habitats. Possible values: 0/1/NULL
    /// </summary>
    [JsonPropertyName("isBrackish")]
    [XmlElement("isBrackish")]
    public int? IsBrackish { get; set; }

    /// <summary>
    /// A flag indicating whether the taxon occurrs in freshwater habitats, i.e. can be found in/above rivers or lakes. Possible values: 0/1/NULL
    /// </summary>
    [JsonPropertyName("isFreshwater")]
    [XmlElement("isFreshwater")]
    public int? IsFreshwater { get; set; }

    /// <summary>
    /// A flag indicating the taxon is a terrestial organism, i.e. occurrs on land as opposed to the sea. Possible values: 0/1/NULL
    /// </summary>
    [JsonPropertyName("IsTerrestrial")]
    [XmlElement("IsTerrestrial")]
    public int? IsTerrestrial { get; set; }

    /// <summary>
    /// A indicating an extinct organism. Possible values: 0/1/NULL
    /// </summary>
    [JsonPropertyName("IsExtinct")]
    [XmlElement("IsExtinct")]
    public int? IsExtinct { get; set; }

    /// <summary>
    /// Type of match. Possible values: exact/like/phonetic/near_1/near_2
    /// </summary>
    [JsonPropertyName("match_type")]
    [XmlElement("match_type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AphiaMatchType MatchType { get; set; } = AphiaMatchType.Exact;

    /// <summary>
    /// The most recent date-time in GMT on which the resource was changed
    /// </summary>
    [JsonPropertyName("modified")]
    [XmlElement("modified")]
    public DateTime? Modified { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(AphiaId)}: {AphiaId}, {nameof(Url)}: {Url}, {nameof(ScientificName)}: {ScientificName}, {nameof(Authority)}: {Authority}, {nameof(TaxonRankId)}: {TaxonRankId}, {nameof(Rank)}: {Rank}, {nameof(Status)}: {Status}, {nameof(UnacceptReason)}: {UnacceptReason}, {nameof(ValidAphiaID)}: {ValidAphiaID}, {nameof(ValidName)}: {ValidName}, {nameof(ValidAuthority)}: {ValidAuthority}, {nameof(ParentNameUsageId)}: {ParentNameUsageId}, {nameof(Kingdom)}: {Kingdom}, {nameof(Phylum)}: {Phylum}, {nameof(Class)}: {Class}, {nameof(Order)}: {Order}, {nameof(Family)}: {Family}, {nameof(Genus)}: {Genus}, {nameof(Citation)}: {Citation}, {nameof(lsId)}: {lsId}, {nameof(IsMarine)}: {IsMarine}, {nameof(IsBrackish)}: {IsBrackish}, {nameof(IsFreshwater)}: {IsFreshwater}, {nameof(IsTerrestrial)}: {IsTerrestrial}, {nameof(IsExtinct)}: {IsExtinct}, {nameof(MatchType)}: {MatchType}, {nameof(Modified)}: {Modified}";
    }

    public bool Equals(AphiaRecord? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return AphiaId == other.AphiaId && Url.Equals(other.Url) && ScientificName == other.ScientificName && Authority == other.Authority && TaxonRankId == other.TaxonRankId && Rank == other.Rank && Status == other.Status && UnacceptReason == other.UnacceptReason && ValidAphiaID == other.ValidAphiaID && ValidName == other.ValidName && ValidAuthority == other.ValidAuthority && ParentNameUsageId == other.ParentNameUsageId && Kingdom == other.Kingdom && Phylum == other.Phylum && Class == other.Class && Order == other.Order && Family == other.Family && Genus == other.Genus && Citation == other.Citation && lsId == other.lsId && IsMarine == other.IsMarine && IsBrackish == other.IsBrackish && IsFreshwater == other.IsFreshwater && IsTerrestrial == other.IsTerrestrial && IsExtinct == other.IsExtinct && MatchType == other.MatchType && Nullable.Equals(Modified, other.Modified);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != this.GetType()) return false;
	    return Equals((AphiaRecord)obj);
    }

    public static bool operator ==(AphiaRecord? left, AphiaRecord? right)
    {
	    return Equals(left, right);
    }

    public static bool operator !=(AphiaRecord? left, AphiaRecord? right)
    {
	    return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
	    var hashCode = new HashCode();
	    hashCode.Add(AphiaId);
	    hashCode.Add(Url);
	    hashCode.Add(ScientificName);
	    hashCode.Add(Authority);
	    hashCode.Add(TaxonRankId);
	    hashCode.Add(Rank);
	    hashCode.Add(Status);
	    hashCode.Add(UnacceptReason);
	    hashCode.Add(ValidAphiaID);
	    hashCode.Add(ValidName);
	    hashCode.Add(ValidAuthority);
	    hashCode.Add(ParentNameUsageId);
	    hashCode.Add(Kingdom);
	    hashCode.Add(Phylum);
	    hashCode.Add(Class);
	    hashCode.Add(Order);
	    hashCode.Add(Family);
	    hashCode.Add(Genus);
	    hashCode.Add(Citation);
	    hashCode.Add(lsId);
	    hashCode.Add(IsMarine);
	    hashCode.Add(IsBrackish);
	    hashCode.Add(IsFreshwater);
	    hashCode.Add(IsTerrestrial);
	    hashCode.Add(IsExtinct);
	    hashCode.Add((int)MatchType);
	    hashCode.Add(Modified);
	    return hashCode.ToHashCode();
    }
}