using Motivator.Models;
using System.Threading.Tasks;

namespace Motivator.Services
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(string email, string password);

        Task<UserModel> Add(string name, string email, string password);
    }
}
