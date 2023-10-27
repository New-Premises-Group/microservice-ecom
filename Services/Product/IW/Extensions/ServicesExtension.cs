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
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace IW.Extensions;

public static class ServicesExtension
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddGraphQLServer()
             .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ProductQuery>()
                .AddTypeExtension<CategoryQuery>()
            .AddErrorFilter<ErrorFilter>()
            .AddMutationType(m=>m.Name("Mutation"))
                .AddTypeExtension<ProductMutation>()
                .AddTypeExtension<CategoryMutation>()
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

        // Redis cache database
        builder.Services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connection = builder.Configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connection;
        });

        // DI for services
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        // register the mapper as Singleton service 
        var mapperConfig = new Mapper(typeAdapterConfig);
        builder.Services.AddSingleton<IMapper>(mapperConfig);

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<IProductService,CachedProductService>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

    }
}
