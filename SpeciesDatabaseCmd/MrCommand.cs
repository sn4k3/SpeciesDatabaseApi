using System.CommandLine;
using SpeciesDatabaseApi.Mr;

namespace SpeciesDatabaseCmd;

internal static class MrCommand
{
    private static readonly MrClient Client = new();

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
            GazetteerRecordCommand(),
            FullGazetteerRecordCommand(),

			GazetteerGeometriesCommand(),
            GazetteerTypesCommand(),
            GazetteerRecordsByNameCommand(),
            GazetteerRecordsByNamesCommand(),

            FeedCommand(),

            GazetteerWmSesCommand(),
            GazetteerRelationsCommand(),
            GazetteerSourcesCommand(),
            GazetteerSourceCommand(),
            GazetteerNamesCommand(),
            GazetteerRecordsBySourceCommand(),
            GazetteerRecordsByTypeCommand(),
            GazetteerRecordsByLatLongCommand()
		};

        return command;
    }

    private static Command GazetteerRecordCommand()
    {
        var command = new Command("GazetteerRecord", "Gets one record for the given MRGID.")
        {
            MrgIdArgument
        };

        command.SetHandler(async (mrgId) =>
        {
            var result = await Client.GetGazetteerRecord(mrgId);
            Program.Print(result);
        }, MrgIdArgument);

        return command;
    }

    private static Command FullGazetteerRecordCommand()
    {
	    var command = new Command("FullGazetteerRecord", "Gets one record for the given MRGID.")
	    {
		    MrgIdArgument
	    };

	    command.SetHandler(async (mrgId) =>
	    {
		    var result = await Client.GetFullGazetteerRecord(mrgId);
		    Program.Print(result);
	    }, MrgIdArgument);

	    return command;
    }

	private static Command GazetteerGeometriesCommand()
    {
        var command = new Command("GazetteerGeometries", "Gets all geometries associated with a gazetteer record.")
        {
            MrgIdArgument
        };

        command.SetHandler(async (mrgId) =>
        {
            var result = await Client.GetGazetteerGeometries(mrgId);
            Program.Print(result);
        }, MrgIdArgument);

        return command;
    }

    private static Command GazetteerTypesCommand()
    {
        var command = new Command("GazetteerTypes", "Gets all possible types.")
        {
        };

        command.SetHandler(async () =>
        {
            var result = await Client.GetGazetteerTypes();
            Program.Print(result);
        });

        return command;
    }

    private static Command GazetteerRecordsByNameCommand()
    {
        var command = new Command("GazetteerRecordsByName", "Gets a list of the first 100 matching records for given name.")
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
            var result = await Client.GetGazetteerRecords(name, like, fuzzy, typeIds, language, offset, count);
            Program.Print(result);
        }, NameArgument, TypeIdsArgument, LikeOption, FuzzyOption, LanguageOption, OffsetOption, CountOption);

        return command;
    }

    private static Command GazetteerRecordsByNamesCommand()
    {
	    var command = new Command("GazetteerRecordsByNames", "Gets a list of the first 100 matching records for given names.")
	    {
		    NamesArgument,

		    LikeOption,
		    FuzzyOption,
	    };

	    command.SetHandler(async (names, like, fuzzy) =>
	    {
		    var result = await Client.GetGazetteerRecords(names, like, fuzzy);
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

    private static Command GazetteerWmSesCommand()
    {
	    var command = new Command("GazetteerWMSes", "Gets WMS information for the given MRGID.")
	    {
		    MrgIdArgument
	    };

	    command.SetHandler(async (mrgId) =>
	    {
		    var result = await Client.GetgetGazetteerWmSes(mrgId);
		    Program.Print(result);
	    }, MrgIdArgument);

	    return command;
    }

    private static Command GazetteerRelationsCommand()
    {
	    var directionArgument = new Argument<MrRelationDirection>("direction", () => MrRelationDirection.Upper);
	    var typeArgument = new Argument<MrRelationType>("type", () => MrRelationType.PartOf);
	    var command = new Command("GazetteerRelations", "Gets the first 100 related records for the given MRGID.")
	    {
		    MrgIdArgument,
		    directionArgument,
            typeArgument
		};

	    command.SetHandler(async (mrgId, direction, type) =>
	    {
		    var result = await Client.GetGazetteerRelations(mrgId, direction, type);
		    Program.Print(result);
	    }, MrgIdArgument, directionArgument, typeArgument);

	    return command;
    }

    private static Command GazetteerSourcesCommand()
    {
	    var command = new Command("GazetteerSources", "Gets all sources per batch of 100.")
	    {
			OffsetArgument
		};

	    command.SetHandler(async (offset) =>
	    {
		    var result = await Client.GetGazetteerSources(offset);
		    Program.Print(result);
	    }, OffsetArgument);

	    return command;
    }

    private static Command GazetteerSourceCommand()
    {
	    var command = new Command("GazetteerSource", "Gets the source name corresponding to a source ID.")
	    {
		    SourceIdArgument
	    };

	    command.SetHandler(async (sourceId) =>
	    {
		    var result = await Client.GetGazetteerSource(sourceId);
		    Program.Print(result);
	    }, SourceIdArgument);

	    return command;
	}

    private static Command GazetteerNamesCommand()
    {
	    var command = new Command("GazetteerNames", "Gets the first 100 names for the given MRGID.")
	    {
		    MrgIdArgument
	    };

	    command.SetHandler(async (mrgId) =>
	    {
		    var result = await Client.GetGazetteerNames(mrgId);
		    Program.Print(result);
	    }, MrgIdArgument);

	    return command;
    }

    private static Command GazetteerRecordsBySourceCommand()
    {
	    var command = new Command("GazetteerRecordsBySource", "Gets the first 100 records for the given source.")
	    {
		    SourceNameArgument
	    };

	    command.SetHandler(async (sourceName) =>
	    {
		    var result = await Client.GetGazetteerRecordsBySource(sourceName);
		    Program.Print(result);
	    }, SourceNameArgument);

	    return command;
    }

    private static Command GazetteerRecordsByTypeCommand()
    {
	    var command = new Command("GazetteerRecordsByType", "Gets the first 100 records for the given source.")
	    {
			TypeNameArgument,
            OffsetArgument
		};

	    command.SetHandler(async (typeName, offset) =>
	    {
		    var result = await Client.GetGazetteerRecordsByType(typeName, offset);
		    Program.Print(result);
	    }, TypeNameArgument, OffsetArgument);

	    return command;
    }

    private static Command GazetteerRecordsByLatLongCommand()
    {
	    var latitudeArgument = new Argument<decimal>("latitude", "A decimal number which ranges from -90 to 90");
	    var longitudeArgument = new Argument<decimal>("longitude", "A decimal number which ranges from -180 to +180");
		var command = new Command("GazetteerRecordsByLatLong", "Gets all gazetteer records where the geometry intersects with the given latitude and longitude per batch of 100. Results are sorted by area, smallest to largest.")
	    {
		    latitudeArgument,
		    longitudeArgument,
			TypeIdsArgument,
		    OffsetOption
	    };

	    command.SetHandler(async (latitude, longitude, typeIds, offset) =>
	    {
		    var result = await Client.GetGazetteerRecordsByLatLong(latitude, longitude, typeIds, offset);
		    Program.Print(result);
	    }, latitudeArgument, longitudeArgument, TypeIdsArgument, OffsetOption);

	    return command;
    }
}