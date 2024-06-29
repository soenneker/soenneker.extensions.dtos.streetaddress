using System.Text;
using Soenneker.Extensions.String;

namespace Soenneker.Extensions.Dtos.StreetAddress;

/// <summary>
/// A collection of helpful StreetAddress extension methods
/// </summary>
public static class StreetAddressExtension
{
    private const string _delimiter = ", ";

    /// <summary>
    /// Transforms a StreetAddress object into a formatted address string.
    /// </summary>
    /// <param name="address">The StreetAddress object to transform.</param>
    /// <returns>A formatted address string.</returns>
    public static string ToFormattedString(this Soenneker.Dtos.StreetAddress.StreetAddress address)
    {
        var sb = new StringBuilder(address.Street1.Length +
                                   (address.Street2?.Length ?? 0) +
                                   address.City.Length +
                                   (address.State?.Length ?? 0) +
                                   (address.Province?.Length ?? 0) +
                                   (address.Region?.Length ?? 0) +
                                   address.PostalCode.Length +
                                   (address.Country?.Length ?? 0) +
                                   (address.AdditionalInfo?.Length ?? 0) +
                                   20); // Adding an estimated length for delimiters

        sb.Append(address.Street1);

        if (!address.Street2.IsNullOrEmpty())
        {
            sb.Append(_delimiter).Append(address.Street2);
        }

        sb.Append(_delimiter).Append(address.City);

        if (!address.State.IsNullOrEmpty())
        {
            sb.Append(_delimiter).Append(address.State);
        }

        if (!address.Province.IsNullOrEmpty())
        {
            sb.Append(_delimiter).Append(address.Province);
        }

        if (!address.Region.IsNullOrEmpty())
        {
            sb.Append(_delimiter).Append(address.Region);
        }

        sb.Append(_delimiter).Append(address.PostalCode);

        if (!address.Country.IsNullOrEmpty())
        {
            sb.Append(_delimiter).Append(address.Country);
        }

        if (!address.AdditionalInfo.IsNullOrEmpty())
        {
            sb.Append(_delimiter).Append(address.AdditionalInfo);
        }

        return sb.ToString();
    }
}
