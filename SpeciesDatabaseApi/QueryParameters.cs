using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace SpeciesDatabaseApi;

public enum QueryParametersSpecialType
{
    /// <summary>
    /// Adds all possible class properties using reflection
    /// </summary>
    ClassPropertiesReflection
}

public enum QueryParameterCase
{
    /// <summary>
    /// As set, do not force case
    /// </summary>
    None,

    /// <summary>
    /// Force lower case
    /// </summary>
    Lower,

    /// <summary>
    /// Force upper case
    /// </summary>
    Upper,
}

/// <summary>
/// Handles query parameters and build the string, avoiding any null value
/// </summary>
public class QueryParameters : List<QueryParameter>
{
    public QueryParameters()
    {
    }

    public QueryParameters(IEnumerable<QueryParameter> collection) : base(collection)
    {
    }

    public QueryParameters(int capacity) : base(capacity)
    {
    }

    /// <summary>
    /// Adds the key and it value to this collection
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public QueryParameters(string key, object? value, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None) : this(1)
    {
        Add(key, value, distinct, valueCase);
    }

    /// <summary>
    /// Adds the <see cref="KeyValuePair{TKey,TValue}"/> to this collection
    /// </summary>
    /// <param name="keyValuePair"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public QueryParameters(KeyValuePair<string, object?> keyValuePair, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None) : this(1)
    {
        Add(keyValuePair, distinct, valueCase);
    }

    /// <summary>
    /// Adds the list of <see cref="KeyValuePair{TKey,TValue}"/> to this collection
    /// </summary>
    /// <param name="list"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public QueryParameters(IEnumerable<KeyValuePair<string, object?>> list, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None) : this(list.Count())
    {
        Add(list, distinct, valueCase);
    }


    /// <summary>
    /// Adds the dictionary contents to this collection
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public QueryParameters(IReadOnlyDictionary<string, object?> dictionary, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None) : this(dictionary.Count)
    {
        Add(dictionary, distinct, valueCase);
    }


    /// <summary>
    /// Adds all possible properties from a class, where property name is key and it value the value
    /// </summary>
    /// <param name="specialType"></param>
    /// <param name="classObj">Class object to fetch properties values from</param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="mustHaveSetter">True if the property must have an setter in addition to the getter</param>
    public QueryParameters(QueryParametersSpecialType specialType, object? classObj, bool distinct = false, bool mustHaveSetter = false)
    {
        Add(specialType, classObj, distinct, mustHaveSetter);
    }

    /// <summary>
    /// Adds the key and it value to this collection
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public void Add(string key, object? value, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None)
    {
        if (value is null) return;
        if (distinct && this.Any(queryParameter => queryParameter.Key == key)) return;
        Add(new QueryParameter(key, value, valueCase));
    }

    /// <summary>
    /// Adds the <see cref="KeyValuePair{TKey,TValue}"/> to this collection
    /// </summary>
    /// <param name="keyValuePair"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public void Add(KeyValuePair<string, object?> keyValuePair, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None)
    {
        Add(keyValuePair.Key, keyValuePair.Value, distinct, valueCase);
    }

    /// <summary>
    /// Adds the list of <see cref="KeyValuePair{TKey,TValue}"/> to this collection
    /// </summary>
    /// <param name="list"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public void Add(IEnumerable<KeyValuePair<string, object?>> list, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None)
    {
        foreach (var keyValuePair in list)
        {
            Add(keyValuePair, distinct, valueCase);
        }
    }

    /// <summary>
    /// Adds the dictionary contents to this collection
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="valueCase">Enforce a value case conversion</param>
    public void Add(IReadOnlyDictionary<string, object?> dictionary, bool distinct = false, QueryParameterCase valueCase = QueryParameterCase.None)
    {
        foreach (var keyValuePair in dictionary)
        {
            Add(keyValuePair, distinct, valueCase);
        }
    }

    /// <summary>
    /// Adds all possible properties from a class, where property name is key and it value the value
    /// </summary>
    /// <param name="specialType"></param>
    /// <param name="classObj">Class object to fetch properties values from</param>
    /// <param name="distinct">True to only add distinct keys</param>
    /// <param name="mustHaveSetter">True if the property must have an setter in addition to the getter</param>
    public void Add(QueryParametersSpecialType specialType, object? classObj, bool distinct = false, bool mustHaveSetter = false)
    {
        if (classObj is null) return;

        switch (specialType)
        {
            case QueryParametersSpecialType.ClassPropertiesReflection:
                var properties = classObj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in properties)
                {
                    var getMethod = propertyInfo.GetMethod;
                    if (getMethod is null) continue;

                    if (mustHaveSetter && propertyInfo.SetMethod is null) continue;

                    var value = propertyInfo.GetValue(classObj);
                    if (value is null) continue;

                    var attr = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
                    Add(attr?.Name ?? getMethod.Name, value, distinct);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(specialType), specialType, null);
        }
    }

    public void Remove(string key)
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            if (string.Equals(this[i].Key, key, StringComparison.Ordinal)) RemoveAt(i);
        }
    }

    /// <summary>
    /// Sorts the collection by the key name
    /// </summary>
    public new void Sort()
    {
        Sort((a, b) => string.Compare(a.Key, b.Key, StringComparison.Ordinal));
    }

    /// <summary>
    /// Gets the query url only for parameters
    /// </summary>
    /// <returns>A query string, which can be empty, if not it will start with a '?' char</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();

        // Sort the parameters alphabetically to avoid http redirection.
        foreach (var item in this.OrderBy(x => x.Key))
        {
            string? query = item.ToUrlString();
            if (string.IsNullOrWhiteSpace(query)) continue;
            sb.Append($"{(sb.Length == 0 ? "?" : "&")}{query}");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the query url, always use this method instead of <see cref="ToString()"/> without argument
    /// </summary>
    /// <param name="client">The <see cref="BaseClient"/> to fetch other others such as ApiToken</param>
    /// <returns>A query string, which can be empty, if not it will start with a '?' char</returns>
    public string ToString(BaseClient client)
    {
        var query = ToString();

        if (client.ApiToken is { CanUse: true, Placement: ApiTokenPlacement.Get })
        {
            return $"{query}{(query.Length == 0 ? "?" : "&")}{Uri.EscapeDataString(client.ApiToken.Key)}={Uri.EscapeDataString(client.ApiToken.Value)}";
        }

        return query;
    }
}