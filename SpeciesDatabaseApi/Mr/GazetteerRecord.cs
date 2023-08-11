using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.Mr;

public class GazetteerRecord : IEquatable<GazetteerRecord>
{
    [JsonPropertyName("MRGID")]
    public int MrgId { get; set; }

    [JsonPropertyName("gazetteerSource")]
    public string GazetteerSource { get; set; } = string.Empty;

    [JsonPropertyName("placeType")]
    public string PlaceType { get; set; } = string.Empty;

    [JsonPropertyName("latitude")]
    public decimal? Latitude { get; set; } = null;

    [JsonPropertyName("longitude")]
    public decimal? Longitude { get; set; } = null;

    [JsonPropertyName("minLatitude")]
    public decimal? MinLatitude { get; set; } = null;

    [JsonPropertyName("minLongitude")]
    public decimal? MinLongitude { get; set; } = null;

    [JsonPropertyName("maxLatitude")]
    public decimal? MaxLatitude { get; set; } = null;

    [JsonPropertyName("maxLongitude")]
    public decimal? MaxLongitude { get; set; } = null;

    [JsonPropertyName("precision")]
    public decimal? Precision { get; set; } = null;

    [JsonPropertyName("preferredGazetteerName")]
    public string PreferredGazetteerName { get; set; } = string.Empty;

    [JsonPropertyName("preferredGazetteerNameLang")]
    public string PreferredGazetteerNameLang { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("accepted")]
    public int Accepted { get; set; }


    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(MrgId)}: {MrgId}, {nameof(GazetteerSource)}: {GazetteerSource}, {nameof(PlaceType)}: {PlaceType}, {nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}, {nameof(MinLatitude)}: {MinLatitude}, {nameof(MinLongitude)}: {MinLongitude}, {nameof(MaxLatitude)}: {MaxLatitude}, {nameof(MaxLongitude)}: {MaxLongitude}, {nameof(Precision)}: {Precision}, {nameof(PreferredGazetteerName)}: {PreferredGazetteerName}, {nameof(PreferredGazetteerNameLang)}: {PreferredGazetteerNameLang}, {nameof(Status)}: {Status}, {nameof(Accepted)}: {Accepted}";
    }

    /// <inheritdoc />
    public bool Equals(GazetteerRecord? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return MrgId == other.MrgId && GazetteerSource == other.GazetteerSource && PlaceType == other.PlaceType && Latitude == other.Latitude && Longitude == other.Longitude && MinLatitude == other.MinLatitude && MinLongitude == other.MinLongitude && MaxLatitude == other.MaxLatitude && MaxLongitude == other.MaxLongitude && Precision == other.Precision && PreferredGazetteerName == other.PreferredGazetteerName && PreferredGazetteerNameLang == other.PreferredGazetteerNameLang && Status == other.Status && Accepted == other.Accepted;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GazetteerRecord)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(MrgId);
        hashCode.Add(GazetteerSource);
        hashCode.Add(PlaceType);
        hashCode.Add(Latitude);
        hashCode.Add(Longitude);
        hashCode.Add(MinLatitude);
        hashCode.Add(MinLongitude);
        hashCode.Add(MaxLatitude);
        hashCode.Add(MaxLongitude);
        hashCode.Add(Precision);
        hashCode.Add(PreferredGazetteerName);
        hashCode.Add(PreferredGazetteerNameLang);
        hashCode.Add(Status);
        hashCode.Add(Accepted);
        return hashCode.ToHashCode();
    }

    public static bool operator ==(GazetteerRecord? left, GazetteerRecord? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GazetteerRecord? left, GazetteerRecord? right)
    {
        return !Equals(left, right);
    }
}