using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Worms;

public class Attribute : IEquatable<Attribute>
{
    /// <summary>
    /// Unique and persistent identifier within WoRMS
    /// </summary>
    [JsonPropertyName("AphiaID")]
    [XmlElement("AphiaID")]
    public int AphiaId { get; set; }

    /// <summary>
    /// The corresponding AttributeKey its MeasurementTypeID
    /// </summary>
    [JsonPropertyName("measurementTypeID")]
    [XmlElement("measurementTypeID")]
    public int MeasurementTypeId { get; set; }

    /// <summary>
    /// The corresponding AttributeKey its MeasurementType
    /// </summary>
    [JsonPropertyName("measurementType")]
    [XmlElement("measurementType")]
    public string MeasurementType { get; set; } = string.Empty;

    /// <summary>
    /// The value of the measurement, fact, characteristic, or assertion
    /// </summary>
    [JsonPropertyName("measurementValue")]
    [XmlElement("measurementValue")]
    public string MeasurementValue { get; set; } = string.Empty;

    /// <summary>
    /// The identifier for the AphiaSource for this attribute
    /// </summary>
    [JsonPropertyName("source_id")]
    [XmlElement("source_id")]
    public int SourceId { get; set; }

    /// <summary>
    /// The AphiaSource reference for this attribute
    /// </summary>
    [JsonPropertyName("reference")]
    [XmlElement("reference")]
    public string Reference { get; set; } = string.Empty;

    /// <summary>
    /// The category identifier to list possible attribute values for this attribute definition
    /// </summary>
    [JsonPropertyName("qualitystatus")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [XmlElement("qualitystatus")]
    public QualityStatusEnum QualityStatus { get; set; } = QualityStatusEnum.Unreviewed;

    /// <summary>
    /// The category identifier to list possible attribute values for this attribute definition
    /// </summary>
    [JsonPropertyName("CategoryID")]
    [XmlElement("CategoryID")]
    public int? CategoryId { get; set; }

    /// <summary>
    /// The AphiaID from where this attribute is inherited
    /// </summary>
    [JsonPropertyName("AphiaID_Inherited")]
    [XmlElement("AphiaID_Inherited")]
    public int AphiaIdInherited { get; set; }

    /// <summary>
    /// The possible child attribute keys that help to describe to current attribute
    /// </summary>
    [JsonPropertyName("children")]
    [XmlElement("children")]
    public Attribute[] Children { get; set; } = Array.Empty<Attribute>();


    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(AphiaId)}: {AphiaId}, {nameof(MeasurementTypeId)}: {MeasurementTypeId}, {nameof(MeasurementType)}: {MeasurementType}, {nameof(MeasurementValue)}: {MeasurementValue}, {nameof(SourceId)}: {SourceId}, {nameof(Reference)}: {Reference}, {nameof(QualityStatus)}: {QualityStatus}, {nameof(CategoryId)}: {CategoryId}, {nameof(AphiaIdInherited)}: {AphiaIdInherited}, Childrens: {Children.Length}";
    }

    public bool Equals(Attribute? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return AphiaId == other.AphiaId && MeasurementTypeId == other.MeasurementTypeId && MeasurementType == other.MeasurementType && MeasurementValue == other.MeasurementValue && SourceId == other.SourceId && Reference == other.Reference && QualityStatus == other.QualityStatus && CategoryId == other.CategoryId && AphiaIdInherited == other.AphiaIdInherited && Children.Equals(other.Children);
    }

    public static bool operator ==(Attribute? left, Attribute? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Attribute? left, Attribute? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Attribute)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(AphiaId);
        hashCode.Add(MeasurementTypeId);
        hashCode.Add(MeasurementType);
        hashCode.Add(MeasurementValue);
        hashCode.Add(SourceId);
        hashCode.Add(Reference);
        hashCode.Add((int)QualityStatus);
        hashCode.Add(CategoryId);
        hashCode.Add(AphiaIdInherited);
        hashCode.Add(Children);
        return hashCode.ToHashCode();
    }
}