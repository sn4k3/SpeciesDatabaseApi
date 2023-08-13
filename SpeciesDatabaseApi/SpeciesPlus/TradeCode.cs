using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("trade-code")]
public class TradeCode : IEquatable<TradeCode>
{
    /// <summary>
    /// CITES trade code [max 255 characters]
    /// </summary>
    [JsonPropertyName("code")]
    [XmlElement("code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Name (translated based on locale) [max 255 characters]
    /// </summary>
    [JsonPropertyName("name")]
    [XmlElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Code)}: {Code}, {nameof(Name)}: {Name}";
    }

    public bool Equals(TradeCode? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Code == other.Code && Name == other.Name;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TradeCode)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Name);
    }

    public static bool operator ==(TradeCode? left, TradeCode? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TradeCode? left, TradeCode? right)
    {
        return !Equals(left, right);
    }
}