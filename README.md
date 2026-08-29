[![](https://img.shields.io/nuget/v/soenneker.extensions.dtos.streetaddress.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.dtos.streetaddress/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.dtos.streetaddress/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.dtos.streetaddress/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.extensions.dtos.streetaddress.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.extensions.dtos.streetaddress/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.extensions.dtos.streetaddress/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.extensions.dtos.streetaddress/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Extensions.Dtos.StreetAddress
A collection of helpful StreetAddress extension methods.

## Installation

```bash
dotnet add package Soenneker.Extensions.Dtos.StreetAddress
```

## Quick start

```csharp
using Soenneker.Extensions.Dtos.StreetAddress;

// Given an existing Soenneker.Dtos.StreetAddress.StreetAddress named address:
var result = address.ToFormattedString();
```

## Common operations

- `ToFormattedString()` - Converts a `StreetAddress` into a single-line formatted address string. Returns a single-line string with comma-separated address components.
- `ToFormattedHtmlString()` - Converts a `StreetAddress` into a multi-line formatted HTML address string. Returns an HTML-safe multi-line address string with `<br/>` line breaks. The country is appended in parentheses at the end.
