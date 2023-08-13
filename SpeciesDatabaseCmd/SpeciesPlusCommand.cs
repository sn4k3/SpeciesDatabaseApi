using System.CommandLine;
using SpeciesDatabaseApi.SpeciesPlus;

namespace SpeciesDatabaseCmd;

internal static class SpeciesPlusCommand
{
    private static readonly SpeciesPlusClient Client = new(string.Empty);

    private static readonly Argument<string> TokenArgument = new("api-token", "The api authentication token.");
    private static readonly Argument<int> TaxonConceptIdArgument = new("taxon-concept-id", "The taxonConceptId to search for.");
    private static readonly Argument<ScopeEnum> ScopeArgument = new("taxon-concept-id", () => ScopeEnum.Current, "Time scope of legislation.");
    private static readonly Argument<string> LanguageArgument = new("language", () => "en", "Select language for the text of legislation notes. Select en, fr, or es.");
    
    private static readonly Argument<string?> NameArgument = new("name", () => null, "Filter taxon concepts by name");

    private static readonly Option<string> LanguageOption = new(new[] { "-l", "--language" }, () => "en", "Select language for the text of legislation notes. Select en, fr, or es.");
    private static readonly Option<TaxomonyEnum> TaxomonyOption = new(new[] { "-t", "--taxonomy" }, () => TaxomonyEnum.CITES, "Filter taxon concepts by taxonomy.");
    private static readonly Option<DateTime?> UpdatedSinceOption = new(new[] { "-s", "--updated-since" }, () => null, "Pull only objects updated after (and including) the specified timestamp in ISO8601 format (UTC time).");
    private static readonly Option<bool> WithDescendantsOption = new(new[] { "--with-descendants" }, () => false, "Broadens the above search by name to include higher taxa.");
    private static readonly Option<bool> WithEuListingsOption = new(new[] { "--with-eu-listings" }, () => false, "Include EU listing data.");
    private static readonly Option<int> PageOption = new(new[]{"-p", "--page" }, () => 1, "Start record number, in order to page through next batch of results.");
    private static readonly Option<int> PerPageOption = new(new[]{"-c", "--count"}, () => 500, "Limit for how many objects returned per page for paginated responses. If not specified it will default to the maximum value of 500.");

    internal static Command CreateCommand()
    {
        var command = new Command(Client.ClientAcronym.ToUpper(), Program.GetRootCommandDescription(Client))
        {
            CitesLegislationCommand(),
            EuLegislationCommand(),
            DistributionsCommand(),
            ReferencesCommand(),
            TaxonConceptsCommand(),
        };

        return command;
    }

    private static Command CitesLegislationCommand()
    {
        var command = new Command("CitesLegislation", "Lists current CITES appendix listings and reservations, CITES quotas, and CITES suspensions for a given taxon concept.")
        {
            TokenArgument,
            TaxonConceptIdArgument,
            ScopeArgument,
            LanguageOption
        };

        command.SetHandler(async (token, taxonConceptId, scope, language) =>
        {
            Client.ApiToken.Value = token;
            var result = await Client.GetCitesLegislation(taxonConceptId, scope, language);
            Program.Print(result);
            if (result is null) return;

            Console.WriteLine();
            for (var i = 0; i < result.CitesListings.Length; i++)
            {
                Console.WriteLine($"# Listing[{i}]");
                Program.Print(result.CitesListings[i]);
            }

            Console.WriteLine();
            for (var i = 0; i < result.CitesQuotas.Length; i++)
            {
                Console.WriteLine($"# Quota[{i}]");
                Program.Print(result.CitesQuotas[i]);
            }

            Console.WriteLine();
            for (var i = 0; i < result.CitesSuspensions.Length; i++)
            {
                Console.WriteLine($"# Suspension[{i}]");
                Program.Print(result.CitesSuspensions[i]);
            }
        }, TokenArgument, TaxonConceptIdArgument, ScopeArgument, LanguageOption);

        return command;
    }

    private static Command EuLegislationCommand()
    {
        var command = new Command("EuLegislation", "Lists current EU annex listings, SRG opinions, and EU suspensions for a given taxon concept.")
        {
            TokenArgument,
            TaxonConceptIdArgument,
            ScopeArgument,
            LanguageOption
        };

        command.SetHandler(async (token, taxonConceptId, scope, language) =>
        {
            Client.ApiToken.Value = token;
            var result = await Client.GetEuLegislation(taxonConceptId, scope, language);
            Program.Print(result);
            if (result is null) return;

            Console.WriteLine();
            for (var i = 0; i < result.Listings.Length; i++)
            {
                Console.WriteLine($"# Listing[{i}]");
                Program.Print(result.Listings[i]);
            }

            Console.WriteLine();
            for (var i = 0; i < result.Decisions.Length; i++)
            {
                Console.WriteLine($"# Decision[{i}]");
                Program.Print(result.Decisions[i]);
            }

        }, TokenArgument, TaxonConceptIdArgument, ScopeArgument, LanguageOption);

        return command;
    }

    private static Command DistributionsCommand()
    {
        var command = new Command("Distributions", "Lists distributions for a given taxon concept.")
        {
            TokenArgument,
            TaxonConceptIdArgument,
            LanguageArgument
        };

        command.SetHandler(async (token, taxonConceptId, language) =>
        {
            Client.ApiToken.Value = token;
            var results = await Client.GetDistributions(taxonConceptId, language);
            Program.Print(results);
            
        }, TokenArgument, TaxonConceptIdArgument, LanguageArgument);

        return command;
    }

    private static Command ReferencesCommand()
    {
        var command = new Command("References", "Lists references for a given taxon concept.")
        {
            TokenArgument,
            TaxonConceptIdArgument,
            LanguageArgument
        };

        command.SetHandler(async (token, taxonConceptId) =>
        {
            Client.ApiToken.Value = token;
            var results = await Client.GetReferences(taxonConceptId);
            Program.Print(results);

        }, TokenArgument, TaxonConceptIdArgument);

        return command;
    }

    private static Command TaxonConceptsCommand()
    {
        var command = new Command("TaxonConcepts", "Lists taxon concepts.")
        {
            TokenArgument,
            NameArgument,
            TaxomonyOption,
            WithDescendantsOption,
            WithEuListingsOption,
            UpdatedSinceOption,
            LanguageOption,
            PageOption,
        };

        command.SetHandler(async (token, name, taxomony, withDescendants, withEuListings, updatedSince, language, page) =>
        {
            Client.ApiToken.Value = token;
            var results = await Client.GetTaxonConcepts(name, taxomony, withDescendants, withEuListings, updatedSince, language, page);

            Program.Print(results);
            if (results == null) return;

            Program.Print(results.Pagination);
            Program.Print(results.Concepts);

        }, TokenArgument, NameArgument, TaxomonyOption, WithDescendantsOption, WithEuListingsOption,
            UpdatedSinceOption, LanguageOption, PageOption);

        return command;
    }

}