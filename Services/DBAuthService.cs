using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Motivator.DB.Models;
using Motivator.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tasks = System.Threading.Tasks;

namespace Motivator.Services
{
    public class DBAuthService : IAuthService
    {
        private readonly IUserRepository userRepo;

        public DBAuthService(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public Tasks.Task<User> Add(string userName, string email, string password)
        {
            if (userRepo.TryGetUserByName(userName, out User _))
            {
                throw new InvalidOperationException("Username already in use");
            }

            if (userRepo.TryGetUserByEmail(email, out User _))
            {
                throw new InvalidOperationException("EMail already in use");
            }

            var user = new User() { Username = userName, Email = email, Password = HashString(password) };
            userRepo.Add(user);

            return Tasks.Task.FromResult(user);
        }

        public Tasks.Task<User> Authenticate(string userName, string password)
        {
            if (userRepo.TryGetUserByEmail(userName, out User user))
            {
                if (user.Password.Equals(HashString(password)))
                {
                    return Tasks.Task.FromResult(user);
                }
            }

            return Tasks.Task.FromResult<User>(null);
        }

        public bool TryGetUserId(ClaimsPrincipal claims, out int id)
        {
            if(claims?.Identity?.IsAuthenticated == true)
            {
                id = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
                return true;
            }

            id = -1;
            return false;
        }

        public async Tasks.Task Login(HttpContext context, User user)
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
