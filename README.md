[![License](https://img.shields.io/github/license/sn4k3/SpeciesDatabaseApi?style=for-the-badge)](https://github.com/sn4k3/SpeciesDatabaseApi/blob/master/LICENSE.txt)
[![GitHub repo size](https://img.shields.io/github/repo-size/sn4k3/SpeciesDatabaseApi?style=for-the-badge)](#)
[![Code size](https://img.shields.io/github/languages/code-size/sn4k3/SpeciesDatabaseApi?style=for-the-badge)](#)
[![Total code](https://img.shields.io/tokei/lines/github/sn4k3/SpeciesDatabaseApi?style=for-the-badge)](#)
[![Nuget](https://img.shields.io/nuget/v/SpeciesDatabaseApi?style=for-the-badge)](https://www.nuget.org/packages/SpeciesDatabaseApi)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/sn4k3?color=red&style=for-the-badge)](https://github.com/sponsors/sn4k3)

# Species Database Api

Queries and fetch data from species, taxon and conservation database(s) to retrieve information using the provider API.

## Clients

| Acronym                                 | Name                                           | Class       |
| --------------------------------------- | ---------------------------------------------- | ----------- |
| [WoRMS](https://www.marinespecies.org)  | World Register of Marine Species               | [WormsClient](https://github.com/sn4k3/SpeciesDatabaseApi/blob/master/SpeciesDatabaseApi/Worms/WormsClient.cs) |
| [IUCN](https://www.iucn.org)            | International Union for Conservation of Nature | [IucnClient](https://github.com/sn4k3/SpeciesDatabaseApi/blob/master/SpeciesDatabaseApi/Iucn/IucnClient.cs)  |


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
  private static readonly IucnClient Client = new IucnClient();

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
