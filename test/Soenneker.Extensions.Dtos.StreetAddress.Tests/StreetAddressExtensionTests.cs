using AwesomeAssertions;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Extensions.Dtos.StreetAddress.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class StreetAddressExtensionTests : HostedUnitTest
{
    public StreetAddressExtensionTests(Host host) : base(host)
    {
    }

    [Test]
    public void ToFormattedString_ValidAddress_ReturnsFormattedString()
    {
        // Arrange
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Line1 = "123 Main St",
            Line2 = "Apt 4B",
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

    [Test]
    public void ToFormattedString_AddressWithoutOptionalFields_ReturnsFormattedString()
    {
        // Arrange
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Line1 = "123 Main St",
            City = "Springfield",
            State = "IL",
            PostalCode = "62704"
        };

        // Act
        string formattedAddress = address.ToFormattedString();

        // Assert
        formattedAddress.Should().Be("123 Main St, Springfield, IL, 62704");
    }

    [Test]
    public void ToFormattedString_AddressWithNullFields_ReturnsFormattedString()
    {
        // Arrange
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Line1 = "123 Main St",
            Line2 = null,
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

    [Test]
    public void ToFormattedString_skips_missing_leading_and_middle_components()
    {
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Province = "ON",
            Country = "CA"
        };

        address.ToFormattedString().Should().Be("ON, CA");
    }

    [Test]
    public void ToFormattedHtmlString_encodes_address_components()
    {
        var address = new Soenneker.Dtos.StreetAddress.StreetAddress
        {
            Line1 = "1 <Main> & Co",
            City = "A&B",
            State = "TX",
            PostalCode = "75001",
            Country = "US"
        };

        address.ToFormattedHtmlString().Should().Be("1 &lt;Main&gt; &amp; Co<br/>A&amp;B, TX 75001 (US)");
    }
}
