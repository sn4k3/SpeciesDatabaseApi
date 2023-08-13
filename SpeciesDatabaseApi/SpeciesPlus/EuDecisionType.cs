using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("eu-decision-type")]
public class EuDecisionType : IEquatable<EuDecisionType>
{
    /// <summary>
    /// Name of decision type, e.g. Suspension (a), Negative, No opinion
    /// </summary>
    [JsonPropertyName("name")]
    [XmlElement("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Additional description where available [max 255 characters]
    /// </summary>
    [JsonPropertyName("description")]
    [XmlElement("description")]
    public string? Description { get; set; }

    /// <summary>
    /// One of SUSPENSION, POSITIVE_OPINION, NEGATIVE_OPINION, NO_OPINION 
    /// </summary>
    [JsonPropertyName("type")]
    [XmlElement("type")]
    public string? Type { get; set; }


    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Type)}: {Type}";
    }

    public bool Equals(EuDecisionType? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Description == other.Description && Type == other.Type;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EuDecisionType)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Description, Type);
    }

    public static bool operator ==(EuDecisionType? left, EuDecisionType? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EuDecisionType? left, EuDecisionType? right)
    {
        return !Equals(left, right);
    }
}