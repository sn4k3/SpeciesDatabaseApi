using System.Text.Json.Serialization;
using System.Xml.Serialization;
using System;

namespace SpeciesDatabaseApi.Iucn;

public class CategorySpecie : IEquatable<CategorySpecie>
{
	/// <summary>
	/// Gets the taxonid for the record
	/// </summary>
	[JsonPropertyName("taxonid")]
	[XmlElement("taxonid")]
	public int TaxonId { get; set; }

	[JsonPropertyName("scientific_name")]
	[XmlElement("scientific_name")]
	public string ScientificName { get; set; } = string.Empty;

	[JsonPropertyName("subspecies")]
	[XmlElement("subspecies")]
	public string? SubSpecies { get; set; }

	[JsonPropertyName("rank")]
	[XmlElement("rank")]
	public string? Rank { get; set; }

	[JsonPropertyName("subpopulation")]
	[XmlElement("subpopulation")]
	public string? SubPopulation { get; set; }

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(TaxonId)}: {TaxonId}, {nameof(ScientificName)}: {ScientificName}, {nameof(SubSpecies)}: {SubSpecies}, {nameof(Rank)}: {Rank}, {nameof(SubPopulation)}: {SubPopulation}";
	}

	protected bool Equals(CountrySpecie other)
	{
		return TaxonId == other.TaxonId && ScientificName == other.ScientificName && SubSpecies == other.SubSpecies && SubPopulation == other.SubPopulation;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((CategorySpecie)obj);
	}

	public bool Equals(CategorySpecie? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return TaxonId == other.TaxonId && ScientificName == other.ScientificName && SubSpecies == other.SubSpecies && Rank == other.Rank && SubPopulation == other.SubPopulation;
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(TaxonId, ScientificName, SubSpecies, Rank, SubPopulation);
	}

	public static bool operator ==(CategorySpecie? left, CategorySpecie? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(CategorySpecie? left, CategorySpecie? right)
	{
		return !Equals(left, right);
	}
}