using Assignment.IntegrationEventBus.Events;
using Assignment.IntegrationEventBus.Infrastructure;
using Dapr;
using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
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
        .UseUoW<UserDbContext>(optionBuilder => optionBuilder.UseSqlite($"Data Source=user.db;"));
});

var app = builder.Build();

#region 迁移模型

var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<UserDbContext>();
dbContext.Database.Migrate();

#endregion

app.MapGet("/", () => "Hello IntegrationEventBus!");

app.MapGet("/register", (IIntegrationEventBus eventBus) =>
{
    //todo: 模拟注册用户并发布注册用户事件, 直接使用集成事件总线发布将不记录本地消息表
    eventBus.PublishAsync(new RegisterUserEvent()
    {
        Account = "Tom",
        Mobile = "19999999999"
    });
});

app.MapPost("/IntegrationEvent/RegisterUser",
    [Topic("pubsub", nameof(RegisterUserEvent))](RegisterUserEvent @event) => { Console.WriteLine($"注册用户成功: {@event.Account}"); });

app.UseRouting();

app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
});

app.Run();
