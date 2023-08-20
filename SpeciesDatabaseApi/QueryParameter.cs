using System.Collections.Generic;
using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace SpeciesDatabaseApi;

public class QueryParameter : IEquatable<QueryParameter>
{
    /// <summary>
    /// The key of the parameter
    /// </summary>
    public string Key { get; init; }

    /// <summary>
    /// The value of the parameter
    /// </summary>
    public object? Value { get; init; }

    /// <summary>
    /// Enforce a case conversion
    /// </summary>
    public QueryParameterCase ValueCase { get; init; }

    public QueryParameter(string key, object? value, QueryParameterCase valueCase = QueryParameterCase.None)
    {
        Key = key;
        Value = value;
        ValueCase = valueCase;
    }

    public QueryParameter(KeyValuePair<string, object?> keyValuePair, QueryParameterCase valueCase = QueryParameterCase.None) : this(keyValuePair.Key, keyValuePair.Value, valueCase)
    {
    }

    public override string ToString()
    {
        var value = ValueCase switch
        {
            QueryParameterCase.None => Value?.ToString(),
            QueryParameterCase.Lower => Value?.ToString()?.ToLower(),
            QueryParameterCase.Upper => Value?.ToString()?.ToUpper(),
            _ => throw new ArgumentOutOfRangeException()
        };
        return $"{Key}={value}";
    }

    public string? ToUrlString()
    {
        string? value;
        switch (Value)
        {
            case null:
                return null;
            case IList list:
                if (list.Count == 0) return null;

                var items = new List<string>(list.Count);
                foreach (var obj in list)
                {
                    if (obj is null) continue;
                    value = obj.ToString()?.Trim();
                    if (string.IsNullOrWhiteSpace(value)) continue;
                    items.Add(value);
                }

                if (list.Count == 0) return null;

                // Key as array, handle differently
                if (Key.EndsWith("[]"))
                {
                    var sb = new StringBuilder();
                    foreach (var arrayItem in items)
                    {
                        if (sb.Length > 0) sb.Append('&');

                        value = ValueCase switch
                        {
                            QueryParameterCase.None => arrayItem,
                            QueryParameterCase.Lower => arrayItem.ToLower(),
                            QueryParameterCase.Upper => arrayItem.ToUpper(),
                            _ => throw new ArgumentOutOfRangeException()
                        };
                        sb.Append($"{Uri.EscapeDataString(Key)}={Uri.EscapeDataString(value)}");
                    }

                    return sb.ToString();
                }

                value = string.Join(',', items);
                break;
            case Enum enumValue:
                if (enumValue.GetType().IsDefined(typeof(FlagsAttribute), false))
                {
                    // Treat as list
                    var enumList = Enum.GetValues(enumValue.GetType()).Cast<Enum>().Where(enumValue.HasFlag);
                    value = string.Join(',', enumList);
                }
                else
                {
                    // Treat as string
                    value = enumValue.ToString();
                }
                break;
            case bool boolValue:
                value = boolValue.ToString().ToLowerInvariant();
                break;
            case string str:
                value = str.Trim();
                if (string.IsNullOrWhiteSpace(value)) return null;
                break;
            default:
                value = Value.ToString()?.Trim();
                if (string.IsNullOrWhiteSpace(value)) return null;
                break;
        }

        value = ValueCase switch
        {
            QueryParameterCase.None => value,
            QueryParameterCase.Lower => value.ToLower(),
            QueryParameterCase.Upper => value.ToUpper(),
            _ => throw new ArgumentOutOfRangeException()
        };

        return $"{Uri.EscapeDataString(Key)}={Uri.EscapeDataString(value)}";
    }

    public bool Equals(QueryParameter? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Key == other.Key;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((QueryParameter)obj);
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    public static bool operator ==(QueryParameter? left, QueryParameter? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(QueryParameter? left, QueryParameter? right)
    {
        return !Equals(left, right);
    }
}