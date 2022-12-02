using System.Reflection;
using Assignment17.Ordering.API.Application.Commands;
using Assignment17.Ordering.API.Application.IntegrationEvents;
using Assignment17.Ordering.API.Infrastructure;
using Assignment17.Ordering.Domain.Exceptions;
using Assignment17.Ordering.Infrastructure;
using Dapr;
using FluentValidation;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Data.UoW.EFCore;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Masa.Contrib.Dispatcher.Events;
using Masa.Contrib.Dispatcher.IntegrationEvents;
using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddMasaDbContext<OrderingContext>(options => options.UseSqlServer());

#region 注册Swagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
assemblies.Add(typeof(OrderingContext).Assembly);
assemblies.Add(typeof(OrderingDomainException).Assembly);

builder.Services
    .AddValidatorsFromAssembly(Assembly.GetEntryAssembly())
    .AddDomainEventBus(assemblies.Distinct().ToArray(), options =>
    {
        options
            .UseIntegrationEventBus(dispatcherOptions => dispatcherOptions.UseDapr().UseEventLog<OrderingContext>())
            .UseEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
            .UseUoW<OrderingContext>(dbContextBuilder => dbContextBuilder.UseSqlServer())
            .UseRepository<OrderingContext>();
    });

#region 使用DaprClient

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprStarter(options =>
    {
        options.DaprGrpcPort = 3000;
        options.DaprGrpcPort = 3001;
    });
}

#endregion

var app = builder.Build();

//使用全局异常
app.UseMasaExceptionHandler();

#region 启用Swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion

#region 种子数据

await app.MigrateDbContext<OrderingContext>(async (context, serviceProvider) =>
{
    await new OrderingContextSeed().SeedAsync(context);
});

#endregion

#region Dapr订阅集成事件

app.UseRouting();

app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});

#endregion

app.MapGet("/", () => "Hello Assignment17");

app.MapGet("/test", () =>
{
    throw new UserFriendlyException("test");
});

app.MapPost("/api/v1/orders/create", async (
    IEventBus eventBus,
    CreateOrderCommand command,
    CancellationToken cancellationToken) =>
{
    await eventBus.PublishAsync(command, cancellationToken);
    return Results.Ok();
});

#region 订阅集成事件

app.MapPost("/integrationEvent/OrderStatusChangedToSubmitted",
    [Topic("pubsub", nameof(OrderStatusChangedToSubmittedIntegrationEvent))]
    (ILogger<Program> logger, OrderStatusChangedToSubmittedIntegrationEvent @event) =>
    {
        logger.LogInformation("接收到订单提交事件, {Order}", @event);
    });

#endregion

app.Run();
