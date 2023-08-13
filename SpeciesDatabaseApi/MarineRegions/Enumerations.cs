namespace SpeciesDatabaseApi.MarineRegions;

public enum RelationDirection
{
    Upper, 
    Lower,
    Both
}

public enum RelationType
{
    PartOf,
    PartlyPartOf, 
    AdjacentTo, 
    SimilarTo, 
    AdministrativePartOf, 
    InfluencedBy,
    All
}
