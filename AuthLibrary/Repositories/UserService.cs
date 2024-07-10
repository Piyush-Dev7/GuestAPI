using DataAccess.Core.DataModel;
using DataAccess.Core.Mappers;
using DataAccess.Core.Repositories.Contract;
using GuestSharedModel.DtoModels;
using GuestSharedModel.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Core.Repositories
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> RegisterUser(UserDto userDto)
        {
            var user = userDto.ToEntity();
            return await Save(user);
        }

        private async Task<bool> Save(ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(t => t.Id.Equals(userId) && t.IsActive) ?? new ApplicationUser();
            return user.ToDto();
        }

        public async Task<UserStatus> CheckDuplicateUser(UserDto user)
        {
            var _user = await _context.Users.FirstOrDefaultAsync(t => t.Name.ToLower().Equals(user.Name.ToLower()) || t.Email.ToLower().Equals(user.Email.ToLower()));
            if (_user is null)
                return UserStatus.New;
            if (user.Name.Equals(_user.Name, StringComparison.OrdinalIgnoreCase))
                return UserStatus.DuplicateName;
            if(user.Email.Equals(_user.Email, StringComparison.OrdinalIgnoreCase))
                return UserStatus.DuplicateEmail;
            return UserStatus.ErrorInCreation;
        }
    }
}
