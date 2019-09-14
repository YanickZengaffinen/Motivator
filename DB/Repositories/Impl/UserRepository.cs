using Microsoft.EntityFrameworkCore;
using Motivator.DB.Models;
using System;
using System.Threading.Tasks;

namespace Motivator.DB.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        public MotivatorContext Context { get; set; }

        public UserRepository(MotivatorContext context)
        {
            this.Context = context;
        }

        public async Task Add(User user)
        {
            await Context.Users.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
        }

        public async Task<User> GetUserByName(string userName)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Username.Equals(userName, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<User> GetUserByEmail(string eMail)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Email.Equals(eMail, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
