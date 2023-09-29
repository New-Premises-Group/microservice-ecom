using Microsoft.AspNetCore.Authentication.JwtBearer;
using IW.Models;
using Microsoft.EntityFrameworkCore;
using IW.Repository;
using IW.Repositories;
using IW.Interfaces;
using IW.Services;
using IW.Authentication;
using IW.Interfaces.Repositories;
using IW.Interfaces.Services;
using IW.Exceptions;
using IW.Controllers.Queries;
using IW.Controllers.Mutations;

namespace IW.Extensions;

public static class ServicesExtension
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddGraphQLServer()
             .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<UserQuery>()
                .AddTypeExtension<RoleQuery>()
            .AddErrorFilter<ErrorFilter>()
            .AddMutationType(m=>m.Name("Mutation"))
                .AddTypeExtension<UserMutation>()
                .AddTypeExtension<RoleMutation>()
            .AddMutationConventions(applyToAllMutations: true)
            .AddAuthorization();
        //Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        builder.Services.ConfigureOptions<JwtOptionSetup>();
        builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
        builder.Services.AddAuthorization();

        // Application database context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // DI for UserService
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserService,UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();

        builder.Services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
