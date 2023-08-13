using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.MarineSpecies;

public class AttributeKey : IEquatable<AttributeKey>
{
    /// <summary>
    /// An internal identifier for the measurementType
    /// </summary>
    [JsonPropertyName("measurementTypeID")]
    [XmlElement("measurementTypeID")]
    public int MeasurementTypeId { get; set; }

    /// <summary>
    /// The nature of the measurement, fact, characteristic, or assertion https://www.marinespecies.org/traits/wiki
    /// </summary>
    [JsonPropertyName("measurementType")]
    [XmlElement("measurementType")]
    public string MeasurementType { get; set; } = string.Empty;

    /// <summary>
    /// The data type that is expected as value for this attribute definition
    /// </summary>
    [JsonPropertyName("input_id")]
    [XmlElement("input_id")]
    public int InputId { get; set; }

    /// <summary>
    /// The category identifier to list possible attribute values for this attribute definition
    /// </summary>
    [JsonPropertyName("CategoryID")]
    [XmlElement("CategoryID")]
    public int? CategoryId { get; set; }

    /// <summary>
    /// The possible child attribute keys that help to describe to current attribute
    /// </summary>
    [JsonPropertyName("children")]
    [XmlElement("children")]
    public AttributeKey[] Children { get; set; } = Array.Empty<AttributeKey>();

    public override string ToString()
    {
        return $"{nameof(MeasurementTypeId)}: {MeasurementTypeId}, {nameof(MeasurementType)}: {MeasurementType}, {nameof(InputId)}: {InputId}, {nameof(CategoryId)}: {CategoryId}, Childrens: {Children.Length}";
    }

    public bool Equals(AttributeKey? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return MeasurementTypeId == other.MeasurementTypeId && MeasurementType == other.MeasurementType && InputId == other.InputId && CategoryId == other.CategoryId && Children.Equals(other.Children);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((AttributeKey)obj);
    }

    public static bool operator ==(AttributeKey? left, AttributeKey? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(AttributeKey? left, AttributeKey? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(MeasurementTypeId, MeasurementType, InputId, CategoryId, Children);
    }
}