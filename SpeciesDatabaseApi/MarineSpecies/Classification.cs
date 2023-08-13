using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.MarineSpecies
{
    public class Classification : IEquatable<Classification>
    {
        /// <summary>
        /// Unique and persistent identifier within WoRMS. Primary key in the database
        /// </summary>
        [JsonPropertyName("AphiaID")]
        [XmlElement("AphiaID")]
        public int AphiaId { get; set; }

        /// <summary>
        /// The taxonomic rank of the most specific name in the <see cref="ScientificName"/>
        /// </summary>
        [JsonPropertyName("rank")]
        [XmlElement("rank")]
        public string Rank { get; set; } = string.Empty;

        /// <summary>
        /// The taxonomic rank of the most specific name in the <see cref="ScientificName"/>
        /// </summary>
        [JsonPropertyName("scientificname")]
        [XmlElement("scientificname")]
        public string ScientificName { get; set; } = string.Empty;

        /// <summary>
        /// The child of this classification. NULL if no child
        /// </summary>
        [JsonPropertyName("child")]
        [XmlElement("child")]
        public Classification? Child { get; set; }

        /// <summary>
        /// True if this classification have a child, otherwise false.
        /// </summary>
        public bool HaveChild => Child is not null;


        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(AphiaId)}: {AphiaId}, {nameof(Rank)}: {Rank}, {nameof(ScientificName)}: {ScientificName}, {nameof(HaveChild)}: {HaveChild}";
        }

        public bool Equals(Classification? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return AphiaId == other.AphiaId && Rank == other.Rank && ScientificName == other.ScientificName && Equals(Child, other.Child);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Classification)obj);
        }

        public static bool operator ==(Classification? left, Classification? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Classification? left, Classification? right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(AphiaId, Rank, ScientificName, Child);
        }
    }
}
