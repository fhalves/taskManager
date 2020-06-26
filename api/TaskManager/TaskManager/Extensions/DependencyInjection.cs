using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Services;
using TaskManager.Domain.Interfaces;
using TaskManager.Domain.Interfaces.Repositories;
using TaskManager.Domain.Interfaces.Services;
using TaskManager.Domain.Models;
using TaskManager.Domain.Models.Request;
using TaskManager.Infra.Common;
using TaskManager.Infra.Factory;
using TaskManager.Infra.Repositories;
using TaskManager.Infra.Validations;

namespace TaskManager.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureIoC(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<ConnectionString>(configuration.GetSection("ConnectionStrings"));
            services.Configure<JwtSecret>(configuration.GetSection("JwtConfigurations"));

            services.AddTransient<IDbFactory, DbFactory>();
            services.AddTransient<IAppSettingsConfigurations, AppSettingsConfigurations>();

            AddRepositories(services);
            AddServices(services);
            AddFluentValidations(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }

        private static void AddFluentValidations(IServiceCollection services)
        {
            services.AddSingleton<IValidator<AuthRequest>, AuthValidation>();
            services.AddSingleton<IValidator<User>, UserValidation>();
            services.AddSingleton<IValidator<Tasks>, TaskValidation>();
        }
    }

}
