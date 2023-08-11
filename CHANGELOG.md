# Change Log

## 12/08/2023 - v1.0.1

- Add Marine Regions (`MrClient`)
- Add `bool ThrowExceptionIfRequestStatusCodeFails` properties to clients:  
  - Gets or sets if it should throw an exception when the request code is other than success.  
  - If false it will return a null object
- Fix the regex for `WebsiteUrl` to better remove the api sub-domain

## 11/08/2023 - v1.0.0

- First release