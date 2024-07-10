using GuestSharedModel.Enums;
using System.Text.Json.Serialization;

namespace GuestSharedModel.DtoModels
{
    public class GuestContactDto
    {
        public int Id { get; set; }
        public Guid GuestId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
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
