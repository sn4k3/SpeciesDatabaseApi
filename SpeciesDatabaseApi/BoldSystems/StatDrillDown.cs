using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class StatDrillDown : IEquatable<StatDrillDown>
{
	[JsonPropertyName("entity")]
	public StatEntity[] Entity { get; set; } = Array.Empty<StatEntity>();

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Entity)}: {Entity}";
	}

	public bool Equals(StatDrillDown? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Entity.Equals(other.Entity);
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((StatDrillDown)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return Entity.GetHashCode();
	}

	public static bool operator ==(StatDrillDown? left, StatDrillDown? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(StatDrillDown? left, StatDrillDown? right)
	{
		return !Equals(left, right);
	}
}