
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IW.Controllers;
using IW.Models;
using Microsoft.EntityFrameworkCore;
using IW.Repository;
using IW.Repositories;
using IW.Interfaces;

namespace IW.Extensions;

public static class ServicesRegistor
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>();
        //Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = Environment.GetEnvironmentVariable("ASPNETCORE_ISSUER"),
                ValidAudience = Environment.GetEnvironmentVariable("ASPNETCORE_AUDIENCE"),
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ASPNETCORE_JWTKEY") ?? 
                throw new Exception("SECRET_TOKEN environment variable is not set."))),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
            };
        });
        builder.Services.AddAuthorization();

        // Application database context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // DI for UserService
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserService,IUserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}
