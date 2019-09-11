using Microsoft.EntityFrameworkCore;
using Motivator.Models;

namespace Motivator.DB
{
    public class MotivatorContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public MotivatorContext(DbContextOptions<MotivatorContext> options) : base(options) { }
    }
}
