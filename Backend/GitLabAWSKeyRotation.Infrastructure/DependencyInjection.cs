using Microsoft.Extensions.DependencyInjection;
using GitLabAWSKeyRotation.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Infrastructure.Persistance.Repositories;

namespace GitLabAWSKeyRotation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddPersistance();
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            // Todo move to config / pull credentials from environment variables
            services.AddDbContext<GitLabAWSKeyRotationDbContext>(options => options.UseMySql("server=localhost;database=GitLabAWSKeyRotation;user=root;password=RandomPass0815;treattinyasboolean=true", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.2-mariadb")));

            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<ICodeRepositoryRepository, CodeRepositoryRepository>();
            return services;
        }
    }
}
