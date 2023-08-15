using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class SpecimenRecord : IEquatable<SpecimenRecord>
{
    [JsonPropertyName("record_id")]
    public string RecordId { get; set; } = string.Empty;

    [JsonPropertyName("processid")]
    public string ProcessId { get; set; } = string.Empty;

    [JsonPropertyName("bin_uri")]
    public string? BinUri { get; set; }

    [JsonPropertyName("specimen_identifiers")]
    public SpecimenIdentifiers SpecimenIdentifiers { get; set; } = new();

    [JsonPropertyName("taxomony")]
    public Taxomony Taxomony { get; set; } = new();

    [JsonPropertyName("specimen_desc")]
    public SpecimenDesc SpecimenDesc { get; set; } = new ();

    [JsonPropertyName("collection_event")]
    public CollectionEvent CollectionEvent { get; set; } = new();

    [JsonPropertyName("tracefiles")]
    public TraceFiles? TraceFiles { get; set; }

    [JsonPropertyName("sequences")]
    public Sequences? Sequences { get; set; }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(RecordId)}: {RecordId}, {nameof(ProcessId)}: {ProcessId}, {nameof(BinUri)}: {BinUri}, {nameof(SpecimenIdentifiers)}: {SpecimenIdentifiers}, {nameof(Taxomony)}: {Taxomony}, {nameof(SpecimenDesc)}: {SpecimenDesc}, {nameof(CollectionEvent)}: {CollectionEvent}, {nameof(TraceFiles)}: {TraceFiles}, {nameof(Sequences)}: {Sequences}, {nameof(Notes)}: {Notes}";
    }

    public bool Equals(SpecimenRecord? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return RecordId == other.RecordId && ProcessId == other.ProcessId && BinUri == other.BinUri && SpecimenIdentifiers.Equals(other.SpecimenIdentifiers) && Taxomony.Equals(other.Taxomony) && SpecimenDesc.Equals(other.SpecimenDesc) && CollectionEvent.Equals(other.CollectionEvent) && Equals(TraceFiles, other.TraceFiles) && Equals(Sequences, other.Sequences) && Notes == other.Notes;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SpecimenRecord)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(RecordId);
        hashCode.Add(ProcessId);
        hashCode.Add(BinUri);
        hashCode.Add(SpecimenIdentifiers);
        hashCode.Add(Taxomony);
        hashCode.Add(SpecimenDesc);
        hashCode.Add(CollectionEvent);
        hashCode.Add(TraceFiles);
        hashCode.Add(Sequences);
        hashCode.Add(Notes);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(SpecimenRecord? left, SpecimenRecord? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SpecimenRecord? left, SpecimenRecord? right)
    {
        return !Equals(left, right);
    }
}