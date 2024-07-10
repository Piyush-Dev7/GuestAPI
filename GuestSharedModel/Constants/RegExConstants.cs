namespace GuestSharedModel.Constants
{
    public class RegExConstants
    {
        #region PhoneNumbers
        public const string USPhoneRegEx = @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}"; //North America
        public const string UKPhoneRegEx = @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$";
        public const string FrancePhoneRegEx = @"^(?:(?:\+|00)33[\s.-]{0,3}(?:\(0\)[\s.-]{0,3})?|0)[1-9](?:(?:[\s.-]?\d{2}){4}|\d{2}(?:[\s.-]?\d{3}){2})$";
        public const string GermanyPhoneRegEx = @"(\(?([\d \-\)\–\+\/\(]+){6,}\)?([ .\-–\/]?)([\d]+))";
        public const string ChinaPhoneRegEx = @"^(?:(?:\d{3}-)?\d{8}|^(?:\d{4}-)?\d{7,8})(?:-\d+)?$";
        public const string ChinaMobilePhoneRegEx = @"^(?:(?:\+|00)86)?1(?:(?:3[\d])|(?:4[5-79])|(?:5[0-35-9])|(?:6[5-7])|(?:7[0-8])|(?:8[\d])|(?:9[189]))\d{8}$";
        public const string IndiaPhoneRegEx = @"^([0|\+[0-9]{1,5})?([7-9][0-9]{9})$";
        public const string BrazilPhoneRegEx = @"(([0-9]{2}|0{1}((x|[0-9]){2}[0-9]{2}))\)\s*[0-9]{3,4}[- ]*[0-9]{4}";
        public const string AustraliaPhoneRegEx = @"(^1300\d{6}$)|(^1800|1900|1902\d{6}$)|(^0[2|3|7|8]{1}[0-9]{8}$)|(^13\d{4}$)|(^04\d{2,3}\d{6}$)";
        public const string DutchPhoneRegEx = @"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)";
        public const string SwedenPhoneRegEx = @"^(([+]\d{2}[ ][1-9]\d{0,2}[ ])|([0]\d{1,3}[-]))((\d{2}([ ]\d{2}){2})|(\d{3}([ ]\d{3})*([ ]\d{2})+))$";
        //more custom country regEx to be added here
        #endregion
    }
}
