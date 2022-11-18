using Assignment.IntegrationEventBus.Events;
using Assignment.IntegrationEventBus.Infrastructure;
using Dapr;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
using Masa.Contrib.Data.UoW.EFCore;
using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region 启动dapr sidecar

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprStarter();
}

#endregion

builder.Services.AddIntegrationEventBus(option =>
{
    option.UseDapr()
        .UseEventLog<UserDbContext>()
        .UseUoW<UserDbContext>(optionBuilder => optionBuilder.UseSqlite($"Data Source=./Db/{Guid.NewGuid():N}.db;"));
});

var app = builder.Build();

app.MapGet("/", () => "Hello IntegrationEventBus!");

app.MapGet("/register", async (IIntegrationEventBus eventBus) =>
{
    //todo: 模拟注册用户并发布注册用户事件
    await eventBus.PublishAsync(new RegisterUserEvent()
    {
        Account = "Tom",
        Mobile = "19999999999"
    });
});

app.MapPost("/IntegrationEvent/RegisterUser", [Topic("pubsub", nameof(RegisterUserEvent))](RegisterUserEvent @event) =>
{
    Console.WriteLine($"注册用户成功: {@event.Account}");
});

app.Run();
