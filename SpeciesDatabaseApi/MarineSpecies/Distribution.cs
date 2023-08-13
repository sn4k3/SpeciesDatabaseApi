using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.MarineSpecies;

public class Distribution : IEquatable<Distribution>
{
    /// <summary>
    /// The specific description of the place
    /// </summary>
    [JsonPropertyName("locality")]
    [XmlElement("locality")]
    public string Locality { get; set; } = string.Empty;

    /// <summary>
    /// An identifier for the locality. Using the Marine Regions Geographic IDentifier (MRGID), see https://www.marineregions.org/mrgid.php
    /// </summary>
    [JsonPropertyName("locationID")]
    [XmlElement("locationID")]
    public string LocationId { get; set; } = string.Empty;

    /// <summary>
    /// A geographic name less specific than the information captured in the locality term. Possible values: an IHO Sea Area or Nation, derived from the MarineRegions gazetteer
    /// </summary>
    [JsonPropertyName("higherGeography")]
    [XmlElement("higherGeography")]
    public string HigherGeography { get; set; } = string.Empty;

    /// <summary>
    /// An identifier for the geographic region within which the locality occurred, using MRGID
    /// </summary>
    [JsonPropertyName("higherGeographyID")]
    [XmlElement("higherGeographyID")]
    public string HigherGeographyId { get; set; } = string.Empty;

    /// <summary>
    /// The status of the distribution record. Possible values are 'valid' ,'doubtful' or 'inaccurate'. See here for explanation of the statuses
    /// </summary>
    [JsonPropertyName("recordStatus")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [XmlElement("recordStatus")]
    public RecordStatusEnum RecordStatus { get; set; } = RecordStatusEnum.Inaccurate;

    /// <summary>
    /// The type status of the distribution. Possible values: 'holotype' or empty.
    /// </summary>
    [JsonPropertyName("typeStatus")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [XmlElement("typeStatus")]
    public TypeStatusEnum? TypeStatus { get; set; } = TypeStatusEnum.Empty;

    /// <summary>
    /// The process by which the biological individual(s) represented in the Occurrence became established at the location. Possible values: values listed as Origin in WRiMS
    /// </summary>
    [JsonPropertyName("establishmentMeans")]
    [XmlElement("establishmentMeans")]
    public string? EstablishmentMeans { get; set; }

    /// <summary>
    /// The geographic latitude (in decimal degrees, WGS84)
    /// </summary>
    [JsonPropertyName("decimalLatitude")]
    [XmlElement("decimalLatitude")]
    public decimal? DecimalLatitude { get; set; }

    /// <summary>
    /// The geographic longitude (in decimal degrees, WGS84)
    /// </summary>
    [JsonPropertyName("decimalLongitude")]
    [XmlElement("decimalLongitude")]
    public decimal? DecimalLongitude { get; set; }

    /// <summary>
    /// Quality status of the record. Possible values: 'checked', 'trusted' or 'unreviewed'. See https://www.marinespecies.org/aphia.php?p=manual#topic22
    /// </summary>
    [JsonPropertyName("qualityStatus")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [XmlElement("qualityStatus")]
    public QualityStatusEnum? QualityStatus { get; set; } = QualityStatusEnum.Unreviewed;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Locality)}: {Locality}, {nameof(LocationId)}: {LocationId}, {nameof(HigherGeography)}: {HigherGeography}, {nameof(HigherGeographyId)}: {HigherGeographyId}, {nameof(RecordStatus)}: {RecordStatus}, {nameof(TypeStatus)}: {TypeStatus}, {nameof(EstablishmentMeans)}: {EstablishmentMeans}, {nameof(DecimalLatitude)}: {DecimalLatitude}, {nameof(DecimalLongitude)}: {DecimalLongitude}, {nameof(QualityStatus)}: {QualityStatus}";
    }

    public bool Equals(Distribution? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Locality == other.Locality && LocationId == other.LocationId && HigherGeography == other.HigherGeography && HigherGeographyId == other.HigherGeographyId && RecordStatus == other.RecordStatus && TypeStatus == other.TypeStatus && EstablishmentMeans == other.EstablishmentMeans && DecimalLatitude == other.DecimalLatitude && DecimalLongitude == other.DecimalLongitude && QualityStatus == other.QualityStatus;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Distribution)obj);
    }

    public static bool operator ==(Distribution? left, Distribution? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Distribution? left, Distribution? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Locality);
        hashCode.Add(LocationId);
        hashCode.Add(HigherGeography);
        hashCode.Add(HigherGeographyId);
        hashCode.Add((int)RecordStatus);
        hashCode.Add(TypeStatus);
        hashCode.Add(EstablishmentMeans);
        hashCode.Add(DecimalLatitude);
        hashCode.Add(DecimalLongitude);
        hashCode.Add(QualityStatus);
        return hashCode.ToHashCode();
    }
}