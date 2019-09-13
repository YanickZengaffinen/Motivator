using Microsoft.EntityFrameworkCore;
using Motivator.DB.Models;

namespace Motivator.DB
{
    public class MotivatorContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public MotivatorContext(DbContextOptions<MotivatorContext> options) : base(options) { }
    }
}
