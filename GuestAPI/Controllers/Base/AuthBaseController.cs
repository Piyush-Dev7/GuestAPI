using Microsoft.AspNetCore.Mvc;

namespace GuestsAPI.Controllers.Base
{
    public class AuthBaseController : ControllerBase
    {
        public IActionResult IsAuthenticated()
        {
            return Ok();
        }
    }
}
