using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.Iucn
{
    public class Citation : IEquatable<Citation>
    {
        [JsonPropertyName("citation")]
        [XmlElement("citation")]
        public string CitationText { get; set; } = string.Empty;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(CitationText)}: {CitationText}";
        }

        public bool Equals(Citation? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CitationText == other.CitationText;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Citation)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return CitationText.GetHashCode();
        }

        public static bool operator ==(Citation? left, Citation? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Citation? left, Citation? right)
        {
            return !Equals(left, right);
        }
    }
}
