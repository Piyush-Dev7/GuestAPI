using GuestSharedModel.DtoModels;
using GuestSharedModel.Enums;

namespace DataAccess.Core.Repositories.Contract
{
    public interface IUserService
    {
        Task<UserDto> GetUser(Guid userId);
        Task<bool> RegisterUser(UserDto userDto);
        Task<UserStatus> CheckDuplicateUser(UserDto user);
    }
}
