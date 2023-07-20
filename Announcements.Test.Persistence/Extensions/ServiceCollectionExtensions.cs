using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Announcements.Test.Application.Interfaces.Repositories;
using Announcements.Test.Persistence.Contexts;
using Announcements.Test.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Announcements.Test.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");

            services.AddDbContext<ApplicationDbContext>(opts => opts.UseNpgsql(connectionString));
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddTransient<IAnnouncementsRepository, AnnouncementsRepository>()
                .AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}
