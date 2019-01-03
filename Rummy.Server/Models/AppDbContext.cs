using Microsoft.EntityFrameworkCore;
using Rummy.Shared.Models;

namespace Rummy.Server.Models
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
