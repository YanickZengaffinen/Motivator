using Motivator.Models;
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

        public void Add(UserModel user)
        {
            Context.Users.Add(user);
            Context.SaveChanges();
        }

        public void Update(UserModel user)
        {
            Context.Users.Update(user);
            Context.SaveChanges();
        }

        public bool TryGetUserByName(string userName, out UserModel user)
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

        public bool TryGetUserByEmail(string eMail, out UserModel user)
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
