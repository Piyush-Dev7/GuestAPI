using GuestSharedModel.DtoModels;
using GuestSharedModel.Enums;

namespace DataAccess.Core.Repositories.Contract
{
    public interface IGuestRepository
    {
        Task<List<GuestDto>> GetAllGuests(Guid userId);
        Task<GuestDto> GetGuest(Guid userId, Guid guestId);
        Task<GuestStatus> Create(GuestDto guest, Guid userId);
        Task<GuestStatus> Update(GuestDto guest);
        Task<bool> CheckDuplicateContact(List<string> guestContactsList);
    }
}
