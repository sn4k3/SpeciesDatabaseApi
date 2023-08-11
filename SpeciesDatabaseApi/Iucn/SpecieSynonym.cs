using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieSynonym : IEquatable<SpecieSynonym>
{
    [JsonPropertyName("accepted_id")]
    [XmlElement("accepted_id")]
    public int AcceptedId { get; set; }

    [JsonPropertyName("accepted_name")]
    [XmlElement("accepted_name")]
    public string AcceptedName { get; set; } = string.Empty;

    [JsonPropertyName("authority")]
    [XmlElement("authority")]
    public string Authority { get; set; } = string.Empty;

    [JsonPropertyName("synonym")]
    [XmlElement("synonym")]
    public string Synonym { get; set; } = string.Empty;

    [JsonPropertyName("syn_authority")]
    [XmlElement("syn_authority")]
    public string? SynAuthority { get; set; }


    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(AcceptedId)}: {AcceptedId}, {nameof(AcceptedName)}: {AcceptedName}, {nameof(Authority)}: {Authority}, {nameof(Synonym)}: {Synonym}, {nameof(SynAuthority)}: {SynAuthority}";
    }

    /// <inheritdoc />
    public bool Equals(SpecieSynonym? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return AcceptedId == other.AcceptedId && AcceptedName == other.AcceptedName && Authority == other.Authority && Synonym == other.Synonym && SynAuthority == other.SynAuthority;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SpecieSynonym)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(AcceptedId, AcceptedName, Authority, Synonym, SynAuthority);
    }

    public static bool operator ==(SpecieSynonym? left, SpecieSynonym? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SpecieSynonym? left, SpecieSynonym? right)
    {
        return !Equals(left, right);
    }
}