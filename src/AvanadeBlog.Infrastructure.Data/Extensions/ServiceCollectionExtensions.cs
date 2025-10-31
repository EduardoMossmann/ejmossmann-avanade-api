using AvanadeBlog.Domain.Interfaces;
using AvanadeBlog.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AvanadeBlog.Infrastructure.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
