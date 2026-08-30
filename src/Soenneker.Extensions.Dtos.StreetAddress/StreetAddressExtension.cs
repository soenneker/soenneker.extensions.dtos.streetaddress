using System.Net;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Extensions.Dtos.StreetAddress;

/// <summary>
/// A collection of helpful StreetAddress extension methods
/// </summary>
public static class StreetAddressExtension
{
    private const string _delimiter = ", ";
    private const string _htmlLineBreak = "<br/>";

    /// <summary>
    /// Converts a <see cref="StreetAddress"/> into a single-line formatted address string.
    /// </summary>
    /// <param name="address">The <see cref="StreetAddress"/> object to format.</param>
    /// <returns>
    /// A single-line string with comma-separated address components.
    /// </returns>
    /// <example>
    /// <code>
    /// var address = new StreetAddress {
    ///     Line1 = "123 Main St",
    ///     Line2 = "Apt 4B",
    ///     City = "New York",
    ///     State = "NY",
    ///     PostalCode = "10001",
    ///     Country = "USA"
    /// };
    ///
    /// string result = address.ToFormattedString();
    /// // result: "123 Main St, Apt 4B, New York, NY, 10001, USA"
    /// </code>
    /// </example>
    public static string ToFormattedString(this Soenneker.Dtos.StreetAddress.StreetAddress address)
    {
        using var psb = new PooledStringBuilder(128);
        var hasComponent = false;

        if (!address.Line1.IsNullOrEmpty())
        {
            psb.Append(address.Line1);
            hasComponent = true;
        }

        if (!address.Line2.IsNullOrEmpty())
        {
            if (hasComponent)
                psb.Append(_delimiter);
            psb.Append(address.Line2);
            hasComponent = true;
        }

        if (!address.City.IsNullOrEmpty())
        {
            if (hasComponent)
                psb.Append(_delimiter);
            psb.Append(address.City);
            hasComponent = true;
        }

        string? administrativeArea = !address.State.IsNullOrEmpty()
            ? address.State
            : !address.Province.IsNullOrEmpty()
                ? address.Province
                : address.Region;

        if (!administrativeArea.IsNullOrEmpty())
        {
            if (hasComponent)
                psb.Append(_delimiter);
            psb.Append(administrativeArea);
            hasComponent = true;
        }

        if (!address.PostalCode.IsNullOrEmpty())
        {
            if (hasComponent)
                psb.Append(_delimiter);
            psb.Append(address.PostalCode);
            hasComponent = true;
        }

        if (!address.Country.IsNullOrEmpty())
        {
            if (hasComponent)
                psb.Append(_delimiter);
            psb.Append(address.Country);
            hasComponent = true;
        }

        if (!address.AdditionalInfo.IsNullOrEmpty())
        {
            if (hasComponent)
                psb.Append(_delimiter);
            psb.Append(address.AdditionalInfo);
        }

        return psb.ToString();
    }

    /// <summary>
    /// Converts a <see cref="StreetAddress"/> into a multi-line formatted HTML address string.
    /// </summary>
    /// <param name="address">The <see cref="StreetAddress"/> object to format.</param>
    /// <returns>
    /// An HTML-safe multi-line address string with <c>&lt;br/&gt;</c> line breaks.
    /// The country is appended in parentheses at the end.
    /// </returns>
    /// <example>
    /// <code>
    /// var address = new StreetAddress {
    ///     Line1 = "123 Main St",
    ///     Line2 = "Suite 500",
    ///     City = "Toronto",
    ///     Province = "ON",
    ///     PostalCode = "M5V 2T6",
    ///     Country = "Canada"
    /// };
    ///
    /// string result = address.ToFormattedHtmlString();
    /// // result:
    /// // "123 Main St&lt;br/&gt;Suite 500&lt;br/&gt;Toronto, ON M5V 2T6 (Canada)"
    /// </code>
    /// </example>
    public static string ToFormattedHtmlString(this Soenneker.Dtos.StreetAddress.StreetAddress address)
    {
        using var psb = new PooledStringBuilder(128);
        var hasOutput = false;

        if (!address.Line1.IsNullOrEmpty())
        {
            psb.Append(WebUtility.HtmlEncode(address.Line1));
            hasOutput = true;
        }

        if (!address.Line2.IsNullOrEmpty())
        {
            if (hasOutput)
                psb.Append(_htmlLineBreak);
            psb.Append(WebUtility.HtmlEncode(address.Line2));
            hasOutput = true;
        }

        string? administrativeArea = !address.State.IsNullOrEmpty()
            ? address.State
            : !address.Province.IsNullOrEmpty()
                ? address.Province
                : address.Region;

        bool hasLocalityLine = !address.City.IsNullOrEmpty() || !administrativeArea.IsNullOrEmpty() || !address.PostalCode.IsNullOrEmpty() ||
                               !address.Country.IsNullOrEmpty();

        if (hasLocalityLine && hasOutput)
            psb.Append(_htmlLineBreak);

        var hasLocalityComponent = false;

        if (!address.City.IsNullOrEmpty())
        {
            psb.Append(WebUtility.HtmlEncode(address.City));
            hasLocalityComponent = true;
        }

        if (!administrativeArea.IsNullOrEmpty())
        {
            if (hasLocalityComponent)
                psb.Append(", ");
            psb.Append(WebUtility.HtmlEncode(administrativeArea));
            hasLocalityComponent = true;
        }

        if (!address.PostalCode.IsNullOrEmpty())
        {
            if (hasLocalityComponent)
                psb.Append(' ');
            psb.Append(WebUtility.HtmlEncode(address.PostalCode));
            hasLocalityComponent = true;
        }

        if (!address.Country.IsNullOrEmpty())
        {
            if (hasLocalityComponent)
                psb.Append(" (");
            else
                psb.Append('(');
            psb.Append(WebUtility.HtmlEncode(address.Country));
            psb.Append(')');
        }

        return psb.ToString();
    }
}
