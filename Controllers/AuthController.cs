using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Motivator.Models;
using Motivator.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Motivator.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }

        [Route("login")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userService.Authenticate(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("InvalidCredentials", "Could not validate your credentials");
                return View(model);
            }

            await SignInUser(user);

            return RedirectToAction("Overview", "Task");
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [Route("register")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userService.Add(model.Name, model.Email, model.Password);
            await SignInUser(user);

            return RedirectToAction("Overview", "Task");
        }


        [Route("logout")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Route("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        private async Task SignInUser(UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
        }
    }
}