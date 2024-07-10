using DataAccess.Core.DataModel;
using GuestSharedModel.DtoModels;

namespace DataAccess.Core.Mappers
{
    public static class GuestContactMapper
    {
        public static GuestContact ToEntity(this string guestContactNo, GuestDto guest)
        {
            if (string.IsNullOrWhiteSpace(guestContactNo) || guest is null)
                return new GuestContact();
            return new GuestContact
            {
                CreatedDate = DateTime.UtcNow,
                GuestId = guest.Id,
                PhoneNumber = guestContactNo
            };
            
        }
    }
}
