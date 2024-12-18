using FluentAssertions;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Extensions.Dtos.StreetAddress.Tests;

[Collection("Collection")]
public class StreetAddressExtensionTests : FixturedUnitTest
{
    public StreetAddressExtensionTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void ToFormattedString_ValidAddress_ReturnsFormattedString()
    {
        // Arrange
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Street1 = "123 Main St",
            Street2 = "Apt 4B",
            City = "Springfield",
            State = "IL",
            PostalCode = "62704",
            Country = "USA",
            AdditionalInfo = "Near the big park"
        };

        // Act
        string formattedAddress = address.ToFormattedString();

        // Assert
        formattedAddress.Should().Be("123 Main St, Apt 4B, Springfield, IL, 62704, USA, Near the big park");
    }

    [Fact]
    public void ToFormattedString_AddressWithoutOptionalFields_ReturnsFormattedString()
    {
        // Arrange
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Street1 = "123 Main St",
            City = "Springfield",
            State = "IL",
            PostalCode = "62704"
        };

        // Act
        string formattedAddress = address.ToFormattedString();

        // Assert
        formattedAddress.Should().Be("123 Main St, Springfield, IL, 62704");
    }

    [Fact]
    public void ToFormattedString_AddressWithNullFields_ReturnsFormattedString()
    {
        // Arrange
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Street1 = "123 Main St",
            Street2 = null,
            City = "Springfield",
            State = "IL",
            PostalCode = "62704",
            Country = null,
            AdditionalInfo = null
        };

        // Act
        string formattedAddress = address.ToFormattedString();

        // Assert
        formattedAddress.Should().Be("123 Main St, Springfield, IL, 62704");
    }
}
