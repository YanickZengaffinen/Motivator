using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Motivator.DB.Repositories;
using Motivator.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Motivator.Services
{
    public class DBAuthService : IAuthService
    {
        private readonly IUserRepository userRepo;

        public DBAuthService(IUserRepository userRepo)
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

        public async Task Login(HttpContext context, UserModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await context.SignInAsync(principal);
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
