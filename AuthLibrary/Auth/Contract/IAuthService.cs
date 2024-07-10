using GuestSharedModel.DtoModels;

namespace DataAccess.Core.Auth.Contract
{
    public interface IAuthService
    {
        string GenerateToken(UserDto user);
        Task<Guid> ValidateCredentials(UserDto user);
        Guid ValidateToken(string token);
    }
}
