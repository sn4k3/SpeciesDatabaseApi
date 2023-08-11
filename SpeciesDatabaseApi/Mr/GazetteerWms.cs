using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Mr;

public class GazetteerWms : IEquatable<GazetteerWms>
{
    [JsonPropertyName("value")]
    [XmlElement("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("MRGID")]
    [XmlElement("MRGID")]
    public int MrgId { get; set; }

    [JsonPropertyName("url")]
    [XmlElement("url")]
    public Uri? Url { get; set; }

    [JsonPropertyName("namespace")]
    [XmlElement("namespace")]
    public string Namespace { get; set; } = string.Empty;

    [JsonPropertyName("featureType")]
    [XmlElement("featureType")]
    public string FeatureType { get; set; } = string.Empty;

    [JsonPropertyName("featureName")]
    [XmlElement("featureName")]
    public string FeatureName { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Value)}: {Value}, {nameof(MrgId)}: {MrgId}, {nameof(Url)}: {Url}, {nameof(Namespace)}: {Namespace}, {nameof(FeatureType)}: {FeatureType}, {nameof(FeatureName)}: {FeatureName}";
    }

    public bool Equals(GazetteerWms? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value && MrgId == other.MrgId && Equals(Url, other.Url) && Namespace == other.Namespace && FeatureType == other.FeatureType && FeatureName == other.FeatureName;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GazetteerWms)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Value, MrgId, Url, Namespace, FeatureType, FeatureName);
    }

    public static bool operator ==(GazetteerWms? left, GazetteerWms? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GazetteerWms? left, GazetteerWms? right)
    {
        return !Equals(left, right);
    }
}