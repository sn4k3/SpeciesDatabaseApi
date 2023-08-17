using System.CommandLine;
using SpeciesDatabaseApi.MarineRegions;

namespace SpeciesDatabaseCmd;

internal static class MarineRegionsCommand
{
    private static readonly MarineRegionsClient Client = new();

    private static readonly Argument<int> MrgIdArgument = new("mrgid", "The MRGID to search for.");
    private static readonly Argument<string> NameArgument = new("name", "The name to search for.");
    private static readonly Argument<string[]> NamesArgument = new("names", "The names to search for.");
    private static readonly Argument<int[]> TypeIdsArgument = new("type-ids", "One or more placetypeIDs.");
    private static readonly Argument<int> OffsetArgument = new( "offset", () => 0, "Provide offset to return next batch of 100 sources.");
    private static readonly Argument<int> SourceIdArgument = new( "source-id", "The SourceID to search for.");
    private static readonly Argument<string> SourceNameArgument = new( "source-name", "The source name to search for.");
    private static readonly Argument<string> TypeNameArgument = new( "type-name", "The type name to search for.");

    private static readonly Option<string?> LanguageOption = new(new[] { "--language" }, "Language (ISO 639-1 code).");
    private static readonly Option<bool> LikeOption = new(new[] { "-l", "--like" }, () => true, "Adds a '%'-sign before and after the GazetteerName (SQL LIKE function).");
    private static readonly Option<bool> FuzzyOption = new(new[] { "-f", "--fuzzy" }, () => false, "Uses Levenshtein query to find nearest matches.");
    private static readonly Option<int> OffsetOption = new(new[]{"-o", "--offset"}, "Start record number, in order to page through next batch of results.");
    private static readonly Option<int> CountOption = new(new[]{"-c", "--count"}, () => 100, "number of records to retrieve. max=100.");

    internal static Command CreateCommand()
    {
        var command = new Command(Client.ClientAcronym.ToUpper(), Program.GetRootCommandDescription(Client))
        {
            RecordCommand(),
            FullRecordCommand(),

            GeometriesCommand(),
            TypesCommand(),
            RecordsByNameCommand(),
            RecordsByNamesCommand(),

            FeedCommand(),

            WmSesCommand(),
            RelationsCommand(),
            SourcesCommand(),
            SourceCommand(),
            NamesCommand(),
            RecordsBySourceCommand(),
            RecordsByTypeCommand(),
            RecordsByLatLongCommand()
        };

        return command;
    }

    private static Command RecordCommand()
    {
        var command = new Command("Record", "Gets one record for the given MRGID.")
        {
            MrgIdArgument
        };

        command.SetHandler(async (mrgId) =>
        {
            var result = await Client.GetRecord(mrgId);
            Program.Print(result);
        }, MrgIdArgument);

        return command;
    }

    private static Command FullRecordCommand()
    {
        var command = new Command("FullRecord", "Gets one record for the given MRGID.")
        {
            MrgIdArgument
        };

        command.SetHandler(async (mrgId) =>
        {
            var result = await Client.GetFullRecord(mrgId);
            Program.Print(result);
        }, MrgIdArgument);

        return command;
    }

    private static Command GeometriesCommand()
    {
        var command = new Command("Geometries", "Gets all geometries associated with a gazetteer record.")
        {
            MrgIdArgument
        };

        command.SetHandler(async (mrgId) =>
        {
            var result = await Client.GetGeometries(mrgId);
            Program.Print(result);
        }, MrgIdArgument);

        return command;
    }

    private static Command TypesCommand()
    {
        var command = new Command("Types", "Gets all possible types.")
        {
        };

        command.SetHandler(async () =>
        {
            var result = await Client.GetTypes();
            Program.Print(result);
        });

        return command;
    }

    private static Command RecordsByNameCommand()
    {
        var command = new Command("RecordsByName", "Gets a list of the first 100 matching records for given name.")
        {
            NameArgument,
            TypeIdsArgument,

            LikeOption,
            FuzzyOption,
            LanguageOption,
            OffsetOption,
            CountOption,
        };

        command.SetHandler(async (name, typeIds, like, fuzzy, language, offset, count) =>
        {
            var result = await Client.GetRecords(name, like, fuzzy, typeIds, language, offset, count);
            Program.Print(result);
        }, NameArgument, TypeIdsArgument, LikeOption, FuzzyOption, LanguageOption, OffsetOption, CountOption);

        return command;
    }

    private static Command RecordsByNamesCommand()
    {
        var command = new Command("RecordsByNames", "Gets a list of the first 100 matching records for given names.")
        {
            NamesArgument,

            LikeOption,
            FuzzyOption,
        };

        command.SetHandler(async (names, like, fuzzy) =>
        {
            var result = await Client.GetRecords(names, like, fuzzy);
            if (result is null)
            {
                Program.Print(result);
                return;
            }

            foreach (var record in result)
            {
                Program.Print(record);
            }
            
        }, NamesArgument, LikeOption, FuzzyOption);

        return command;
    }

    private static Command FeedCommand()
    {
        var command = new Command("Feed", "Gets the Linked Data Event Stream feed")
        {
        };

        command.SetHandler(async () =>
        {
            var result = await Client.GetFeed();
            Program.Print(result);
        });

        return command;
    }

    private static Command WmSesCommand()
    {
        var command = new Command("WMSes", "Gets WMS information for the given MRGID.")
        {
            MrgIdArgument
        };

        command.SetHandler(async (mrgId) =>
        {
            var result = await Client.GetWmSes(mrgId);
            Program.Print(result);
        }, MrgIdArgument);

        return command;
    }

    private static Command RelationsCommand()
    {
        var directionArgument = new Argument<RelationDirection>("direction", () => RelationDirection.Upper);
        var typeArgument = new Argument<RelationType>("type", () => RelationType.PartOf);
        var command = new Command("Relations", "Gets the first 100 related records for the given MRGID.")
        {
            MrgIdArgument,
            directionArgument,
            typeArgument
        };

        command.SetHandler(async (mrgId, direction, type) =>
        {
            var result = await Client.GetRelations(mrgId, direction, type);
            Program.Print(result);
        }, MrgIdArgument, directionArgument, typeArgument);

        return command;
    }

    private static Command SourcesCommand()
    {
        var command = new Command("Sources", "Gets all sources per batch of 100.")
        {
            OffsetArgument
        };

        command.SetHandler(async (offset) =>
        {
            var result = await Client.GetSources(offset);
            Program.Print(result);
        }, OffsetArgument);

        return command;
    }

    private static Command SourceCommand()
    {
        var command = new Command("Source", "Gets the source name corresponding to a source ID.")
        {
            SourceIdArgument
        };

        command.SetHandler(async (sourceId) =>
        {
            var result = await Client.GetSource(sourceId);
            Program.Print(result);
        }, SourceIdArgument);

        return command;
    }

    private static Command NamesCommand()
    {
        var command = new Command("Names", "Gets the first 100 names for the given MRGID.")
        {
            MrgIdArgument
        };

        command.SetHandler(async (mrgId) =>
        {
            var result = await Client.GetNames(mrgId);
            Program.Print(result);
        }, MrgIdArgument);

        return command;
    }

    private static Command RecordsBySourceCommand()
    {
        var command = new Command("RecordsBySource", "Gets the first 100 records for the given source.")
        {
            SourceNameArgument
        };

        command.SetHandler(async (sourceName) =>
        {
            var result = await Client.GetRecordsBySource(sourceName);
            Program.Print(result);
        }, SourceNameArgument);

        return command;
    }

    private static Command RecordsByTypeCommand()
    {
        var command = new Command("RecordsByType", "Gets the first 100 records for the given source.")
        {
            TypeNameArgument,
            OffsetArgument
        };

        command.SetHandler(async (typeName, offset) =>
        {
            var result = await Client.GetRecordsByType(typeName, offset);
            Program.Print(result);
        }, TypeNameArgument, OffsetArgument);

        return command;
    }

    private static Command RecordsByLatLongCommand()
    {
        var latitudeArgument = new Argument<decimal>("latitude", "A decimal number which ranges from -90 to 90");
        var longitudeArgument = new Argument<decimal>("longitude", "A decimal number which ranges from -180 to +180");
        var command = new Command("RecordsByLatLong", "Gets all gazetteer records where the geometry intersects with the given latitude and longitude per batch of 100. Results are sorted by area, smallest to largest.")
        {
            latitudeArgument,
            longitudeArgument,
            TypeIdsArgument,
            OffsetOption
        };

        command.SetHandler(async (latitude, longitude, typeIds, offset) =>
        {
            var result = await Client.GetRecordsByLatLong(latitude, longitude, typeIds, offset);
            Program.Print(result);
        }, latitudeArgument, longitudeArgument, TypeIdsArgument, OffsetOption);

        return command;
    }
}