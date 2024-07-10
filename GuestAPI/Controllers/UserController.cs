using DataAccess.Core.Repositories.Contract;
using GuestSharedModel.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace GuestsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserDto user)
        {
            try
            {
                if (user is null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password) || string.IsNullOrWhiteSpace(user.Name))
                    return BadRequest("Required user information is missing.");

                var validateUserStatus = await _userService.CheckDuplicateUser(user);
                switch (validateUserStatus)
                {
                    case GuestSharedModel.Enums.UserStatus.DuplicateName:
                        return BadRequest("User name is taken.");
                    case GuestSharedModel.Enums.UserStatus.DuplicateEmail:
                        return BadRequest("User email is taken.");
                    case GuestSharedModel.Enums.UserStatus.Added:
                    case GuestSharedModel.Enums.UserStatus.ErrorInCreation:
                        return BadRequest("Failed to validate user contact admin");
                    case GuestSharedModel.Enums.UserStatus.New:
                        var result = await _userService.RegisterUser(user);
                        if (result)
                            return Ok("User registered successfully.");
                        else
                            return Ok("Failed to Register User.");
                    default:
                        throw new Exception();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController -> Post -> User Registration Failed. Exception:- {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
