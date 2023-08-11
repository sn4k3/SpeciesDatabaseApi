using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class ComprehensiveGroup : IEquatable<ComprehensiveGroup>
{
    [JsonPropertyName("group_name")]
    [XmlElement("group_name")]
    public string GroupName { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(GroupName)}: {GroupName}";
    }

    public bool Equals(ComprehensiveGroup? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GroupName == other.GroupName;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ComprehensiveGroup)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return GroupName.GetHashCode();
    }

    public static bool operator ==(ComprehensiveGroup? left, ComprehensiveGroup? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ComprehensiveGroup? left, ComprehensiveGroup? right)
    {
        return !Equals(left, right);
    }
}