using Motivator.DB.Repositories;
using Motivator.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Motivator.Services
{
    public class DBUserService : IUserService
    {
        private readonly IUserRepository userRepo;

        public DBUserService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public Task<UserModel> Add(string userName, string email, string password)
        {
            if (userRepo.TryGetUserByName(userName, out UserModel _))
            {
                throw new InvalidOperationException("Username already in use");
            }

            if (userRepo.TryGetUserByEmail(email, out UserModel _))
            {
                throw new InvalidOperationException("EMail already in use");
            }

            var user = new UserModel() { Username = userName, Email = email, Password = HashString(password) };
            userRepo.Add(user);

            return Task.FromResult(user);
        }

        public Task<UserModel> Authenticate(string userName, string password)
        {
            if (userRepo.TryGetUserByEmail(userName, out UserModel user))
            {
                if (user.Password.Equals(HashString(password)))
                {
                    return Task.FromResult(user);
                }
            }

            return Task.FromResult<UserModel>(null);
        }

        private string HashString(string str)
        {
            var message = Encoding.Unicode.GetBytes(str);
            using (var hash = new SHA256Managed())
            {
                var hashValue = hash.ComputeHash(message);
                return Encoding.Unicode.GetString(hashValue);
            }
        }
    }
}
