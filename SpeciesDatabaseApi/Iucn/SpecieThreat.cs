using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class SpecieThreat : IEquatable<SpecieThreat>
{
	[JsonPropertyName("code")]
	[XmlElement("code")]
	public string Code { get; set; } = string.Empty;

	[JsonPropertyName("title")]
	[XmlElement("title")]
	public string Title { get; set; } = string.Empty;

	[JsonPropertyName("timing")]
	[XmlElement("timing")]
	public string Timing { get; set; } = string.Empty;

	[JsonPropertyName("scope")]
	[XmlElement("scope")]
	public string? Scope { get; set; }

	[JsonPropertyName("severity")]
	[XmlElement("severity")]
	public string? Severity { get; set; }

	[JsonPropertyName("score")]
	[XmlElement("score")]
	public string? Score { get; set; }

	[JsonPropertyName("invasive")]
	[XmlElement("invasive")]
	public string? Invasive { get; set; }

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{nameof(Code)}: {Code}, {nameof(Title)}: {Title}, {nameof(Timing)}: {Timing}, {nameof(Scope)}: {Scope}, {nameof(Severity)}: {Severity}, {nameof(Score)}: {Score}, {nameof(Invasive)}: {Invasive}";
	}

	public bool Equals(SpecieThreat? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return Code == other.Code && Title == other.Title && Timing == other.Timing && Scope == other.Scope && Severity == other.Severity && Score == other.Score && Invasive == other.Invasive;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((SpecieThreat)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(Code, Title, Timing, Scope, Severity, Score, Invasive);
	}

	public static bool operator ==(SpecieThreat? left, SpecieThreat? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(SpecieThreat? left, SpecieThreat? right)
	{
		return !Equals(left, right);
	}
}