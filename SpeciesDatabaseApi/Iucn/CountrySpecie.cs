using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class CountrySpecie : CategorySpecie, IEquatable<CountrySpecie>
{
    /// <summary>
    /// Gets the category classification
    /// </summary>
    [JsonPropertyName("category")]
    [XmlElement("category")]
    public string Category { get; set; } = "NE";

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(Category)}: {Category}";
    }

    public new bool Equals(CountrySpecie? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Category == other.Category;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CountrySpecie)obj);
    }

    public static bool operator ==(CountrySpecie? left, CountrySpecie? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CountrySpecie? left, CountrySpecie? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Category);
    }
}