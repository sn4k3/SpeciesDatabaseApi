# Change Log

## 17/08/2023 - v1.2.1

- Improve the `BaseClient` and remove the overloads for url parameters, now `QueryParameters` must be used instead to populate the parameters collection
- Improve the `WormsClient` and `MarineRegionsCLient`, rename/shorten all methods

## 15/08/2023 - v1.2.0

- **BaseClient**: 
  - Add methods and types for Xml requests
  - Add methods for download files
  - Do not convert query parameters to lower-case
  - Handle `enums` with flags as lists on query parameters
- **WormsClient:** Add `GetAphiaLink(string|int)` method to get a link to WoRMS
- **IUCN:** Make `GetSpecieRedirectLink(string|int)` method static
- Add Barcode of Life Data Sytem (`BoldSystemsClient`)

## 13/08/2023 - v1.1.0

- Add Species+/CITES (`SpeciesPlusClient`)
- Move `Worms` to `MarineSpecies` namespace
- Move `Mr` to `MarineRegions` namespace
- Fix GET parameters with spaces between '&' making invalid urls
- Improve some type names for coherence

## 12/08/2023 - v1.0.1

- Add Marine Regions (`MrClient`)
- Add `bool ThrowExceptionIfRequestStatusCodeFails` properties to clients:  
  - Gets or sets if it should throw an exception when the request code is other than success.  
  - If false it will return a null object
- Fix the regex for `WebsiteUrl` to better remove the api sub-domain

## 11/08/2023 - v1.0.0
    
- First release