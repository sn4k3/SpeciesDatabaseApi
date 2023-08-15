using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class TraceFiles : IEquatable<TraceFiles>
{
    [JsonPropertyName("read")]
    public TraceFile[] Traces { get; set; } = Array.Empty<TraceFile>();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Traces)}: {Traces.Length}";
    }

    public bool Equals(TraceFiles? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Traces.Equals(other.Traces);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TraceFiles)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return Traces.GetHashCode();
    }

    public static bool operator ==(TraceFiles? left, TraceFiles? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TraceFiles? left, TraceFiles? right)
    {
        return !Equals(left, right);
    }
}