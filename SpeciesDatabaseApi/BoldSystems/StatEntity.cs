using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class StatEntity : IEquatable<StatEntity>
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("records")]
	public int Records { get; set; }

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Name)}: {Name}, {nameof(Records)}: {Records}";
	}

	public bool Equals(StatEntity? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Name == other.Name && Records == other.Records;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((StatEntity)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Name, Records);
	}

	public static bool operator ==(StatEntity? left, StatEntity? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(StatEntity? left, StatEntity? right)
	{
		return !Equals(left, right);
	}
}