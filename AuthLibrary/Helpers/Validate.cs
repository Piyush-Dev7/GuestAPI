using GuestSharedModel.Constants;
using GuestSharedModel.Enums;
using System.Text.RegularExpressions;

namespace DataAccess.Core.Helpers
{
    public static class Validate
    {
        public static bool ValidatePhoneNumber(this string phoneNumber, Country country)
        {
            phoneNumber = phoneNumber?.Trim() ?? string.Empty;
            switch (country)
            {
                case Country.US:
                    return Regex.Match(phoneNumber, RegExConstants.USPhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.UK:
                    return Regex.Match(phoneNumber, RegExConstants.UKPhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.FR:
                    return Regex.Match(phoneNumber, RegExConstants.FrancePhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.DE:
                    return Regex.Match(phoneNumber, RegExConstants.GermanyPhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.CN:
                    return Regex.Match(phoneNumber, RegExConstants.ChinaMobilePhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.IN:
                    var isValidNumber = Regex.Match(phoneNumber, RegExConstants.IndiaPhoneRegEx, RegexOptions.IgnoreCase).Success;
                    if(phoneNumber.StartsWith("+91"))
                        phoneNumber = phoneNumber.Substring(3);
                    if (phoneNumber.StartsWith("0"))
                        phoneNumber = phoneNumber.Substring(1);
                    return isValidNumber;
                case Country.BR:
                    return Regex.Match(phoneNumber, RegExConstants.BrazilPhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.AU:
                    return Regex.Match(phoneNumber, RegExConstants.AustraliaPhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.NL:
                    return Regex.Match(phoneNumber, RegExConstants.DutchPhoneRegEx, RegexOptions.IgnoreCase).Success;
                case Country.SE:
                    return Regex.Match(phoneNumber, RegExConstants.SwedenPhoneRegEx, RegexOptions.IgnoreCase).Success;
                default:
                    return Regex.Match(phoneNumber, RegExConstants.IndiaPhoneRegEx , RegexOptions.IgnoreCase).Success;
            }
            
        }
    }
}
