using Microsoft.AspNetCore.Http;
using Motivator.Models;
using System.Threading.Tasks;

namespace Motivator.Services
{
    public interface IAuthService
    {
        Task<UserModel> Authenticate(string email, string password);

        Task<UserModel> Add(string name, string email, string password);

        Task Login(HttpContext context, UserModel user);
    }
}
