using System;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

public class SpecimenDesc : IEquatable<SpecimenDesc>
{
    [JsonPropertyName("voucher_status")]
    public string VoucherStatus { get; set; } = string.Empty;

    [JsonPropertyName("reproduction")]
    public char? Reproduction { get; set; }

    [JsonPropertyName("sex")]
    public char? Sex { get; set; }

    [JsonPropertyName("lifestage")]
    public string? LifeStage { get; set; }

    [JsonPropertyName("tissue_type")]
    public string? TissueType { get; set; }

    [JsonPropertyName("extrainfo")]
    public string? ExtraInfo { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(VoucherStatus)}: {VoucherStatus}, {nameof(Reproduction)}: {Reproduction}, {nameof(Sex)}: {Sex}, {nameof(LifeStage)}: {LifeStage}, {nameof(TissueType)}: {TissueType}, {nameof(ExtraInfo)}: {ExtraInfo}";
    }

    public bool Equals(SpecimenDesc? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return VoucherStatus == other.VoucherStatus && Reproduction == other.Reproduction && Sex == other.Sex && LifeStage == other.LifeStage && TissueType == other.TissueType && ExtraInfo == other.ExtraInfo;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SpecimenDesc)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(VoucherStatus, Reproduction, Sex, LifeStage, TissueType, ExtraInfo);
    }

    public static bool operator ==(SpecimenDesc? left, SpecimenDesc? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(SpecimenDesc? left, SpecimenDesc? right)
    {
        return !Equals(left, right);
    }
}