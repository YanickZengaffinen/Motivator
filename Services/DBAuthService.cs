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

        public async Task<User> Add(string userName, string email, string password)
        {
            if(await userRepo.GetUserByName(userName) != null)
            {
                throw new InvalidOperationException("Username already in use");
            }

            if (await userRepo.GetUserByEmail(email) != null)
            {
                throw new InvalidOperationException("EMail already in use");
            }

            var user = new User() { Username = userName, Email = email, Password = HashString(password) };
            await userRepo.Add(user);

            return user;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await userRepo.GetUserByEmail(email);
            if (user != null)
            {
                if (user.Password.Equals(HashString(password)))
                {
                    return user;
                }
            }

            return null;
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

        public async Task Login(HttpContext context, User user)
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
