using Motivator.Models;

namespace Motivator.DB.Repositories
{
    public interface IUserRepository
    {
        void Add(UserModel user);

        void Update(UserModel user);

        bool TryGetUserByName(string name, out UserModel user);

        bool TryGetUserByEmail(string email, out UserModel user);
    }
}
