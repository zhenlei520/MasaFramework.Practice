using Assignment.CqrsDemo.Application.Goods.Commands;
using Assignment.CqrsDemo.Application.Goods.Queries;
using Assignment.CqrsDemo.IntegrationEvents;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddIntegrationEventBus(dispatcherOptions =>
{
    //不使用本地消息表
    dispatcherOptions.UseDapr();
    dispatcherOptions.UseEventBus();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/goods/add", async (AddGoodsCommand command, IEventBus eventBus) =>
{
    await eventBus.PublishAsync(command);
});

app.MapGet("/goods/{id}", async (Guid id, IEventBus eventBus) =>
{
    var query = new GoodsItemQuery(id);
    await eventBus.PublishAsync(query);
    return query.Result;
});

app.MapPost("/integration/goods/add", (AddGoodsIntegrationEvent @event, ILogger<Program> logger) =>
{
    //todo: 模拟添加商品到缓存
    logger.LogInformation("添加商品到缓存");
}).WithTopic("pubsub", nameof(AddGoodsIntegrationEvent));

app.Run();
