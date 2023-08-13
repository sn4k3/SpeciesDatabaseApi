namespace SpeciesDatabaseApi.SpeciesPlus;

public enum ScopeEnum
{
    SelectAll,
    Current,
    Historic
}

public enum DistributionsTypeEnum
{
    COUNTRY,
    TERRITORY
}

public enum EuDecisionTypeEnum
{
    SUSPENSION,
    POSITIVE_OPINION,
    NEGATIVE_OPINION,
    NO_OPINION,

    COUNTRY,
    TERRITORY
}

public enum TaxaRankEnum
{
    KINGDOM, 
    PHYLUM, 
    CLASS, 
    ORDER, 
    FAMILY, 
    SUBFAMILY, 
    GENUS, 
    SPECIES, 
    SUBSPECIES, 
    VARIETY
}

public enum TaxomonyEnum
{
	CITES,
	CMS
}