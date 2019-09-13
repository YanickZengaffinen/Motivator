using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Motivator.Services;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Motivator.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Have to supply an e-mail address")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Have to supply a password")]
        public string Password { get; set; }

        private readonly IAuthService authService;

        public LoginModel(IAuthService authService)
        {
            this.authService = authService;
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var user = await authService.Authenticate(Email, Password);
            if(user == null)
            {
                ModelState.AddModelError("Invalid Credentials", "Could not validate your credentials");
                return Page();
            }

            await authService.Login(HttpContext, user);

            return RedirectToPage("/Tasks/Overview");
        }
    }
}