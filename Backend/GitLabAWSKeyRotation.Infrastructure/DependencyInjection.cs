using Microsoft.Extensions.DependencyInjection;
using GitLabAWSKeyRotation.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Persistance;
using GitLabAWSKeyRotation.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using GitLabAWSKeyRotation.Infrastructure.Authentication;
using GitLabAWSKeyRotation.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace GitLabAWSKeyRotation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddPersistance(configuration);
            services.AddAuth(configuration);
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<GitLabAWSKeyRotationDbContext>(options => options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.2-mariadb")));
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IGitlabAccessTokenRepository, GitlabAccessTokenRepository>();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                };
                /* example token:
                 * eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYW5hbHl0aWNzLWNsaW5pY2FsLWV4cG9ydCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL3VwbiI6IlNhbXBsZUludmVudG9yeSIsIm5iZiI6MTU5MDQ5MjQ1NiwiZXhwIjoyMTQ3MDAwMDAwLCJyb2xlIjpbIlNhbXBsZUludmVudG9yeVF1ZXJ5Il0sImlzcyI6Imh0dHBzOi8vYXBpLmluZGl2dW1lZC5jb20iLCJhdWQiOiJBbnkifQ.KP5DCoyNxaU7X8_I13qjuHtT99_dqlKxl-YAymhsZHo
                 * 
                 */
            });
            return services;
        }
        
    }
}
