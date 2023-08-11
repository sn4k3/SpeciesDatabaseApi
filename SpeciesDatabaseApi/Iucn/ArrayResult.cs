using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn;


public class ArrayResult<T> : BaseResponse, IEquatable<ArrayResult<T>>
{
	private int _count;

	[JsonPropertyName("count")]
	[XmlElement("count")]
	public int Count
	{
		get => _count == 0 ? Results.Length : _count;
		set => _count = value;
	}

	[JsonPropertyName("result")]
	[XmlElement("result")]
	public T[] Results { get; set; } = Array.Empty<T>();

	/// <inheritdoc />
	public override string ToString()
	{
		var sb = new StringBuilder($"{base.ToString()}, {nameof(Count)}: {Count}, {nameof(Results)}: {Results.Length}");
		sb.AppendLine();
		for (var i = 0; i < Results.Length; i++)
		{
			sb.AppendLine($"## Result[{i}]:");
			sb.AppendLine(Results[i]?.ToString());
		}

		return sb.ToString();
	}

	public IEnumerator<T> GetEnumerator()
	{
		return ((IEnumerable<T>)Results).GetEnumerator();
	}

	public bool Equals(ArrayResult<T>? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return base.Equals(other) && Results.Equals(other.Results);
	}
	
	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((ArrayResult<T>)obj);
	}

	/// <inheritdoc />
	public override int GetHashCode()
	{
		return HashCode.Combine(base.GetHashCode(), Results);
	}

	public static bool operator ==(ArrayResult<T>? left, ArrayResult<T>? right)
	{
		return Equals(left, right);
	}

	public static bool operator !=(ArrayResult<T>? left, ArrayResult<T>? right)
	{
		return !Equals(left, right);
	}

	/// <inheritdoc />
	public T this[int index] => Results[index];
}