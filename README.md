[![License](https://img.shields.io/github/license/sn4k3/SpeciesDatabaseApi?style=for-the-badge)](https://github.com/sn4k3/SpeciesDatabaseApi/blob/master/LICENSE.txt)
[![GitHub repo size](https://img.shields.io/github/repo-size/sn4k3/SpeciesDatabaseApi?style=for-the-badge)](#)
[![Code size](https://img.shields.io/github/languages/code-size/sn4k3/SpeciesDatabaseApi?style=for-the-badge)](#)
[![Nuget](https://img.shields.io/nuget/v/SpeciesDatabaseApi?style=for-the-badge)](https://www.nuget.org/packages/SpeciesDatabaseApi)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/sn4k3?color=red&style=for-the-badge)](https://github.com/sponsors/sn4k3)
<!--[![Downloads](https://img.shields.io/github/downloads/sn4k3/SpeciesDatabaseApi/total?style=for-the-badge)](https://github.com/sn4k3/SpeciesDatabaseApi/releases)!-->

# Species Database Api

Queries and fetch data from species, taxon and conservation database(s) to retrieve information using the provider API.

## Clients

| Acronym                                 | Name                                           | Class                                                                                                          | Terms of use                                                  |
| --------------------------------------- | ---------------------------------------------- | -------------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------- | 
| [WoRMS](https://www.marinespecies.org)  | World Register of Marine Species               | [WormsClient](https://github.com/sn4k3/SpeciesDatabaseApi/blob/master/SpeciesDatabaseApi/Worms/WormsClient.cs) | [Terms of use](https://www.marinespecies.org/about.php#terms) |
| [IUCN](https://www.iucn.org)            | International Union for Conservation of Nature | [IucnClient](https://github.com/sn4k3/SpeciesDatabaseApi/blob/master/SpeciesDatabaseApi/Iucn/IucnClient.cs)    | [Terms of use](http://apiv3.iucnredlist.org/about) |
| [MR](https://www.marineregions.org)     | Marine Regions                                 | [MrClient](https://github.com/sn4k3/SpeciesDatabaseApi/blob/master/SpeciesDatabaseApi/Mr/MrClient.cs)          | [Terms of use](https://www.marineregions.org/disclaimer.php) |

## Terms of use

Before the use of any provider you must accept and follow the terms of use of each used client. 
Please refer to the "terms of use" from the above links.  

## Structure

- Calls follow the async programming
- Returned data have a class data model

## Example (WoRMS)

```C#
  private static readonly WormsClient Client = new WormsClient();

  private async void Main()
  {
     var result = await Client.GetAphiaRecordByAphiaId(105792);
     Console.WriteLine(result);
  }
```

<details>
  <summary>Result:</summary>

```text
AphiaId: 105792  
Url: https://marinespecies.org/aphia.php?p=taxdetails&id=105792  
ScientificName: Carcharhinus leucas  
Authority: (Müller & Henle 1839)  
TaxonRankId: 220  
Rank: Species  
Status: accepted  
UnacceptReason:  
ValidAphiaID: 105792  
ValidName: Carcharhinus leucas  
ValidAuthority: (Müller & Henle 1839)  
ParentNameUsageId: 105719  
Kingdom: Animalia  
Phylum: Chordata  
Class: Elasmobranchii  
Order: Carcharhiniformes  
Family: Carcharhinidae  
Genus: Carcharhinus  
Citation: Froese R. and D. Pauly. Editors. (2023). FishBase. Carcharhinus leucas (Müller & Henle 1839). Accessed through: World Register of Marine Species at: https://marinespecies.org/aphia.php?p=taxdetails&id=105792 on 2023-08-09  
lsId: urn:lsid:marinespecies.org:taxname:105792  
IsMarine: 1  
IsBrackish: 1  
IsFreshwater: 1  
IsTerrestrial: 0  
IsExtinct:  
MatchType: Exact  
Modified: 15/01/2008 17:27:08  
```
</details>


## Example (IUCN)

```C#
  private static readonly IucnClient Client = new IucnClient("your-api-key");

  private async void Main()
  {
     var results = await Client.GetSpecieCommonNames("Carcharodon carcharias");
     foreach(var result in results)
     {
        Console.WriteLine(result);
     }
  }
```

<details>
  <summary>Result:</summary>

```text
TaxonName: White Shark, Primary: True, Language: eng
TaxonName: Great White Shark, Primary: False, Language: eng
```
</details>

## Command-line

The project **[SpeciesDatabaseCmd](https://github.com/sn4k3/SpeciesDatabaseApi/tree/master/SpeciesDatabaseCmd)** allow to call all the API using the command-line and also provide a sample on how to use the library.  
Run the "SpeciesDatabaseCmd.exe" and follow the in-terminal instructions to call the commands.

### Example: 

```bash
# Usage:
#   SpeciesDatabaseCmd [command] [options]
# 
# Options:
#   --version       Show version information
#   -?, -h, --help  Show help and usage information
# 
# Commands:
#  WORMS  Query - World Register of Marine Species (https://marinespecies.org)
#  IUCN   Query - International Union for Conservation of Nature (http://iucnredlist.org)
#  MR     Query - Marine Regions (https://marineregions.org)

SpeciesDatabaseCmd.exe IUCN SpecieCommonNames "Carcharodon carcharias"

> Name: Carcharodon carcharias
> Error:
> Message:
> IsSuccess: True
> Count: 2
> Results: 2
> ## Result[0]:
> TaxonName: White Shark
> Primary: True
> Language: eng
> ## Result[1]:
> TaxonName: Great White Shark
> Primary: False
> Language: eng
```


# Link package (Visual Studio)

- Via "Manage NuGet packages"
- Manually via terminal:
```powershell
dotnet add package SpeciesDatabaseApi
```
