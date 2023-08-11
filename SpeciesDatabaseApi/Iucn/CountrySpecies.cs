using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class CountrySpecies : ArrayResult<CountrySpecie>, IEquatable<CountrySpecies>
{
    [JsonPropertyName("country")]
    [XmlElement("country")]
    public string CountryCode { get; set; } = string.Empty;
    
    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(CountryCode)}: {CountryCode}, {base.ToString()}";
    }

    public bool Equals(CountrySpecies? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && CountryCode == other.CountryCode;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CountrySpecies)obj);
    }

    public static bool operator ==(CountrySpecies? left, CountrySpecies? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CountrySpecies? left, CountrySpecies? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), CountryCode);
    }
}