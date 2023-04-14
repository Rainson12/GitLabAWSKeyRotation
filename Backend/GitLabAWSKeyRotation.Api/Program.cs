
using GitLabAWSKeyRotation.Api.Errors;
using GitLabAWSKeyRotation.Application;
using GitLabAWSKeyRotation.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace GitLabAWSKeyRotation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication();

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ProblemDetailsFactory, GitLabAWSKeyRotationProblemDetailsFactory>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
               .SetIsOriginAllowed(hostName => true));
            //app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}