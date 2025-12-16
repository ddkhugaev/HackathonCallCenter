using Hackathon.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Call> Calls { get; set; }
        public DbSet<Agent> Agents { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
