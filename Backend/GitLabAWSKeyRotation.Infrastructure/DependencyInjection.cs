using Microsoft.Extensions.DependencyInjection;
using GitLabAWSKeyRotation.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Configuration;

namespace GitLabAWSKeyRotation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddPersistance(configuration);
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<GitLabAWSKeyRotationDbContext>(options => options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.2-mariadb")));

            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IGitlabAccessTokenRepository, GitlabAccessTokenRepository>();
            return services;
        }
    }
}
