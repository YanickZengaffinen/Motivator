using Microsoft.AspNetCore.Http;
using Motivator.DB.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Motivator.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Check if the given credentials are valid
        /// </summary>
        Task<User> Authenticate(string email, string password);

        /// <summary>
        /// Add some new credentials to the system
        /// </summary>
        Task<User> Add(string name, string email, string password);

        /// <summary>
        /// Login a user in the current context
        /// </summary>
        Task Login(HttpContext context, User user);

        /// <summary>
        /// Logout a user
        /// </summary>
        Task Logout(HttpContext context);

        /// <summary>
        /// Check if there is not already an account registered with a given email
        /// </summary>
        Task<bool> IsUniqueEmail(string email);

        /// <summary>
        /// Try to read the id from a user
        /// </summary>
        /// <returns>False if the user has no id or is not logged in</returns>
        bool TryGetUserId(ClaimsPrincipal claims, out int id);
    }
}
