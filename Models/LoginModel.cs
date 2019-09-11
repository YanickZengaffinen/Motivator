using System.ComponentModel.DataAnnotations;

namespace Motivator.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Have to supply an e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Have to supply a password")]
        public string Password { get; set; }
    }
}
