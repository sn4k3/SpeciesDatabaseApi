using System.CommandLine;
using SpeciesDatabaseApi.Iucn;

namespace SpeciesDatabaseCmd;

internal static class IucnCommand
{
    private static readonly IucnClient Client = new(string.Empty);

    private static readonly Argument<string> CountryIsoCodeArgument = new("country-code", "The country iso code.");
    private static readonly Argument<string> SpecieNameArgument = new("specie-name", "The name of the specie.");
    private static readonly Argument<int> SpecieIdArgument = new("specie-id", "The id of the specie.");
    private static readonly Argument<string> RegionIdentifierArgument = new("region-id", () => "global", "The region identifier to search.");
    private static readonly Argument<QueryClassificationEnum> CategoryArgument = new("classification", "The classification to search.");
    private static readonly Argument<string> ComprehensiveGroupArgument = new("comprehensive-group", "The name of the comprehensive group.");


    private static readonly Option<int> PageOption = new(new[]{"-p", "--page"}, "Sets region identifier");
    private static readonly Option<string> ApiTokenOption = new(new[]{"-t", "--token"}, () => "9bb4facb6d23f48efbf424bb05c0c1ef1cf6f468393bc745d42179ac4aca5fee", "Sets the token for the api calls");

    internal static Command CreateCommand()
    {
        var command = new Command(Client.ClientAcronym.ToUpper(), Program.GetRootCommandDescription(Client))
        {
            VersionCommand(),

            CountriesCommand(),
            CountrySpeciesCommand(),

            RegionsCommand(),

            SpeciesCommand(),
            SpeciesCountCommand(),
            SpecieCitationByNameCommand(),
            SpecieCitationByIdCommand(),
            SpeciesByCategoryCommand(),
            SpecieByNameCommand(),
            SpecieByIdCommand(),
            SpecieNarrativeByNameCommand(),
            SpecieNarrativeByIdCommand(),
            SpecieSynonymsCommand(),
            SpecieCommonNamesCommand(),
            SpecieCountriesByIdCommand(),
            SpecieCountriesByNameCommand(),
            SpecieHistoryByIdCommand(),
            SpecieHistoryByNameCommand(),

            SpecieThreatsByIdCommand(),
            SpecieThreatsByNameCommand(),

            SpecieHabitatsByIdCommand(),
            SpecieHabitatsByNameCommand(),

            SpecieConservationMeasuresByIdCommand(),
            SpecieConservationMeasuresByNameCommand(),

            PlantGrowthFormsByIdCommand(),
            PlantGrowthFormsByNameCommand(),

            ComprehensiveGroupsCommand(),
            SpeciesByComprehensiveGroupCommand(),

            SpecieWeblinkCommand()
        };

        return command;
    }

    private static Command VersionCommand()
    {
        var command = new Command("Version", "Gets what version of the IUCN Red List is driving the API.")
        {
        };

        command.SetHandler(async () =>
        {
            var result = await Client.GetVersion();
            Program.Print(result);
        });

        return command;
    }

    private static Command CountriesCommand()
    {
        var command = new Command("Countries", "Gets a list of countries.")
        {
            ApiTokenOption
        };

        command.SetHandler(async (apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetCountries();
            Program.Print(result);
        }, ApiTokenOption);

        return command;
    }

    private static Command CountrySpeciesCommand()
    {
        var command = new Command("CountrySpecies", "Gets a list of species by country iso-code.")
        {
            CountryIsoCodeArgument,
            ApiTokenOption
        };

        command.SetHandler(async (countryIsoCode, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetCountrySpecies(countryIsoCode);
            Program.Print(result);
        }, CountryIsoCodeArgument, ApiTokenOption);

        return command;
    }

    private static Command RegionsCommand()
    {
        var command = new Command("Regions", "Gets a list of regions.")
        {
            ApiTokenOption
        };

        command.SetHandler(async (apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetRegions();
            Program.Print(result);
        }, ApiTokenOption);

        return command;
    }

    private static Command SpeciesCommand()
    {
        var command = new Command("Species", "Gets a list of all the species published, as well as the Red List category.")
        {
            RegionIdentifierArgument,
            PageOption,
            ApiTokenOption
        };

        command.SetHandler(async (regionIdentifier, page, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecies(regionIdentifier, page);
            Program.Print(result);
        }, RegionIdentifierArgument, PageOption,ApiTokenOption);

        return command;
    }

    private static Command SpeciesCountCommand()
    {
        var command = new Command("SpeciesCount", "Gets the total Species count.")
        {
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (regionIdentifier, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpeciesCount(regionIdentifier);
            Program.Print(result);
        }, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieCitationByNameCommand()
    {
        var command = new Command("SpecieCitationByName", "Gets the citation for a given species assessment.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (speciesName, regionIdentifier, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieCitation(speciesName, regionIdentifier);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieCitationByIdCommand()
    {
        var command = new Command("SpecieCitationById", "Gets the citation for a given species assessment.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (speciesId, regionIdentifier, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieCitation(speciesId, regionIdentifier);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpeciesByCategoryCommand()
    {
        var command = new Command("SpeciesByCategory", "Gets a list of species by category.")
        {
            CategoryArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (category, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpeciesByCategory(category);
            Program.Print(result);
        }, CategoryArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieByIdCommand()
    {
        var command = new Command("SpecieById", "Gets information about individual specie.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecie(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieByNameCommand()
    {
        var command = new Command("SpecieByName", "Gets information about individual specie.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecie(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieNarrativeByNameCommand()
    {
        var command = new Command("SpecieNarrativeByName", "Gets narrative information about individual species.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieNarrative(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieNarrativeByIdCommand()
    {
        var command = new Command("SpecieNarrativeById", "Gets narrative information about individual species.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieNarrative(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieSynonymsCommand()
    {
        var command = new Command("SpecieSynonyms", "Can be used to either gain information about synonyms via an accepted species name, or vice versa.")
        {
            SpecieNameArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName,  apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieSynonyms(specieName);
            Program.Print(result);
        }, SpecieNameArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieCommonNamesCommand()
    {
        var command = new Command("SpecieCommonNames", "Gets a list of common names per species.")
        {
            SpecieNameArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieCommonNames(specieName);
            Program.Print(result);
        }, SpecieNameArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieCountriesByIdCommand()
    {
        var command = new Command("SpecieCountriesById", "Gets a list of countries of occurrence by species id.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieCountries(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieCountriesByNameCommand()
    {
        var command = new Command("SpecieCountriesByName", "Gets a list of countries of occurrence by species id.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieCountries(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieHistoryByIdCommand()
    {
        var command = new Command("SpecieHistoryById", "Gets a list of historical assessments by specie id.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieHistory(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieHistoryByNameCommand()
    {
        var command = new Command("SpecieHistoryByName", "Gets a list of historical assessments by specie name.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieHistory(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieThreatsByIdCommand()
    {
        var command = new Command("SpecieThreatsById", "Gets a list of threats by specie id.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieThreats(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieThreatsByNameCommand()
    {
        var command = new Command("SpecieThreatsByName", "Gets a list of threats by specie name.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieThreats(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieHabitatsByIdCommand()
    {
        var command = new Command("SpecieHabitatsById", "Gets a list of habitats by specie id.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieHabitats(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieHabitatsByNameCommand()
    {
        var command = new Command("SpecieHabitatsByName", "Gets a list of habitats by specie name.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieHabitats(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieConservationMeasuresByIdCommand()
    {
        var command = new Command("SpecieConservationMeasuresById", "Gets a list of conservation measures by specie id.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieConservationMeasures(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieConservationMeasuresByNameCommand()
    {
        var command = new Command("SpecieConservationMeasuresByName", "Gets a list of conservation measures by specie name.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieConservationMeasures(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command PlantGrowthFormsByIdCommand()
    {
        var command = new Command("PlantGrowthFormsById", "Gets a list of plant growth forms by specie id.")
        {
            SpecieIdArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieId, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetPlantGrowthForms(specieId, regionId);
            Program.Print(result);
        }, SpecieIdArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command PlantGrowthFormsByNameCommand()
    {
        var command = new Command("PlantGrowthFormsByName", "Gets a list of plant growth forms by specie name.")
        {
            SpecieNameArgument,
            RegionIdentifierArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, regionId, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetPlantGrowthForms(specieName, regionId);
            Program.Print(result);
        }, SpecieNameArgument, RegionIdentifierArgument, ApiTokenOption);

        return command;
    }

    private static Command ComprehensiveGroupsCommand()
    {
        var command = new Command("ComprehensiveGroups", "Gets a list of comprehensive groups.")
        {
            ApiTokenOption
        };

        command.SetHandler(async (apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetComprehensiveGroups();
            Program.Print(result);
        }, ApiTokenOption);

        return command;
    }

    private static Command SpeciesByComprehensiveGroupCommand()
    {
        var command = new Command("SpeciesByComprehensiveGroup", "Gets a list of species by comprehensive group.")
        {
            ComprehensiveGroupArgument,
            ApiTokenOption
        };

        command.SetHandler(async (comprehensiveGroup,  apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpeciesByComprehensiveGroup(comprehensiveGroup);
            Program.Print(result);
        }, ComprehensiveGroupArgument, ApiTokenOption);

        return command;
    }

    private static Command SpecieWeblinkCommand()
    {
        var command = new Command("SpecieWeblink", "Gets the direct link of the species on the Red List website.")
        {
            SpecieNameArgument,
            ApiTokenOption
        };

        command.SetHandler(async (specieName, apiToken) =>
        {
            Client.ApiToken.Value = apiToken;
            var result = await Client.GetSpecieWeblink(specieName);
            Program.Print(result);
        }, SpecieNameArgument, ApiTokenOption);

        return command;
    }
}