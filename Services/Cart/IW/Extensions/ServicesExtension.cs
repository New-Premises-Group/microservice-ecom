using IW.Authentication;
using IW.Controllers.Mutations;
using IW.Controllers.Queries;
using IW.Exceptions;
using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Interfaces.Services;
using IW.Models;
using IW.Repositories;
using IW.Repository;
using IW.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace IW.Extensions;

public static class ServicesExtension
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddGraphQLServer()
             .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<CartQuery>()
                .AddTypeExtension<CartItemQuery>()
            .AddErrorFilter<ErrorFilter>()
            .AddMutationType(m => m.Name("Mutation"))
                .AddTypeExtension<CartMutation>()
                .AddTypeExtension<CartItemMutation>()
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

        // DI for services
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<ICartItemService, CartItemService>();
        builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
    }
}