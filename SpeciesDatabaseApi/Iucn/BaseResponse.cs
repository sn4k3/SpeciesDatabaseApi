using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;

public class BaseResponse : IEquatable<BaseResponse>
{
    [JsonPropertyName("error")]
    [XmlElement("error")]
    public string? Error { get; set; }

    [JsonPropertyName("message")]
    [XmlElement("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets if this response returned the data correctly or if have error or message
    /// </summary>
    public bool IsSuccess => string.IsNullOrEmpty(Error) && string.IsNullOrEmpty(Message);

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Error)}: {Error}, {nameof(Message)}: {Message}, {nameof(IsSuccess)}: {IsSuccess}";
    }

    public bool Equals(BaseResponse? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Error == other.Error && Message == other.Message;
    }

    public static bool operator ==(BaseResponse? left, BaseResponse? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(BaseResponse? left, BaseResponse? right)
    {
        return !Equals(left, right);
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BaseResponse)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Error, Message);
    }
}