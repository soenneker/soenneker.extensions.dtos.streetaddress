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

        psb.Append(address.Line1);

        if (!address.Line2.IsNullOrEmpty())
        {
            psb.Append(_delimiter);
            psb.Append(address.Line2);
        }

        psb.Append(_delimiter);
        psb.Append(address.City);

        if (!address.State.IsNullOrEmpty())
        {
            psb.Append(_delimiter);
            psb.Append(address.State);
        }
        else if (!address.Province.IsNullOrEmpty())
        {
            psb.Append(_delimiter);
            psb.Append(address.Province);
        }
        else if (!address.Region.IsNullOrEmpty())
        {
            psb.Append(_delimiter);
            psb.Append(address.Region);
        }

        psb.Append(_delimiter);
        psb.Append(address.PostalCode);

        if (!address.Country.IsNullOrEmpty())
        {
            psb.Append(_delimiter);
            psb.Append(address.Country);
        }

        if (!address.AdditionalInfo.IsNullOrEmpty())
        {
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

        psb.Append(address.Line1);

        if (!address.Line2.IsNullOrEmpty())
        {
            psb.Append(_htmlLineBreak);
            psb.Append(address.Line2);
        }

        var hasLine3 = false;

        if (!address.City.IsNullOrEmpty())
        {
            psb.Append(_htmlLineBreak);
            psb.Append(address.City);
            hasLine3 = true;
        }

        if (!address.State.IsNullOrEmpty())
        {
            psb.Append(hasLine3 ? ", " : _htmlLineBreak);
            psb.Append(address.State);
            hasLine3 = true;
        }
        else if (!address.Province.IsNullOrEmpty())
        {
            psb.Append(hasLine3 ? ", " : _htmlLineBreak);
            psb.Append(address.Province);
            hasLine3 = true;
        }
        else if (!address.Region.IsNullOrEmpty())
        {
            psb.Append(hasLine3 ? ", " : _htmlLineBreak);
            psb.Append(address.Region);
            hasLine3 = true;
        }

        if (!address.PostalCode.IsNullOrEmpty())
        {
            psb.Append(hasLine3 ? " " : _htmlLineBreak);
            psb.Append(address.PostalCode);
        }

        if (!address.Country.IsNullOrEmpty())
        {
            psb.Append(" (");
            psb.Append(address.Country);
            psb.Append(')');
        }

        return psb.ToString();
    }
}
