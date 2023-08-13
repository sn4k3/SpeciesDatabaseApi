using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

/// <summary>
/// Lists current EU annex listings, SRG opinions, and EU suspensions for a given taxon concept
/// </summary>
[XmlRoot("eu-legislation")]
public class EuLegislation : IEquatable<EuLegislation>
{
    [JsonPropertyName("eu_listings")]
    [XmlElement("eu-listings")]
    public EuListing[] Listings { get; set; } = Array.Empty<EuListing>();

    [JsonPropertyName("eu_decisions")]
    [XmlElement("eu-decisions")]
    public EuDecision[] Decisions { get; set; } = Array.Empty<EuDecision>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Listings)}: {Listings}, {nameof(Decisions)}: {Decisions}";
    }

    public bool Equals(EuLegislation? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Listings.Equals(other.Listings) && Decisions.Equals(other.Decisions);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EuLegislation)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Listings, Decisions);
    }

    public static bool operator ==(EuLegislation? left, EuLegislation? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EuLegislation? left, EuLegislation? right)
    {
        return !Equals(left, right);
    }
}