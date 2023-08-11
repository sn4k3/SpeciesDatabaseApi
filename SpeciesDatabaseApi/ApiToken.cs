﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace SpeciesDatabaseApi;

/// <summary>
/// Represents an API token to send with the request
/// </summary>
public class ApiToken
{
    #region Members

    private string? _value;

    #endregion

    #region Properties

    /// <summary>
    /// Gets if this token is actually required by the client or not
    /// </summary>
    public bool IsRequired { get; private set; }

    /// <summary>
    /// Gets the key name of the token
    /// </summary>
    public string Key { get; private set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value of the token
    /// </summary>
    public string? Value
    {
        get => _value;
        set
        {
            _value = value;
            IsRequired = HasValue;
        }
    }

    /// <summary>
    /// Gets where the token will be placed within the request
    /// </summary>
    public ApiTokenPlacement Placement { get; private set; }

    /// <summary>
    /// Gets if this token have a value set / is valid
    /// </summary>
    [MemberNotNullWhen(true, nameof(Value))]
    public bool HasValue => !string.IsNullOrWhiteSpace(Value);

    /// <summary>
    /// Gets if this token can be used by a request, ie: <see cref="IsRequired"/> && <see cref="HasValue"/>
    /// </summary>
    [MemberNotNullWhen(true, nameof(Value))]
    public bool CanUse => IsRequired && HasValue;

    #endregion

    #region Constructors

    public ApiToken()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="key">The key name of the token</param>
    /// <param name="value">The value of the token</param>
    /// <param name="placement">Where the token will be placed within the request</param>
    public ApiToken(string key, string value, ApiTokenPlacement placement = ApiTokenPlacement.Manual)
    {
        IsRequired = true;
        Key = key;
        Value = value;
        Placement = placement;
    }

    public void Deconstruct(out string key, out string? value)
    {
        key = Key;
        value = Value;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets all the token properties
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="placement"></param>
    protected internal void Set(string key, string value, ApiTokenPlacement placement = ApiTokenPlacement.Manual)
    {
        Key = key;
        Value = value;
        Placement = placement;
    }

    #endregion

    #region Equality and Format

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(Key)}: {Key}, {nameof(Value)}: {Value}, {nameof(Placement)}: {Placement}";
    }

    protected bool Equals(ApiToken other)
    {
        return Key == other.Key && Value == other.Value && Placement == other.Placement;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ApiToken)obj);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Key, Value, (int)Placement);
    }

    #endregion
}