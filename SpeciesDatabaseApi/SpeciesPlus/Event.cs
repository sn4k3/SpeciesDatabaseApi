using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("event")]
public class Event : IEquatable<Event>
{
    [JsonPropertyName("name")]
    [XmlElement("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    [XmlElement("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    [XmlElement("url")]
    public Uri? Url { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Date)}: {Date}, {nameof(Url)}: {Url}";
    }

    public bool Equals(Event? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Date == other.Date && Equals(Url, other.Url);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Event)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Date, Url);
    }

    public static bool operator ==(Event? left, Event? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Event? left, Event? right)
    {
        return !Equals(left, right);
    }
}