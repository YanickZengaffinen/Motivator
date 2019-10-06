using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Motivator.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Motivator.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Have to supply a name")]
        public string Name { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Have to supply an e-mail address")]
        public string Email { get; set; }

        [BindProperty, DataType(DataType.Password)]
        [Required(ErrorMessage = "Have to supply a password")]
        public string Password { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string RepeatPassword { get; set; }

        private readonly IAuthService authService;

        public RegisterModel(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {
            if(! await IsValid())
            {
                return Page();
            }

            var user = await authService.Add(Name, Email, Password);
            await authService.Login(HttpContext, user);

            return RedirectToPage("/Todos/Overview");
        }

        private async Task<bool> IsValid()
        {
            if (!ModelState.IsValid)
                return false;

            if (RepeatPassword?.Equals(Password, StringComparison.InvariantCulture) != true)
            {
                ModelState.AddModelError("noMatch", "Passwords do not match");
                return false;
            }

            if(!await authService.IsUniqueEmail(Email))
            {
                ModelState.AddModelError("emailAlreadyUsed", "EMail already in use");
                return false;
            }
                

            return true;
        }
    }
}