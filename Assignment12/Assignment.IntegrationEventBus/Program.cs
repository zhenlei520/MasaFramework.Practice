using Assignment.IntegrationEventBus.Events;
using Assignment.IntegrationEventBus.Infrastructure;
using Dapr;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
using Masa.Contrib.Data.EntityFrameworkCore.Sqlite;
using Masa.Contrib.Data.UoW.EF;
using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region 启动dapr sidecar

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprStarter(option =>
    {
        option.AppPort = 5061;
        option.DaprGrpcPort = 5032;
        option.DaprHttpPort = 5031;
    }, false);
}

#endregion

builder.Services.AddIntegrationEventBus<IntegrationEventLogService>(option =>
{
    option.UseDapr();
    option.UseUoW<UserDbContext>(optionBuilder => optionBuilder.UseSqlite($"Data Source=./Db/{Guid.NewGuid():N}.db;"));
    option.UseEventLog<UserDbContext>();
});

var app = builder.Build();

#region 迁移数据库

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UserDbContext>();
    context.Database.Migrate();
    context.Database.EnsureCreated();
}

#endregion

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
