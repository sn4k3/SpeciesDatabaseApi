using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class Sequence : IEquatable<Sequence>
{
    [JsonPropertyName("sequenceID")]
    public int SequenceId { get; set; }

    [JsonPropertyName("markercode")]
    public string MarkerCode { get; set; } = string.Empty;

    [JsonPropertyName("genbank_accession")]
    public string GenBankAccession { get; set; } = string.Empty;

    [JsonPropertyName("nucleotides")]
    public string Nucleotides { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(SequenceId)}: {SequenceId}, {nameof(MarkerCode)}: {MarkerCode}, {nameof(GenBankAccession)}: {GenBankAccession}, {nameof(Nucleotides)}: {Nucleotides}";
    }

    public bool Equals(Sequence? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return SequenceId == other.SequenceId && MarkerCode == other.MarkerCode && GenBankAccession == other.GenBankAccession && Nucleotides == other.Nucleotides;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Sequence)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(SequenceId, MarkerCode, GenBankAccession, Nucleotides);
    }

    public static bool operator ==(Sequence? left, Sequence? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Sequence? left, Sequence? right)
    {
        return !Equals(left, right);
    }
}