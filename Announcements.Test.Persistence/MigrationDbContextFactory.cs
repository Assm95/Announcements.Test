using Announcements.Test.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Announcements.Test.Persistence
{
    internal class MigrationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false);

            IConfigurationRoot configuration = configurationBuilder.Build();
            string? connectionString = configuration.GetConnectionString("DbConnection");
            
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseNpgsql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
