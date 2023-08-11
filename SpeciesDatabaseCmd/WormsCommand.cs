using System.CommandLine;
using SpeciesDatabaseApi;
using SpeciesDatabaseApi.Worms;

namespace SpeciesDatabaseCmd;

internal static class WormsCommand
{
    private static readonly WormsClient Client = new();


    private static readonly Argument<int> AttributeIdArgument = new("attributeID", "The attribute definition id to search for.");
    private static readonly Argument<int> categoryIdArgument = new("categoryId", "The categoryId to search for.");


    private static readonly Argument<int> AphiaIdArgument = new("aphiaID", "The AphiaID to search for.");
    private static readonly Argument<int[]> AphiaIdsArgument = new("aphiaIDs", "The AphiaIDs to search for.");
    private static readonly Argument<string> ExternalIdArgument = new("id", "The external identifier to search for.");
    private static readonly Argument<ExternalIdentifierType> ExternalIdentifierTypeArgument = new("type", "Type of external identifier to return.");
    private static readonly Argument<string> ScientificNameArgument = new("scientific-name", "Name to search for.");
    private static readonly Argument<string[]> ScientificNamesArgument = new("scientific-names", "Names to search for.");

    private static readonly Argument<int> TaxonomicIdArgument = new("taxonomicId", "A taxonomic rank identifier.");
    private static readonly Argument<string> TaxonomicNameArgument = new("taxonomic-name", "A taxonomic rank name.");

    private static readonly Argument<string> VernacularNameArgument = new("vernacular-name", "The vernacular to find records for.");


    private static readonly Option<bool> IncludeChildrenOption = new(new[]{"-c", "--include-children"}, () => true, "Include the tree of children.");
    private static readonly Option<bool> IncludeInheritedOption = new(new[]{"-i", "--include-inherited" }, "Include attributes inherited from the taxon its parent(s).");
    private static readonly Option<bool> LikeOption = new(new[]{"-l", "--like"}, () => true, "Add a '%'-sign added after the ScientificName (SQL LIKE function).");
    private static readonly Option<bool> MarineOnlyOption = new(new[]{"-m", "--marine-only"}, () => true, "Limit to marine taxa.");
    private static readonly Option<int> OffsetOption = new(new[]{"-o", "--offset"}, () => 1, "Starting record number, when retrieving next chunk of (50) records.");

    internal static Command CreateCommand()
    {
        var command = new Command(Client.ClientAcronym.ToUpper(), $"Query - {Client.ClientFullName}")
        {
            //Attributes
            AphiaAttributeKeysByIdCommand(),
            AphiaAttributesByAphiaIdCommand(),
            AphiaAttributeValuesByCategoryIdCommand(),
            AphiaIDsByAttributeKeyIdCommand(),

            // Distributions
            AphiaDistributionsByAphiaIdCommand(),

            // External Identifiers
            AphiaExternalIdByAphiaIdCommand(),
            AphiaRecordByExternalIdCommand(),

            // Sources
            AphiaSourcesByAphiaIdCommand(),

            // Taxonomic data
            AphiaChildrenByAphiaIdCommand(),
            AphiaClassificationByAphiaIdCommand(),
            AphiaIdByNameCommand(),
            AphiaNameByAphiaIdCommand(),
            AphiaRecordByAphiaIdCommand(),
            AphiaRecordFullByAphiaIdCommand(),
            AphiaRecordsByAphiaIdsCommand(),
            AphiaRecordsByDateCommand(),
            AphiaRecordsByMatchNamesCommand(),
            AphiaRecordsByNameCommand(),
            AphiaRecordsByNamesCommand(),
            AphiaRecordsByTaxonRankIdCommand(),
            AphiaSynonymsByAphiaIdCommand(),
            AphiaTaxonRanksByIdCommand(),
            AphiaTaxonRanksByNameCommand(),

            // Vernaculars
            AphiaRecordsByVernacularCommand(),
            AphiaVernacularsByAphiaIdCommand(),
        };
        return command;
    }

    private static Command AphiaAttributeKeysByIdCommand()
    {
        var command = new Command("AphiaAttributeKeysByID", "Get attribute definitions. To refer to root items specify ID = '0'.")
        {
            AttributeIdArgument,
            IncludeChildrenOption
        };

        command.SetHandler(async (attributeId, includeChildren) =>
        {
            var result = await Client.GetAphiaAttributeKeysById(attributeId, includeChildren);
            Program.Print(result);
        }, AttributeIdArgument, IncludeChildrenOption);

        return command;
    }

    private static Command AphiaAttributesByAphiaIdCommand()
    {
        var command = new Command("AphiaAttributesByAphiaID", "Get a list of attributes for a given AphiaID.")
        {
            AphiaIdArgument,
            IncludeInheritedOption
        };

        command.SetHandler(async (aphiaId, includeInherited) =>
        {
            var result = await Client.GetAphiaAttributesByAphiaId(aphiaId, includeInherited);
            Program.Print(result);
        }, AphiaIdArgument, IncludeInheritedOption);

        return command;
    }

    private static Command AphiaAttributeValuesByCategoryIdCommand()
    {
        var command = new Command("AphiaAttributeValuesByCategoryID", "Get list values that are grouped by an CategoryID.")
        {
            categoryIdArgument,
        };

        command.SetHandler(async (categoryId) =>
        {
            var result = await Client.GetAphiaAttributeValuesByCategoryId(categoryId);
            Program.Print(result);
        }, categoryIdArgument);

        return command;
    }

    private static Command AphiaIDsByAttributeKeyIdCommand()
    {
        var command = new Command("AphiaIDsByAttributeKeyID", "Get a list of AphiaIDs (max 50) with attribute tree for a given attribute definition ID.")
        {
            AttributeIdArgument,
            OffsetOption
        };

        command.SetHandler(async (attributeId, offset) =>
        {
            var result = await Client.GetAphiaIDsByAttributeKeyId(attributeId, offset);
            Program.Print(result);
        }, AttributeIdArgument, OffsetOption);

        return command;
    }

    private static Command AphiaDistributionsByAphiaIdCommand()
    {
        var command = new Command("AphiaDistributionsByAphiaID", "Get all distributions for a given AphiaID.")
        {
            AphiaIdArgument,
            IncludeInheritedOption
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaDistributionsByAphiaId(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command AphiaExternalIdByAphiaIdCommand()
    {
        var command = new Command("AphiaExternalIDByAphiaID", "Get any external identifier(s) for a given AphiaID")
        {
            AphiaIdArgument,
            ExternalIdentifierTypeArgument
        };

        command.SetHandler(async (aphiaId, type) =>
        {
            var result = await Client.GetAphiaExternalIdByAphiaId(aphiaId, type);
            Program.Print(result);
        }, AphiaIdArgument, ExternalIdentifierTypeArgument);

        return command;
    }

    private static Command AphiaRecordByExternalIdCommand()
    {
        var command = new Command("AphiaRecordByExternalID", "Get the Aphia Record for a given external identifier")
        {
            ExternalIdArgument,
            ExternalIdentifierTypeArgument
        };

        command.SetHandler(async (externalId, type) =>
        {
            var result = await Client.GetAphiaRecordByExternalId(externalId, type);
            Program.Print(result);
        }, ExternalIdArgument, ExternalIdentifierTypeArgument);

        return command;
    }

    private static Command AphiaSourcesByAphiaIdCommand()
    {
        var command = new Command("AphiaSourcesByAphiaID", "Get one or more sources/references including links, for one AphiaID")
        {
            AphiaIdArgument
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaSourcesByAphiaId(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command AphiaChildrenByAphiaIdCommand()
    {
        var command = new Command("AphiaChildrenByAphiaID", "Get the direct children (max. 50) for a given AphiaID.")
        {
            AphiaIdArgument,
            MarineOnlyOption, 
            OffsetOption
        };

        command.SetHandler(async (aphiaId, marineOnly, offset) =>
        {
            var result = await Client.GetAphiaChildrenByAphiaId(aphiaId, marineOnly, offset);
            Program.Print(result);
        }, AphiaIdArgument, MarineOnlyOption, OffsetOption);

        return command;
    }

    private static Command AphiaClassificationByAphiaIdCommand()
    {
        var command = new Command("AphiaClassificationByAphiaID", "Get the complete classification for one taxon. This also includes any sub or super ranks.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaClassificationByAphiaId(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);
        
        return command;
    }

    private static Command AphiaIdByNameCommand()
    {
        var command = new Command("AphiaIDByName", "Get the AphiaID for a given name.")
        {
            ScientificNameArgument,
        };

        command.SetHandler(async (scientificName) =>
        {
            var result = await Client.GetAphiaIdByName(scientificName);
            Program.Print(result);
        }, ScientificNameArgument);

        return command;
    }

    private static Command AphiaNameByAphiaIdCommand()
    {
        var command = new Command("AphiaNameByAphiaID", "Get the name for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaNameByAphiaId(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command AphiaRecordByAphiaIdCommand()
    {
        var command = new Command("AphiaRecordByAphiaID", "Get the complete AphiaRecord for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaRecordByAphiaId(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command AphiaRecordFullByAphiaIdCommand()
    {
        var command = new Command("AphiaRecordFullByAphiaID", "Returns the linked open data implementation of an AphiaRecord for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaRecordFullByAphiaId(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command AphiaRecordsByAphiaIdsCommand()
    {
        var command = new Command("AphiaRecordsByAphiaIDs", "Get an AphiaRecord for multiple AphiaIDs in one go (max: 50).")
        {
            AphiaIdsArgument,
        };

        command.SetHandler(async (aphiaIds) =>
        {
            var result = await Client.GetAphiaRecordsByAphiaIds(aphiaIds);
            Program.Print(result);
        }, AphiaIdsArgument);

        return command;
    }

    private static Command AphiaRecordsByDateCommand()
    {
        var startDateArgument = new Argument<DateTime>("startDate", "ISO 8601 formatted start date(time). i.e. 2023-08-08T15:59:31+00:00");
        var endDateArgument = new Argument<DateTime>("endDate", "ISO 8601 formatted end date(time). i.e. 2023-08-08T15:59:31+00:00");

        var command = new Command("AphiaRecordsByDate", "Lists all AphiaRecords (taxa) that have their last edit action (modified or added) during the specified period.")
        {
            startDateArgument,
            endDateArgument,
            MarineOnlyOption,
            OffsetOption
        };

        command.SetHandler(async (startDate, endDate, marineOnly, offset) =>
        {
            var result = await Client.GetAphiaRecordsByDate(startDate, endDate, marineOnly, offset);
            Program.Print(result);
        }, startDateArgument, endDateArgument, MarineOnlyOption, OffsetOption);

        return command;
    }

    private static Command AphiaRecordsByMatchNamesCommand()
    {
        var command = new Command("AphiaRecordsByMatchNames", "For each given scientific name (may include authority), try to find one or more AphiaRecords, using the TAXAMATCH fuzzy matching algorithm by Tony Rees.")
        {
            ScientificNamesArgument,
            MarineOnlyOption
        };

        command.SetHandler(async (scientificNames, marineOnly) =>
        {
            var results = await Client.GetAphiaRecordsByMatchNames(scientificNames, marineOnly);
            if (results is null)
            {
                Program.WriteLineWarning("No data returned.");
                return;
            }

            foreach (var result in results)
            {
                Program.Print(result);
            }
        }, ScientificNamesArgument, MarineOnlyOption);

        return command;
    }

    private static Command AphiaRecordsByNameCommand()
    {
        var command = new Command("AphiaRecordsByName", "Get one or more matching (max. 50) AphiaRecords for a given name.")
        {
            ScientificNameArgument,
            LikeOption,
            MarineOnlyOption,
            OffsetOption
        };

        command.SetHandler(async (scientificName, like, marineOnly, offset) =>
        {
            var result = await Client.GetAphiaRecordsByName(scientificName, like, marineOnly, offset);
            Program.Print(result);
        }, ScientificNameArgument, LikeOption, MarineOnlyOption, OffsetOption);

        return command;
    }

    private static Command AphiaRecordsByNamesCommand()
    {
        var command = new Command("AphiaRecordsByNames", "For each given scientific name, try to find one or more AphiaRecords. This allows you to match multiple names in one call. Limited to 500 names at once for performance reasons.")
        {
            ScientificNamesArgument,
            LikeOption,
            MarineOnlyOption,
        };

        command.SetHandler(async (scientificNames, like, marineOnly) =>
        {
            var results = await Client.GetAphiaRecordsByNames(scientificNames, like, marineOnly);
            if (results is null)
            {
                Program.WriteLineWarning("No data returned.");
                return;
            }

            foreach (var result in results)
            {
                Program.Print(result);
            }
        }, ScientificNamesArgument, LikeOption, MarineOnlyOption);

        return command;
    }

    private static Command AphiaRecordsByTaxonRankIdCommand()
    {
        var belongsToArgument = new Argument<int>("belongsTo", "Limit the results to descendants of the given AphiaID.");
        var command = new Command("AphiaRecordsByTaxonRankID", "Get the AphiaRecords for a given taxonRankID (max 50).")
        {
            TaxonomicIdArgument,
            belongsToArgument,
            OffsetOption
        };

        command.SetHandler(async (taxonomicId, belongsTo, offset) =>
        {
            var result = await Client.GetAphiaRecordsByTaxonRankId(taxonomicId, belongsTo, offset);
            Program.Print(result);
        }, TaxonomicIdArgument, belongsToArgument, OffsetOption);

        return command;
    }

    private static Command AphiaSynonymsByAphiaIdCommand()
    {
        var command = new Command("AphiaSynonymsByAphiaID", "Get all synonyms for a given AphiaID.")
        {
            AphiaIdArgument,
            OffsetOption
        };

        command.SetHandler(async (aphiaId, offset) =>
        {
            var result = await Client.GetAphiaSynonymsByAphiaId(aphiaId, offset);
            Program.Print(result);
        }, AphiaIdArgument, OffsetOption);

        return command;
    }

    private static Command AphiaTaxonRanksByIdCommand()
    {
        var command = new Command("AphiaTaxonRanksByID", "Get taxonomic ranks by their identifier.")
        {
            TaxonomicIdArgument,
            AphiaIdArgument,
        };

        command.SetHandler(async (taxonomicId, aphiaId) =>
        {
            var result = await Client.GetAphiaTaxonRanksById(taxonomicId, aphiaId);
            Program.Print(result);
        }, TaxonomicIdArgument, AphiaIdArgument);

        return command;
    }

    private static Command AphiaTaxonRanksByNameCommand()
    {
        var command = new Command("AphiaTaxonRanksByName", "Get taxonomic ranks by their name.")
        {
            TaxonomicNameArgument,
            AphiaIdArgument,
        };

        command.SetHandler(async (taxonomicName, aphiaId) =>
        {
            var result = await Client.GetAphiaTaxonRanksByName(taxonomicName, aphiaId);
            Program.Print(result);
        }, TaxonomicNameArgument, AphiaIdArgument);

        return command;
    }

    private static Command AphiaRecordsByVernacularCommand()
    {
        var command = new Command("AphiaRecordsByVernacular", "Get taxonomic ranks by their name.")
        {
            VernacularNameArgument,
            LikeOption,
            OffsetOption
        };

        command.SetHandler(async (vernacularName, like, offset) =>
        {
            var result = await Client.GetAphiaRecordsByVernacular(vernacularName, like, offset);
            Program.Print(result);
        }, VernacularNameArgument, LikeOption, OffsetOption);

        return command;
    }

    private static Command AphiaVernacularsByAphiaIdCommand()
    {
        var command = new Command("AphiaVernacularsByAphiaID", "Get all vernaculars for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaVernacularsByAphiaId(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }
}