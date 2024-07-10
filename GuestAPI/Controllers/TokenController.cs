using DataAccess.Core.Auth.Contract;
using GuestSharedModel.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace GuestsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<TokenController> _logger;
        public TokenController(IAuthService authService, ILogger<TokenController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserDto request)
        {
            try
            {
                var userId = await _authService.ValidateCredentials(request);
                if (userId.Equals(Guid.Empty))
                    return BadRequest("Invalid user credentials.");
                request.Id = userId;
                return Ok(_authService.GenerateToken(request));
            }
            catch (Exception ex)
            {
                _logger.LogError($"TokenController -> POST -> Token genration Failed. Exception:- {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    public class LoginRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
    }
}
