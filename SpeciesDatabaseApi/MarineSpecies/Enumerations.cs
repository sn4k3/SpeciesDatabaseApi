namespace SpeciesDatabaseApi.MarineSpecies;

public enum AphiaMatchTypeEnum
{
    Exact,
    Like,
    Phonetic,
    Near_1,
    Near_2,
}

public enum RecordStatusEnum
{
    Inaccurate,
    Doubtful,
    Valid
}

public enum TypeStatusEnum
{
    Empty,
    HoloType,
}

public enum QualityStatusEnum
{
    Unreviewed,
    Checked,
    Trusted,
}

public enum ExternalIdentifierTypeEnum
{
    Algaebase,
    Bold,
    Dyntaxa,
    Fishbase,
    Iucn,
    Lsid,
    Ncbi,
    Tsn,
    Gisd
}