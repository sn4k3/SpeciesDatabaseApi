using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class PlantGrowthForm : IEquatable<PlantGrowthForm>
{
	[JsonPropertyName("name")]
	[XmlElement("name")]
	public string Name { get; set; } = string.Empty;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Name)}: {Name}";
	}

	public bool Equals(PlantGrowthForm? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Name == other.Name;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((PlantGrowthForm)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return Name.GetHashCode();
	}

	public static bool operator ==(PlantGrowthForm? left, PlantGrowthForm? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(PlantGrowthForm? left, PlantGrowthForm? right)
	{
		return !Equals(left, right);
	}
}