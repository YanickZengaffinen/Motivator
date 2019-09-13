using Microsoft.AspNetCore.Http;
using Motivator.Models;
using System.Threading.Tasks;

namespace Motivator.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Check if the given credentials are valid
        /// </summary>
        Task<UserModel> Authenticate(string email, string password);

        /// <summary>
        /// Add some new credentials to the system
        /// </summary>
        Task<UserModel> Add(string name, string email, string password);

        /// <summary>
        /// Login a user in the current context
        /// </summary>
        Task Login(HttpContext context, UserModel user);
    }
}
