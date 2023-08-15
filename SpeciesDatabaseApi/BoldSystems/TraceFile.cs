using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TraceFile : IEquatable<TraceFile>
{
    [JsonPropertyName("trace_id")]
    public int TraceId { get; set; }

    [JsonPropertyName("run_date")]
    public DateTime RunDate { get; set; }

    [JsonPropertyName("sequencing_center")]
    public string SequencingCenter { get; set; } = string.Empty;

    [JsonPropertyName("direction")]
    public char Direction { get; set; } = char.MinValue;

    [JsonPropertyName("seq_primer")]
    public string SeqPrimer { get; set; } = string.Empty;

    [JsonPropertyName("trace_name")]
    public Uri? TraceName { get; set; }

    [JsonPropertyName("markercode")]
    public string MarkerCode { get; set; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(TraceId)}: {TraceId}, {nameof(RunDate)}: {RunDate}, {nameof(SequencingCenter)}: {SequencingCenter}, {nameof(Direction)}: {Direction}, {nameof(SeqPrimer)}: {SeqPrimer}, {nameof(TraceName)}: {TraceName}, {nameof(MarkerCode)}: {MarkerCode}";
    }

    public bool Equals(TraceFile? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TraceId == other.TraceId && RunDate.Equals(other.RunDate) && SequencingCenter == other.SequencingCenter && Direction == other.Direction && SeqPrimer == other.SeqPrimer && Equals(TraceName, other.TraceName) && MarkerCode == other.MarkerCode;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TraceFile)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(TraceId, RunDate, SequencingCenter, Direction, SeqPrimer, TraceName, MarkerCode);
    }

    public static bool operator ==(TraceFile? left, TraceFile? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TraceFile? left, TraceFile? right)
    {
        return !Equals(left, right);
    }
}