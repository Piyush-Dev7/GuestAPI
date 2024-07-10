using DataAccess.Core.DataModel;
using DataAccess.Core.Helpers;
using DataAccess.Core.Mappers;
using DataAccess.Core.Repositories.Contract;
using GuestSharedModel.DtoModels;
using GuestSharedModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Core.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly ApplicationDbContext _context;
        public GuestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<GuestDto>> GetAllGuests(Guid userId)
        {
            return await _context.UserGuests.Where(t => userId.Equals(t.UserId) && t.User.IsActive).Include(t => t.Guest).ThenInclude(t => t.GuestContacts)
                .Select(t => t.Guest.ToDto(t.Guest.GuestContacts.Select(t1 => t1.PhoneNumber).ToList())).ToListAsync();
        }

        public async Task<GuestDto> GetGuest(Guid userId, Guid guestId)
        {
            return (await _context.UserGuests.Where(t => userId.Equals(t.UserId) && t.User.IsActive && guestId.Equals(t.GuestId)).Include(t => t.Guest).ThenInclude(t => t.GuestContacts).Select(t => t.Guest.ToDto(t.Guest.GuestContacts.Select(t => t.PhoneNumber).ToList())).FirstOrDefaultAsync()) ?? new GuestDto();
        }

        public async Task<GuestStatus> Create(GuestDto guest, Guid userId)
        {
            try
            {
                if (string.IsNullOrEmpty(guest.FirstName) && string.IsNullOrEmpty(guest.LastName))
                    return GuestStatus.NameRequired;

                if (guest.PhoneNumbers == null || !guest.PhoneNumbers.Any())
                    return GuestStatus.PhoneRequired;

                if (!guest.PhoneNumbers.Any(t => t.ValidatePhoneNumber(guest.CountryValue)))
                    return GuestStatus.PhoneInvalid;

                var isGuestExists = await _context.Guests.AnyAsync(t => t.Id.Equals(guest.Id) || t.Email.Equals(guest.Email));
                if (isGuestExists)
                    return GuestStatus.Exists;

                var isContactTaken = await CheckDuplicateContact(guest.PhoneNumbers);
                if (isContactTaken)
                    return GuestStatus.DuplicatePhone;

                guest.Id = Guid.NewGuid();
                var _contacts = CreateContactEntity(guest.PhoneNumbers, guest);
                return await Save(guest.ToEntity(), userId, _contacts);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GuestContact> CreateContactEntity(List<string> guestContactsList, GuestDto guest)
        {
            var contacts = new List<GuestContact>();
            var _currentTime = DateTime.UtcNow;
            foreach (var contact in guestContactsList)
            {
                contacts.Add(new GuestContact
                {
                    GuestId = guest.Id,
                    PhoneNumber = contact,
                    CreatedDate = _currentTime
                });
            }
            return contacts;
        }

        public async Task<bool> CheckDuplicateContact(List<string> guestContactsList)
        {
            //check dupcliate
            return await _context.GuestContacts.AnyAsync(t => guestContactsList != null && guestContactsList.Contains(t.PhoneNumber));
        }

        public async Task<GuestStatus> Update(GuestDto guest)
        {
            return await UpdateContacts(guest.PhoneNumbers.FirstOrDefault()?.ToEntity(guest)); 
        }
        private async Task<GuestStatus> Save(Guest guest, Guid userId, List<GuestContact> contact)
        {
            var _currentDate = DateTime.UtcNow;
            await _context.Guests.AddAsync(guest);

            var userGuests = new UserGuestsMapping
            {
                UserId = userId,
                GuestId = guest.Id,
                CreatedDate = _currentDate
            };
            await _context.UserGuests.AddAsync(userGuests);
            await _context.GuestContacts.AddRangeAsync(contact);
            return (await _context.SaveChangesAsync() > 0) ? GuestStatus.Created : GuestStatus.ErrorInCreation;
        }

        private async Task<GuestStatus> UpdateContacts(GuestContact guestContact)
        {
            if(guestContact is null)
                return GuestStatus.PhoneAddError;
            await _context.GuestContacts.AddAsync(guestContact);
            return (await _context.SaveChangesAsync() > 0) ? GuestStatus.PhoneAdded : GuestStatus.PhoneAddError;
        }
    }
}
