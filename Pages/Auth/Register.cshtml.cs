using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Motivator.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if(!IsValid())
            {
                return Page();
            }

            var user = await authService.Add(Name, Email, Password);
            await authService.Login(HttpContext, user);

            return RedirectToPage("/Tasks/Overview");
        }

        private bool IsValid()
        {
            if (!ModelState.IsValid)
                return false;

            if (RepeatPassword?.Equals(Password, StringComparison.InvariantCulture) != true)
            {
                ModelState.AddModelError("noMatch", "Passwords do not match");
                return false;
            }
                

            return true;
        }
    }
}