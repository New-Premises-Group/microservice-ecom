using IW.Authentication;
using IW.Configurations;
using IW.Exceptions;
using IW.Interfaces;
using IW.Interfaces.Repositories;
using IW.Interfaces.Services;
using IW.MessageBroker;
using IW.MessageBroker.Mutations;
using IW.MessageBroker.Queries;
using IW.Models;
using Mapster;
using IW.Models.DTOs.Item;
using IW.Repositories;
using IW.Repository;
using IW.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using IW.Notifications;
using System.Net.Mail;
using IW.Interfaces.Payloads;
using IW.Models.DTOs.ItemPayloads;
using IW.Models.DTOs.OrderPayloads;
using IW.Handlers.Orders;
using IW.Handlers.Items;
using IW.Handlers.Notifications;
using IW.Handlers.Discounts;

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
                .AddTypeExtension<DiscountQuery>()
            .AddErrorFilter<ErrorFilter>()
            .AddMutationType(m => m.Name("Mutation"))
                .AddTypeExtension<ItemMutation>()
                .AddTypeExtension<OrderMutation>()
                .AddTypeExtension<DiscountMutation>()
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
            //if (builder.Environment.IsDevelopment())
            //{
            //    options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnection"), builder =>
            //    {
            //        builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            //    });
            //    Console.WriteLine("Development Evironment");
            //}
            //if (builder.Environment.IsProduction())
            //{
            //    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            //    Console.WriteLine("Production Evironment");
            //}
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
        builder.Services.AddScoped<IItemService, ItemService>();
        builder.Services.AddScoped<IItemRepository, ItemRepository>();
        builder.Services.AddScoped<OrderService>();
        builder.Services.AddScoped<IOrderService, CachedOrderService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
        ////builder.Services.AddScoped<IRabbitMqConsumer<Order>,RabbitMqConsumer<Order>>();
        //builder.Services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer<Order>>();
        //builder.Services.AddSingleton<IConsumerService, ConsumerService<Order>>();
        //builder.Services.AddHostedService<ConsumerHostedService>();
        builder.Services.AddScoped<IRabbitMqProducer<OrderCreatedMessage>, RabbitMqProducer<OrderCreatedMessage>>();
        builder.Services.AddScoped<IRabbitMqProducer<ItemDto>, RabbitMqProducer<ItemDto>>();

        //Inject command handler services
        builder.Services.AddScoped<CreateOrderHandler>();
        builder.Services.AddScoped<CreateGuestOrderHandler>();
        builder.Services.AddScoped<UpdateOrderHandler>();
        builder.Services.AddScoped<DeleteOrderHandler>();
        builder.Services.AddScoped<FinishOrderHandler>();
        builder.Services.AddScoped<CreateItemsHandler>();
        builder.Services.AddScoped<UpdateItemHandler>();
        builder.Services.AddScoped<DeleteItemHandler>();
        builder.Services.AddScoped<AddNotificationHandler>();
        builder.Services.AddScoped<CreateDiscountHandler>();
        builder.Services.AddScoped<ApplyDiscountHandler>();
        builder.Services.AddScoped<UpdateDiscountHandler>();
        builder.Services.AddScoped<DeleteDiscountHandler>();

        builder.Services.AddScoped<IMediator>(serviceProvider => new Mediator(serviceProvider));
        builder.Services.AddScoped<IItemPayloadFactory, ItemPayloadFactory>();
        builder.Services.AddScoped<IOrderPayloadFactory, OrderPayloadFactory>();

        //Email Service
        builder.Services.AddScoped<IMailService,EmailService>();
        builder.Services
            .AddFluentEmail("npg-ecom@gmail.com")
            .AddRazorRenderer(Directory.GetCurrentDirectory())
            .AddSmtpSender(new SmtpClient() 
            { 
                Host = "smtp.mailgun.org", 
                Port = 587, 
                Credentials = new System.Net.NetworkCredential(
                    "postmaster@newpremisesgroup.tech",
                    "ecomnpg") 
            });
    }
}