using Announcements.Test.Application.Interfaces;
using Announcements.Test.Infrastructure.Options;
using Announcements.Test.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Announcements.Test.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);
        }

        private static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMediator, Mediator>()
                .AddTransient<IFileStorage, LocalFileStorage>();
        }
    }
}
