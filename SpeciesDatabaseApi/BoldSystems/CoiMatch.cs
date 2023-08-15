using System;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

[XmlRoot("match")]
[XmlType("match")]
public class CoiMatch : IEquatable<CoiMatch>
{
	[XmlElement("ID")]
	public string Id { get; set; } = string.Empty;

	[XmlElement("sequencedescription")]
	public string SequenceDescription { get; set; } = string.Empty;

	[XmlElement("database")]
	public string Database { get; set; } = string.Empty;

	[XmlElement("citation")]
	public string Citation { get; set; } = string.Empty;

	[XmlElement("taxonomicidentification")]
	public string TaxonomicIdentification { get; set; } = string.Empty;

	[XmlElement("similarity")]
	public decimal Similarity { get; set; }

	[XmlElement("specimen")]
	public Specimen Specimen { get; set; } = new();

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Id)}: {Id}, {nameof(SequenceDescription)}: {SequenceDescription}, {nameof(Database)}: {Database}, {nameof(Citation)}: {Citation}, {nameof(TaxonomicIdentification)}: {TaxonomicIdentification}, {nameof(Similarity)}: {Similarity}, {nameof(Specimen)}: {Specimen}";
	}

	public bool Equals(CoiMatch? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Id == other.Id && SequenceDescription == other.SequenceDescription && Database == other.Database && Citation == other.Citation && TaxonomicIdentification == other.TaxonomicIdentification && Similarity == other.Similarity && Specimen.Equals(other.Specimen);
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((CoiMatch)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Id, SequenceDescription, Database, Citation, TaxonomicIdentification, Similarity, Specimen);
	}

	public static bool operator ==(CoiMatch? left, CoiMatch? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(CoiMatch? left, CoiMatch? right)
	{
		return !Equals(left, right);
	}
}

