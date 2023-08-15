using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class SequenceData : IEquatable<SequenceData>
{
    [JsonPropertyName("processid")]
    public string ProcessId { get; set; } = string.Empty;

    [JsonPropertyName("identification")]
    public string Identification { get; set; } = string.Empty;

    [JsonPropertyName("marker")]
    public string Marker { get; set; } = string.Empty;

    [JsonPropertyName("accession")]
    public string Accession { get; set; } = string.Empty;

    [JsonPropertyName("nucleotides")]
    public string Nucleotides { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(ProcessId)}: {ProcessId}, {nameof(Identification)}: {Identification}, {nameof(Marker)}: {Marker}, {nameof(Accession)}: {Accession}, {nameof(Nucleotides)}: {Nucleotides}";
    }

    public bool Equals(SequenceData? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ProcessId == other.ProcessId && Identification == other.Identification && Marker == other.Marker && Accession == other.Accession && Nucleotides == other.Nucleotides;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SequenceData)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(ProcessId, Identification, Marker, Accession, Nucleotides);
    }

    public static bool operator ==(SequenceData? left, SequenceData? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SequenceData? left, SequenceData? right)
    {
        return !Equals(left, right);
    }
}