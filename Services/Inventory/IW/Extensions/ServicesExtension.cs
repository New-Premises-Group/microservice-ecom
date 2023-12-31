﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using IW.MessageBroker;
using IW.Configurations;
using IW.MessageBroker.MessageType;

namespace IW.Extensions;

public static class ServicesExtension
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors();
        builder.Services.AddGraphQLServer()
             .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<InventoryQuery>()
                .AddTypeExtension<TransactionQuery>()
            .AddErrorFilter<ErrorFilter>()
            .AddMutationType(m=>m.Name("Mutation"))
                .AddTypeExtension<InventoryMutation>()
                .AddTypeExtension<TransactionMutation>()
            .AddMutationConventions(applyToAllMutations: true)
            .AddAuthorization();

        //Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        builder.Services.ConfigureOptions<JwtOptionSetup>();
        builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
        //Config options for services
        builder.Services.ConfigureOptions<RabbitMqOptionSetup>();
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
        builder.Services.AddScoped<IInventoryService,InventoryService>();
        builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        builder.Services.AddScoped<IRabbitMqProducer, RabbitMqProducer>();
        //builder.Services.AddScoped<IRabbitMqConsumer<Order>,RabbitMqConsumer<Order>>()
        builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();
        builder.Services.AddSingleton<IConsumerService, ConsumerService>();
        builder.Services.AddHostedService<ConsumerHostedService>();
    }
}
