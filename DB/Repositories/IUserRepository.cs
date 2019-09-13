using Motivator.DB.Models;

namespace Motivator.DB.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);

        void Update(User user);

        bool TryGetUserByName(string name, out User user);

        bool TryGetUserByEmail(string email, out User user);
    }
}
