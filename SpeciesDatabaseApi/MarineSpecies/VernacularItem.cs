using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.MarineSpecies
{
    public class VernacularItem : IEquatable<VernacularItem>
    {
        /// <summary>
        /// The vernacular name
        /// </summary>
        [JsonPropertyName("vernacular")]
        [XmlElement("vernacular")]
        public string Vernacular { get; set; } = string.Empty;

        /// <summary>
        /// The language code in ISO 639-3
        /// </summary>
        [JsonPropertyName("language_code")]
        [XmlElement("language_code")]
        public string LanguageCode { get; set; } = string.Empty;

        /// <summary>
        /// The language name in english
        /// </summary>
        [JsonPropertyName("language")]
        [XmlElement("language")]
        public string Language { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{nameof(Vernacular)}: {Vernacular}, {nameof(LanguageCode)}: {LanguageCode}, {nameof(Language)}: {Language}";
        }

        public bool Equals(VernacularItem? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Vernacular == other.Vernacular && LanguageCode == other.LanguageCode && Language == other.Language;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((VernacularItem)obj);
        }

        public static bool operator ==(VernacularItem? left, VernacularItem? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(VernacularItem? left, VernacularItem? right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vernacular, LanguageCode, Language);
        }
    }
}
