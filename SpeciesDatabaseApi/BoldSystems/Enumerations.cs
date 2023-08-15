using System;

namespace SpeciesDatabaseApi.BoldSystems;

public enum BoldDatabaseEnum 
{
    /// <summary>
    /// Every COI barcode record on BOLD with a minimum sequence length of 500bp (warning: unvalidated library and includes records without species level identification).<br/>
    /// This includes many species represented by only one or two specimens as well as all species with interim taxonomy. <br/>
    /// This search only returns a list of the nearest matches and does not provide a probability of placement to a taxon.
    /// </summary>
    COX1,

    /// <summary>
    /// Every COI barcode record with a species level identification and a minimum sequence length of 500bp.<br/>
    /// This includes many species represented by only one or two specimens as well as all species with interim taxonomy.
    /// </summary>
    COX1_SPECIES,

    /// <summary>
    /// All published COI records from BOLD and GenBank with a minimum sequence length of 500bp.<br/>
    /// This library is a collection of records from the published projects section of BOLD.
    /// </summary>
    COX1_SPECIES_PUBLIC,

    /// <summary>
    /// Subset of the Species library with a minimum sequence length of 640bp and containing both public and private records.<br/>
    /// This library is intended for short sequence identification as it provides maximum overlap with short reads from the barcode region of COI.
    /// </summary>
    COX1_L640bp
}

[Flags]
public enum BoldDataTypesEnum
{
	/// <summary>
	/// basic taxonomy info: includes taxid, taxon name, tax rank, tax division, parent taxid, parent taxon name
	/// </summary>
	Basic = 1,

	/// <summary>
	/// specimen and sequence statistics: includes public species count, public BIN count, public marker counts, public record count, specimen count, sequenced specimen count, barcode specimen count, species count, barcode species count
	/// </summary>
	Stats = 2,

	/// <summary>
	/// collection site information: includes country, collection site map
	/// </summary>
	Geo = 4,

	/// <summary>
	/// specimen images: includes copyright information, image URL, image metadata
	/// </summary>
	Images = 8,

	/// <summary>
	/// sequencing labs: includes lab name, record count
	/// </summary>
	SequencingLabs = 16,

	/// <summary>
	/// specimen depositories: includes depository name, record count
	/// </summary>
	Depository = 32,

	/// <summary>
	/// information from third parties: includes Wikipedia summary, Wikipedia URL
	/// </summary>
	ThirdParty = 64,

	/// <summary>
	/// all information: identical to specifying all data types at once
	/// </summary>
	All = 128,
}

public enum BoldStatsDataType
{
	/// <summary>
	/// Provides record counts by [BINs, Country, Storing Institution, Species]
	/// </summary>
	DrillDown,

	/// <summary>
	/// Provides the total counts of [BINs, Countries, Storing Institutions, Orders, Families, Genus, Species] found by the query
	/// </summary>
	Overview
}