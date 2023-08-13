using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("cites-legislation")]
public class CitesLegislation : IEquatable<CitesLegislation>
{
    [JsonPropertyName("cites_listings")]
    [XmlElement("cites-listings")]
    public CitesListing[] CitesListings { get; set; } = Array.Empty<CitesListing>();

    [JsonPropertyName("cites_quotas")]
    [XmlElement("cites-quotas")]
    public CitesQuota[] CitesQuotas { get; set; } = Array.Empty<CitesQuota>();

    [JsonPropertyName("cites_suspensions")]
    [XmlElement("cites-suspensions")]
    public CitesSuspension[] CitesSuspensions { get; set; } = Array.Empty<CitesSuspension>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(CitesListings)}: {CitesListings.Length}, {nameof(CitesQuotas)}: {CitesQuotas.Length}, {nameof(CitesSuspensions)}: {CitesSuspensions.Length}";
    }

    public bool Equals(CitesLegislation? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return CitesListings.Equals(other.CitesListings) && CitesQuotas.Equals(other.CitesQuotas) && CitesSuspensions.Equals(other.CitesSuspensions);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((CitesLegislation)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(CitesListings, CitesQuotas, CitesSuspensions);
    }

    public static bool operator ==(CitesLegislation? left, CitesLegislation? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CitesLegislation? left, CitesLegislation? right)
    {
        return !Equals(left, right);
    }
}