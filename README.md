[![](https://img.shields.io/nuget/v/soenneker.extensions.dtos.streetaddress.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.dtos.streetaddress/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.dtos.streetaddress/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.dtos.streetaddress/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.dtos.streetaddress.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.dtos.streetaddress/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.dtos.streetaddress/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.dtos.streetaddress/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Dtos.StreetAddress

Formats `StreetAddress` values as compact plain text or HTML-safe mailing-address text.

## Installation

```bash
dotnet add package Soenneker.Extensions.Dtos.StreetAddress
```

## Plain text

```csharp
using Soenneker.Dtos.StreetAddress;
using Soenneker.Extensions.Dtos.StreetAddress;

var address = new StreetAddress
{
    Line1 = "123 Main St",
    Line2 = "Apt 4B",
    City = "Springfield",
    State = "IL",
    PostalCode = "62704",
    Country = "US",
    AdditionalInfo = "Rear entrance"
};

string text = address.ToFormattedString();
// 123 Main St, Apt 4B, Springfield, IL, 62704, US, Rear entrance
```

`ToFormattedString()` joins populated components with `, ` and does not add placeholders or extra delimiters for missing values.

## HTML

```csharp
string html = address.ToFormattedHtmlString();
// 123 Main St<br/>Apt 4B<br/>Springfield, IL 62704 (US)
```

The HTML formatter places each street line on its own line and combines city, administrative area, postal code, and country on the final line. Every address component is HTML-encoded before insertion; the generated `<br/>` elements are the only markup. `AdditionalInfo` is included by the plain formatter but not by the HTML formatter.

When several administrative-area properties are populated, both formats use the first available value in this order: `State`, `Province`, then `Region`. Empty and null properties are skipped. Passing a null address is not supported.
