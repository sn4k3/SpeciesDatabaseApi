using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.SpeciesPlus;

[XmlRoot("hash")]
public class TaxonConcepts : IEquatable<TaxonConcepts>
{
    [JsonPropertyName("pagination")]
    [XmlElement("pagination")]
    public Pagination Pagination { get; set; } = new ();

	[JsonPropertyName("taxon_concepts")]
	[XmlElement("taxon-concepts")]
	public TaxonConcept[] Concepts { get; set; } = Array.Empty<TaxonConcept>();

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Pagination)}: {Pagination}, {nameof(Concepts)}: {Concepts.Length}";
	}

	public bool Equals(TaxonConcepts? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Pagination.Equals(other.Pagination) && Concepts.Equals(other.Concepts);
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((TaxonConcepts)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Pagination, Concepts);
	}

	public static bool operator ==(TaxonConcepts? left, TaxonConcepts? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(TaxonConcepts? left, TaxonConcepts? right)
	{
		return !Equals(left, right);
	}
}