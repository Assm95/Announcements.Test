using Announcements.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Announcements.EF
{
    public class AnnouncementDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Announcement> Announcements => Set<Announcement>();
        public AnnouncementDbContext(DbContextOptions contextOptions) : base(contextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
