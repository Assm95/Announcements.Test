using Announcements.Test.Application.Interfaces;
using Announcements.Test.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Announcements.Test.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddServices();
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IMediator, Mediator>()
                .AddTransient<IFileStorage, LocalFileStorage>();
        }
    }
}
