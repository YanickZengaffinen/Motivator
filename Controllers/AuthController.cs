using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Motivator.Services;

namespace Motivator.Pages.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await authService.Logout(ControllerContext.HttpContext);
            return RedirectToPage("/Auth/Login");
        }
    }
}
