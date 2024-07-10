using DataAccess.Core.DataModel;
using GuestSharedModel.DtoModels;

namespace DataAccess.Core.Mappers
{
    public static class GuestMapper
    {
        public static GuestDto ToDto(this Guest guest, List<string> phoneNumbers)
        {
            if (guest is null)
                return new GuestDto();
            return new GuestDto
            {
                Id = guest.Id,
                BirthDate = guest.BirthDate,
                Email = guest.Email,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                PhoneNumbers = phoneNumbers ?? new List<string>(),
                //PhoneNumbers = guest.PhoneNumbers?.Split(',')?.ToList() ?? new List<string>(),
                DisplayTitle = guest.Title.ToString()
            };
        }

        public static Guest ToEntity(this GuestDto guestDto)
        {
            if(guestDto is null)
                return new Guest();
            return new Guest()
            {
                Id = guestDto.Id,
                Title = guestDto.Title,
                BirthDate = guestDto.BirthDate,
                Email = guestDto.Email,
                FirstName = guestDto.FirstName,
               // PhoneNumbers = String.Join(',', guestDto.PhoneNumbers ?? new List<string>()),
                LastName = guestDto.LastName
            };
        }
    }
}
