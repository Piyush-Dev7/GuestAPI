using GuestSharedModel.Enums;
using System.Text.Json.Serialization;

namespace GuestSharedModel.DtoModels
{
    public class GuestDto
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Title Title
        {
            get { Enum.TryParse(DisplayTitle, out Title _title); return _title; }
        }
        public string DisplayTitle { get; set; } = nameof(Title.Mr);
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
        public List<string> PhoneNumbers { get; set; }
        [JsonIgnore]
        public Country CountryValue
        {
            get
            {
                Enum.TryParse(CountryCode, out Country _countryValue);
                return _countryValue;
            }
        }
        public string CountryCode { get; set; } = nameof(Country.IN);
    }
}
