using DataAccess.Core.DataModel;
using GuestSharedModel.DtoModels;

namespace DataAccess.Core.Mappers
{
    public static class UserMapper
    {
        public static ApplicationUser ToEntity(this UserDto userDto)
        {
            if (userDto is null)
                return new ApplicationUser();
            return new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Email = userDto.Email,
                CreatedDate = userDto.CreatedDate,
                IsActive = userDto.IsActive,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                UpdatedDate = userDto.UpdatedDate
            };
        }

        public static UserDto ToDto(this ApplicationUser User)
        {
            if (User is null)
                return new UserDto();
            return new UserDto
            {
                Id = User.Id,
                Name = User.Name,
                Email = User.Email,
                CreatedDate = User.CreatedDate,
                IsActive = User.IsActive,
                UpdatedDate = User.UpdatedDate
            };
        }
    }

}
