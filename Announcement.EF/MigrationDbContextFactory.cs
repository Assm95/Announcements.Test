using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Announcements.EF
{
    internal class MigrationDbContextFactory : IDesignTimeDbContextFactory<AnnouncementDbContext>
    {
        public AnnouncementDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false);

            IConfigurationRoot configuration = configurationBuilder.Build();
            string? connectionString = configuration.GetConnectionString("DbConnection");
            
            DbContextOptionsBuilder optionsBuilder =
                new DbContextOptionsBuilder<AnnouncementDbContext>()
                    .UseNpgsql(connectionString);

            return new AnnouncementDbContext(optionsBuilder.Options);
        }
    }
}
