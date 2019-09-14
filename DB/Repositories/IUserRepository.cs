using Motivator.DB.Models;
using System.Threading.Tasks;

namespace Motivator.DB.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);

        Task Update(User user);

        Task<User> GetUserByName(string name);

        Task<User> GetUserByEmail(string email);
    }
}
