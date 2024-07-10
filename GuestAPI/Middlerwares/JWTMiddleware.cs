using DataAccess.Core.Auth.Contract;
using DataAccess.Core.Repositories.Contract;

namespace GuestsAPI.Middlerwares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IAuthService authService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = authService.ValidateToken(token);
            if (!userId.Equals(Guid.Empty))
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetUser(userId);
            }
            await _next(context);
        }
    }
}
