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
using IW.MessageBroker.Queries;
using IW.MessageBroker.Mutations;
using IW.Configurations;
using IW.MessageBroker;
using Mapster;
using IW.Models.DTOs.OrderDto;
using IW.Models.DTOs.Item;
using MapsterMapper;
using System.Reflection;
using IW.Models.Entities;

namespace IW.Extensions;

public static class ServicesExtension
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddGraphQLServer()
             .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<ItemQuery>()
                .AddTypeExtension<OrderQuery>()
            .AddErrorFilter<ErrorFilter>()
            .AddMutationType(m=>m.Name("Mutation"))
                .AddTypeExtension<ItemMutation>()
                .AddTypeExtension<OrderMutation>()
            .AddMutationConventions(applyToAllMutations: true)
            .AddAuthorization();
        //Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        //Config options for services
        builder.Services.ConfigureOptions<JwtOptionSetup>();
        builder.Services.ConfigureOptions<RabbitMqOptionSetup>();
        builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
        builder.Services.AddAuthorization();

        // Application database context
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // DI for services
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        // scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        // register the mapper as Singleton service 
        var mapperConfig = new Mapper(typeAdapterConfig);
        builder.Services.AddSingleton<IMapper>(mapperConfig);
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IItemService,ItemService>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        //builder.Services.AddScoped<IRabbitMqConsumer<Order>,RabbitMqConsumer<Order>>();
        builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer<Order>>();
        builder.Services.AddSingleton<IConsumerService, ConsumerService<Order>>();
        builder.Services.AddHostedService<ConsumerHostedService>();
        builder.Services.AddScoped<IRabbitMqProducer<OrderCreatedMessage>,RabbitMqProducer<OrderCreatedMessage>>();
        builder.Services.AddScoped<IRabbitMqProducer<ItemDto>,RabbitMqProducer<ItemDto>>();

    }
}
