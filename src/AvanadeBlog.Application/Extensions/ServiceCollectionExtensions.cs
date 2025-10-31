using AvanadeBlog.Application.AutoMapper;
using AvanadeBlog.Application.Interfaces;
using AvanadeBlog.Application.Services;
using AvanadeBlog.Application.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace AvanadeBlog.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<PostRequestValidator>();
            services.AddScoped<CommentRequestValidator>();
            return services;
        }


        public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new BaseProfile());
                cfg.AddProfile(new PostProfile());
                cfg.AddProfile(new CommentProfile());
            });

            return services;
        }
    }
}
