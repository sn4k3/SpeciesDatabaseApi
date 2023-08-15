using System.CommandLine;
using SpeciesDatabaseApi.BoldSystems;

namespace SpeciesDatabaseCmd;

internal static class BoldSystemsCommand
{
    private static readonly BoldSystemsClient Client = new();

    private static readonly Argument<BoldStatsDataType> StatsDataTypeArgument = new("data-type", () => BoldStatsDataType.DrillDown, "Returns all records in one of the specified formats.");
    private static readonly Option<string?> TaxonOption = new(new[] { "--taxon" }, "Returns all records containing matching taxa, defined in a pipe delimited list.");
    private static readonly Option<string?> IdsOption = new(new[] { "--ids" }, "Returns all records containing matching IDs, defined in a pipe delimited list.");
    private static readonly Option<string?> BinOption = new(new[] { "--bin" }, "Returns all records contained in matching BINs, defined in a pipe delimited list.");
    private static readonly Option<string?> ContainerOption = new(new[] { "--container" }, "Returns all records contained in matching projects or datasets, in a pipe delimited list.");
    private static readonly Option<string?> InstitutionsOption = new(new[] { "--institutions" }, "Returns all records stored in matching institutions, defined in a pipe delimited list.");
    private static readonly Option<string?> ResearchersOption = new(new[] { "--researchers" }, "Returns all records containing matching researcher names, defined in a pipe delimited list.");
    private static readonly Option<string?> GeoOption = new(new[] { "--geo" }, "Returns all records collected in matching geographic sites, defined in a pipe delimited list.");
    private static readonly Option<string?> MarkerOption = new(new[] { "--marker" }, "Returns all specimen records containing matching marker codes defined in a pipe delimited list.");

    private static readonly Argument<FileInfo> FilePathArgument = new("file-path", "Specifies where to save the file.");


    private static readonly Argument<BoldDatabaseEnum> DataBaseArgument = new("db", "Specifies the identification database to query.");
    private static readonly Argument<string> SequenceArgument = new("sequence", "Specifies the query sequence.");
    private static readonly Argument<int> TaxIdArgument = new("tax-id", "The taxId to search for.");
    private static readonly Argument<BoldDataTypesEnum> DataTypesArgument = new("data-types", () => BoldDataTypesEnum.Basic, "Specifies the datatypes that will be returned.");

    private static readonly Argument<string> TaxNameArgument = new("tax-name", "The tax name to search for.");
    private static readonly Argument<bool> FuzzyArgument = new("fuzzy", () => false, "Specifies if the search should only find exact matches.");
    
    private static readonly Option<bool> IncludeTreeOption = new(new[] { "-t", "--include-tree" }, () => false, "Returns a list containing information for parent taxa as well as the specified taxon.");

    internal static Command CreateCommand()
    {
        var command = new Command(Client.ClientAcronym.ToUpper(), Program.GetRootCommandDescription(Client))
        {
            StatsCommand(),
            SpecimenCommand(),
            SequencesCommand(),
            SpecimenAndSequenceCommand(),

            DownloadTraceFileCommand(),

            TaxonDataByIdCommand(),
            TaxonDataByNameCommand(),
            CoiMatchesCommand(),
        };

        return command;
    }

    private static Command StatsCommand()
    {
        var command = new Command("Stats", "Users only interested in count data for a given query can now use the API to retrieve the summary information that is provided by BOLD public searches.")
        {
            StatsDataTypeArgument,
            TaxonOption,
            IdsOption,
            BinOption,
            ContainerOption,
            InstitutionsOption,
            ResearchersOption,
            GeoOption
        };

        command.SetHandler(async (dataType, taxon, ids, bin, container, institutions, researchers, geo) =>
        {
            var result = await Client.GetStats(new PublicApiParameters
            {
                Taxon = taxon,
                Ids = ids,
                Bin = bin,
                Container = container,
                Institutions = institutions,
                Researchers = researchers,
                Geo = geo
            }, dataType);
            Program.Print(result);

        }, StatsDataTypeArgument, TaxonOption, IdsOption, BinOption, ContainerOption, InstitutionsOption, ResearchersOption, GeoOption);

        return command;
    }

    private static Command SpecimenCommand()
    {
        var command = new Command("Specimen", "Users can query the system to retrieve matching specimen data records for a combination of parameters.")
        {
            TaxonOption,
            IdsOption,
            BinOption,
            ContainerOption,
            InstitutionsOption,
            ResearchersOption,
            GeoOption
        };

        command.SetHandler(async (taxon, ids, bin, container, institutions, researcher, geo) =>
        {
            var result = await Client.GetSpecimen(new PublicApiParameters
            {
                Taxon = taxon,
                Ids = ids,
                Bin = bin,
                Container = container,
                Institutions = institutions,
                Researchers = researcher,
                Geo = geo
            });
            Program.Print(result?.BoldRecords.Records);

        }, TaxonOption, IdsOption, BinOption, ContainerOption, InstitutionsOption, ResearchersOption, GeoOption);

        return command;
    }

    private static Command SequencesCommand()
    {
        var command = new Command("Sequences", "Users can query the system to retrieve matching sequences for a combination of parameters.")
        {
            TaxonOption,
            IdsOption,
            BinOption,
            ContainerOption,
            InstitutionsOption,
            ResearchersOption,
            GeoOption,
            MarkerOption
        };

        command.SetHandler(async (taxon, ids, bin, container, institutions, researcher, geo, marker) =>
        {
            var result = await Client.GetSequences(new PublicApiParameters
            {
                Taxon = taxon,
                Ids = ids,
                Bin = bin,
                Container = container,
                Institutions = institutions,
                Researchers = researcher,
                Geo = geo,
                Marker = marker
            });
            Program.Print(result);

        }, TaxonOption, IdsOption, BinOption, ContainerOption, InstitutionsOption, ResearchersOption, GeoOption, MarkerOption);

        return command;
    }

    private static Command SpecimenAndSequenceCommand()
    {
        var command = new Command("SpecimenAndSequence", "Users can query the system to retrieve matching specimen data and sequence records for a combination of parameters.")
        {
            TaxonOption,
            IdsOption,
            BinOption,
            ContainerOption,
            InstitutionsOption,
            ResearchersOption,
            GeoOption,
            MarkerOption
        };

        command.SetHandler(async (taxon, ids, bin, container, institutions, researcher, geo, marker) =>
        {
            var result = await Client.GetSpecimenAndSequence(new PublicApiParameters
            {
                Taxon = taxon,
                Ids = ids,
                Bin = bin,
                Container = container,
                Institutions = institutions,
                Researchers = researcher,
                Geo = geo,
                Marker = marker
            });
            Program.Print(result?.BoldRecords.Records);

        }, TaxonOption, IdsOption, BinOption, ContainerOption, InstitutionsOption, ResearchersOption, GeoOption, MarkerOption);

        return command;
    }

    private static Command DownloadTraceFileCommand()
    {
        var command = new Command("DownloadTraceFile", "Users can query the system to retrieve matching specimen data records for a combination of parameters.")
        {
            FilePathArgument,
            TaxonOption,
            IdsOption,
            BinOption,
            ContainerOption,
            ResearchersOption,
            GeoOption,
            MarkerOption
        };

        command.SetHandler(async (filePath, taxon, ids, bin, container, researcher, geo, marker) =>
        {
            await Client.DownloadTraceFile(new PublicApiParameters
            {
                Taxon = taxon,
                Ids = ids,
                Bin = bin,
                Container = container,
                Researchers = researcher,
                Geo = geo,
                Marker = marker
            }, filePath.FullName);

        }, FilePathArgument, TaxonOption, IdsOption, BinOption, ContainerOption, ResearchersOption, GeoOption, MarkerOption);

        return command;
    }

    private static Command TaxonDataByIdCommand()
    {
        var command = new Command("TaxonDataById", "Retrieves taxonomy information by BOLD taxonomy ID.")
        {
            TaxIdArgument,
            DataTypesArgument,
            IncludeTreeOption
        };

        command.SetHandler(async (taxId, dataTypes, includeTree) =>
        {
            if (includeTree)
            {
                var result = await Client.GetTaxonDataIncludeTree(taxId, dataTypes);
                Program.Print(result);
            }
            else
            {
                var result = await Client.GetTaxonData(taxId, dataTypes);
                Program.Print(result);
            }
            
        }, TaxIdArgument, DataTypesArgument, IncludeTreeOption);

        return command;
    }

    private static Command TaxonDataByNameCommand()
    {
        var command = new Command("TaxonDataByName", "Retrieves taxonomy information by BOLD taxonomy name.")
        {
            TaxNameArgument,
            FuzzyArgument,
        };

        command.SetHandler(async (taxName, fuzzy) =>
        {
            var result = await Client.GetTaxonData(taxName, fuzzy);
            Program.Print(result?.TopMatchedNames);

        }, TaxNameArgument, FuzzyArgument);

        return command;
    }

    private static Command CoiMatchesCommand()
    {
        var command = new Command("CoiMatches", "Query the COI ID Engine.")
        {
            DataBaseArgument,
            SequenceArgument,
        };

        command.SetHandler(async (db, sequence) =>
        {
            var result = await Client.GetCoiMatches(db, sequence);
            Program.Print(result);
        }, DataBaseArgument, SequenceArgument);

        return command;
    }

    

}