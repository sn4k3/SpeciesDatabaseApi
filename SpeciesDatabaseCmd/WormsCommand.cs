using System.CommandLine;
using SpeciesDatabaseApi.MarineSpecies;

namespace SpeciesDatabaseCmd;

internal static class WormsCommand
{
    private static readonly WormsClient Client = new();


    private static readonly Argument<int> AttributeIdArgument = new("attributeID", "The attribute definition id to search for.");
    private static readonly Argument<int> CategoryIdArgument = new("categoryId", "The categoryId to search for.");


    private static readonly Argument<int> AphiaIdArgument = new("aphiaID", "The AphiaID to search for.");
    private static readonly Argument<int[]> AphiaIdsArgument = new("aphiaIDs", "The AphiaIDs to search for.");
    private static readonly Argument<string> ExternalIdArgument = new("id", "The external identifier to search for.");
    private static readonly Argument<ExternalIdentifierTypeEnum> ExternalIdentifierTypeArgument = new("type", "Type of external identifier to return.");
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
        var command = new Command(Client.ClientAcronym.ToUpper(), Program.GetRootCommandDescription(Client))
        {
            //Attributes
            AttributeKeysByIdCommand(),
            AttributesCommand(),
            AttributeValuesCommand(),
            AphiaIdsAndAttributesCommand(),

            // Distributions
            DistributionsCommand(),

            // External Identifiers
            ExternalIdCommand(),
            RecordByExternalIdCommand(),

            // Sources
            SourcesCommand(),

            // Taxonomic data
            ChildrenCommand(),
            ClassificationCommand(),
            AphiaIdCommand(),
            AphiaNameCommand(),
            RecordByAphiaIdCommand(),
            FullRecordCommand(),
            RecordsByAphiaIdsCommand(),
            RecordsByDateCommand(),
            RecordsByMatchNamesCommand(),
            RecordsByNameCommand(),
            RecordsByNamesCommand(),
            RecordsByTaxonRankIdCommand(),
            SynonymsCommand(),
            TaxonRanksByIdCommand(),
            TaxonRanksByNameCommand(),

            // Vernaculars
            RecordsByVernacularCommand(),
            VernacularsCommand(),
        };
        return command;
    }

    private static Command AttributeKeysByIdCommand()
    {
        var command = new Command("AttributeKeysByID", "Get attribute definitions. To refer to root items specify ID = '0'.")
        {
            AttributeIdArgument,
            IncludeChildrenOption
        };

        command.SetHandler(async (attributeId, includeChildren) =>
        {
            var result = await Client.GetAttributeKeys(attributeId, includeChildren);
            Program.Print(result);
        }, AttributeIdArgument, IncludeChildrenOption);

        return command;
    }

    private static Command AttributesCommand()
    {
        var command = new Command("Attributes", "Get a list of attributes for a given AphiaID.")
        {
            AphiaIdArgument,
            IncludeInheritedOption
        };

        command.SetHandler(async (aphiaId, includeInherited) =>
        {
            var result = await Client.GetAttributes(aphiaId, includeInherited);
            Program.Print(result);
        }, AphiaIdArgument, IncludeInheritedOption);

        return command;
    }

    private static Command AttributeValuesCommand()
    {
        var command = new Command("AttributeValues", "Get list values that are grouped by an CategoryID.")
        {
            CategoryIdArgument,
        };

        command.SetHandler(async (categoryId) =>
        {
            var result = await Client.GetAttributeValues(categoryId);
            Program.Print(result);
        }, CategoryIdArgument);

        return command;
    }

    private static Command AphiaIdsAndAttributesCommand()
    {
        var command = new Command("AphiaIdsAndAttributes", "Get a list of AphiaIDs (max 50) with attribute tree for a given attribute definition ID.")
        {
            AttributeIdArgument,
            OffsetOption
        };

        command.SetHandler(async (attributeId, offset) =>
        {
            var result = await Client.GetAphiaIdsAndAttributes(attributeId, offset);
            Program.Print(result);
        }, AttributeIdArgument, OffsetOption);

        return command;
    }

    private static Command DistributionsCommand()
    {
        var command = new Command("Distributions", "Get all distributions for a given AphiaID.")
        {
            AphiaIdArgument,
            IncludeInheritedOption
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetDistributions(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command ExternalIdCommand()
    {
        var command = new Command("ExternalId", "Get any external identifier(s) for a given AphiaID")
        {
            AphiaIdArgument,
            ExternalIdentifierTypeArgument
        };

        command.SetHandler(async (aphiaId, type) =>
        {
            var result = await Client.GetExternalId(aphiaId, type);
            Program.Print(result);
        }, AphiaIdArgument, ExternalIdentifierTypeArgument);

        return command;
    }

    private static Command RecordByExternalIdCommand()
    {
        var command = new Command("RecordByExternalID", "Get the Aphia Record for a given external identifier")
        {
            ExternalIdArgument,
            ExternalIdentifierTypeArgument
        };

        command.SetHandler(async (externalId, type) =>
        {
            var result = await Client.GetRecordByExternalId(externalId, type);
            Program.Print(result);
        }, ExternalIdArgument, ExternalIdentifierTypeArgument);

        return command;
    }

    private static Command SourcesCommand()
    {
        var command = new Command("Sources", "Get one or more sources/references including links, for one AphiaID")
        {
            AphiaIdArgument
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetSources(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command ChildrenCommand()
    {
        var command = new Command("Children", "Get the direct children (max. 50) for a given AphiaID.")
        {
            AphiaIdArgument,
            MarineOnlyOption, 
            OffsetOption
        };

        command.SetHandler(async (aphiaId, marineOnly, offset) =>
        {
            var result = await Client.GetChildren(aphiaId, marineOnly, offset);
            Program.Print(result);
        }, AphiaIdArgument, MarineOnlyOption, OffsetOption);

        return command;
    }

    private static Command ClassificationCommand()
    {
        var command = new Command("Classification", "Get the complete classification for one taxon. This also includes any sub or super ranks.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetClassification(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);
        
        return command;
    }

    private static Command AphiaIdCommand()
    {
        var command = new Command("AphiaId", "Get the AphiaID for a given name.")
        {
            ScientificNameArgument,
        };

        command.SetHandler(async (scientificName) =>
        {
            var result = await Client.GetAphiaId(scientificName);
            Program.Print(result);
        }, ScientificNameArgument);

        return command;
    }

    private static Command AphiaNameCommand()
    {
        var command = new Command("AphiaName", "Get the name for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetAphiaName(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command RecordByAphiaIdCommand()
    {
        var command = new Command("RecordByAphiaID", "Get the complete AphiaRecord for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetRecord(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command FullRecordCommand()
    {
        var command = new Command("FullRecord", "Returns the linked open data implementation of an AphiaRecord for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetFullRecord(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }

    private static Command RecordsByAphiaIdsCommand()
    {
        var command = new Command("RecordsByAphiaIDs", "Get an AphiaRecord for multiple AphiaIDs in one go (max: 50).")
        {
            AphiaIdsArgument,
        };

        command.SetHandler(async (aphiaIds) =>
        {
            var result = await Client.GetRecords(aphiaIds);
            Program.Print(result);
        }, AphiaIdsArgument);

        return command;
    }

    private static Command RecordsByDateCommand()
    {
        var startDateArgument = new Argument<DateTime>("startDate", "ISO 8601 formatted start date(time). i.e. 2023-08-08T15:59:31+00:00");
        var endDateArgument = new Argument<DateTime>("endDate", "ISO 8601 formatted end date(time). i.e. 2023-08-08T15:59:31+00:00");

        var command = new Command("RecordsByDate", "Lists all AphiaRecords (taxa) that have their last edit action (modified or added) during the specified period.")
        {
            startDateArgument,
            endDateArgument,
            MarineOnlyOption,
            OffsetOption
        };

        command.SetHandler(async (startDate, endDate, marineOnly, offset) =>
        {
            var result = await Client.GetRecords(startDate, endDate, marineOnly, offset);
            Program.Print(result);
        }, startDateArgument, endDateArgument, MarineOnlyOption, OffsetOption);

        return command;
    }

    private static Command RecordsByMatchNamesCommand()
    {
        var command = new Command("RecordsByMatchNames", "For each given scientific name (may include authority), try to find one or more AphiaRecords, using the TAXAMATCH fuzzy matching algorithm by Tony Rees.")
        {
            ScientificNamesArgument,
            MarineOnlyOption
        };

        command.SetHandler(async (scientificNames, marineOnly) =>
        {
            var results = await Client.GetRecordsFuzzy(scientificNames, marineOnly);
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

    private static Command RecordsByNameCommand()
    {
        var command = new Command("RecordsByName", "Get one or more matching (max. 50) AphiaRecords for a given name.")
        {
            ScientificNameArgument,
            LikeOption,
            MarineOnlyOption,
            OffsetOption
        };

        command.SetHandler(async (scientificName, like, marineOnly, offset) =>
        {
            var result = await Client.GetRecords(scientificName, like, marineOnly, offset);
            Program.Print(result);
        }, ScientificNameArgument, LikeOption, MarineOnlyOption, OffsetOption);

        return command;
    }

    private static Command RecordsByNamesCommand()
    {
        var command = new Command("RecordsByNames", "For each given scientific name, try to find one or more AphiaRecords. This allows you to match multiple names in one call. Limited to 500 names at once for performance reasons.")
        {
            ScientificNamesArgument,
            LikeOption,
            MarineOnlyOption,
        };

        command.SetHandler(async (scientificNames, like, marineOnly) =>
        {
            var results = await Client.GetRecords(scientificNames, like, marineOnly);
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

    private static Command RecordsByTaxonRankIdCommand()
    {
        var belongsToArgument = new Argument<int?>("belongsTo", () => null, "Limit the results to descendants of the given AphiaID.");
        var command = new Command("RecordsByTaxonRankID", "Get the AphiaRecords for a given taxonRankID (max 50).")
        {
            TaxonomicIdArgument,
            belongsToArgument,
            OffsetOption
        };

        command.SetHandler(async (taxonomicId, belongsTo, offset) =>
        {
            var result = await Client.GetRecords(taxonomicId, belongsTo, offset);
            Program.Print(result);
        }, TaxonomicIdArgument, belongsToArgument, OffsetOption);

        return command;
    }

    private static Command SynonymsCommand()
    {
        var command = new Command("Synonyms", "Get all synonyms for a given AphiaID.")
        {
            AphiaIdArgument,
            OffsetOption
        };

        command.SetHandler(async (aphiaId, offset) =>
        {
            var result = await Client.GetSynonyms(aphiaId, offset);
            Program.Print(result);
        }, AphiaIdArgument, OffsetOption);

        return command;
    }

    private static Command TaxonRanksByIdCommand()
    {
        var command = new Command("TaxonRanksByID", "Get taxonomic ranks by their identifier.")
        {
            TaxonomicIdArgument,
            AphiaIdArgument,
        };

        command.SetHandler(async (taxonomicId, aphiaId) =>
        {
            var result = await Client.GetTaxonRanks(taxonomicId, aphiaId);
            Program.Print(result);
        }, TaxonomicIdArgument, AphiaIdArgument);

        return command;
    }

    private static Command TaxonRanksByNameCommand()
    {
        var command = new Command("TaxonRanksByName", "Get taxonomic ranks by their name.")
        {
            TaxonomicNameArgument,
            AphiaIdArgument,
        };

        command.SetHandler(async (taxonomicName, aphiaId) =>
        {
            var result = await Client.GetTaxonRanks(taxonomicName, aphiaId);
            Program.Print(result);
        }, TaxonomicNameArgument, AphiaIdArgument);

        return command;
    }

    private static Command RecordsByVernacularCommand()
    {
        var command = new Command("RecordsByVernacular", "Get taxonomic ranks by their name.")
        {
            VernacularNameArgument,
            LikeOption,
            OffsetOption
        };

        command.SetHandler(async (vernacularName, like, offset) =>
        {
            var result = await Client.GetRecordsByVernacular(vernacularName, like, offset);
            Program.Print(result);
        }, VernacularNameArgument, LikeOption, OffsetOption);

        return command;
    }

    private static Command VernacularsCommand()
    {
        var command = new Command("Vernaculars", "Get all vernaculars for a given AphiaID.")
        {
            AphiaIdArgument,
        };

        command.SetHandler(async (aphiaId) =>
        {
            var result = await Client.GetVernaculars(aphiaId);
            Program.Print(result);
        }, AphiaIdArgument);

        return command;
    }
}