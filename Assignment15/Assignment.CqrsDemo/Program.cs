using Assignment.CqrsDemo.IntegrationEvents;
using Masa.Contrib.Dispatcher.Events;
using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddIntegrationEventBus(dispatcherOptions =>
{
    //不使用本地消息表
    dispatcherOptions.UseDapr();
    dispatcherOptions.UseEventBus();
});

builder.Services.AddEventBus();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/integration/goods/add", (AddGoodsIntegrationEvent @event, ILogger<Program> logger) =>
{
    //todo: 模拟添加商品到缓存
    logger.LogInformation("添加商品到缓存");
}).WithTopic("pubsub", nameof(AddGoodsIntegrationEvent));

app.Run();
