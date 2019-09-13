using Motivator.DB.Models;
using System;
using System.Linq;

namespace Motivator.DB.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        public MotivatorContext Context { get; set; }

        public UserRepository(MotivatorContext context)
        {
            this.Context = context;
        }

        public void Add(User user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void Update(User user)
        {
            Context.Users.Update(user);
            Context.SaveChanges();
        }

        public bool TryGetUserByName(string userName, out User user)
        {
            var dbUser = Context.Users.FirstOrDefault(u => u.Username.Equals(userName, StringComparison.InvariantCultureIgnoreCase));

            if(dbUser != null)
            {
                user = dbUser;
                return true;
            }

            user = null;
            return false;
        }

        public bool TryGetUserByEmail(string eMail, out User user)
        {
            var dbUser = Context.Users.FirstOrDefault(u => u.Email.Equals(eMail, StringComparison.InvariantCultureIgnoreCase));

            if (dbUser != null)
            {
                user = dbUser;
                return true;
            }

            user = null;
            return false;
        }
    }
}
