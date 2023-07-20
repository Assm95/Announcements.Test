using Announcements.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Announcements.Test.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Announcement> Announcements => Set<Announcement>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
