using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class Stat : IEquatable<Stat>
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("drill_down")]
    public StatDrillDown? DrillDown { get; set; }


    /// <inheritdoc />
    public override string ToString()
    {
	    return $"{nameof(Count)}: {Count}, {nameof(DrillDown)}: {DrillDown}";
    }

    public bool Equals(Stat? other)
    {
	    if (ReferenceEquals(null, other)) return false;
	    if (ReferenceEquals(this, other)) return true;
	    return Count == other.Count && Equals(DrillDown, other.DrillDown);
    }

    /// <inheritdoc />
	public override bool Equals(object? obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != this.GetType()) return false;
	    return Equals((Stat)obj);
    }

    /// <inheritdoc />
	public override int GetHashCode()
    {
	    return HashCode.Combine(Count, DrillDown);
    }

    public static bool operator ==(Stat? left, Stat? right)
    {
	    return Equals(left, right);
    }

    public static bool operator !=(Stat? left, Stat? right)
    {
	    return !Equals(left, right);
    }
}