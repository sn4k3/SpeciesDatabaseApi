using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Worms;

public class AttributeValue : IEquatable<AttributeValue>
{
    /// <summary>
    /// An identifier for facts stored in the column measurementValue
    /// </summary>
    [JsonPropertyName("measurementValueID")]
    [XmlElement("measurementValueID")]
    public int MeasurementValueId { get; set; }

    /// <summary>
    /// The value of the measurement, fact, characteristic, or assertion
    /// </summary>
    [JsonPropertyName("measurementValue")]
    [XmlElement("measurementValue")]
    public string MeasurementValue { get; set; } = string.Empty;

    /// <summary>
    /// The identifier for the AphiaSource for this attribute
    /// </summary>
    [JsonPropertyName("measurementValueCode")]
    [XmlElement("measurementValueCode")]
    public string? MeasurementValueCode { get; set; }

    /// <summary>
    /// The possible child attribute keys that help to describe to current attribute
    /// </summary>
    [JsonPropertyName("children")]
    [XmlElement("children")]
    public AttributeValue[] Children { get; set; } = Array.Empty<AttributeValue>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(MeasurementValueId)}: {MeasurementValueId}, {nameof(MeasurementValue)}: {MeasurementValue}, {nameof(MeasurementValueCode)}: {MeasurementValueCode}, Childrens: {Children.Length}";
    }

    public bool Equals(AttributeValue? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return MeasurementValueId == other.MeasurementValueId && MeasurementValue == other.MeasurementValue && MeasurementValueCode == other.MeasurementValueCode && Children.Equals(other.Children);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != this.GetType()) return false;
	    return Equals((AttributeValue)obj);
    }

    public static bool operator ==(AttributeValue? left, AttributeValue? right)
    {
	    return Equals(left, right);
    }

    public static bool operator !=(AttributeValue? left, AttributeValue? right)
    {
	    return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
	    return HashCode.Combine(MeasurementValueId, MeasurementValue, MeasurementValueCode, Children);
    }
}