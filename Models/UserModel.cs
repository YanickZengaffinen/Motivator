using System.ComponentModel.DataAnnotations;

namespace Motivator.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
