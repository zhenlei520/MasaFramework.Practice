using Assignment.CqrsDemo.Application.Goods.Commands;
using Assignment.CqrsDemo.Application.Goods.Queries;
using Assignment.CqrsDemo.IntegrationEvents;
using Dapr;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.Contrib.Dispatcher.Events;
using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
    builder.Services.AddDaprStarter();

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

app.MapPost(
    "/integration/goods/add",
    [Topic("pubsub", nameof(AddGoodsIntegrationEvent))]
    (AddGoodsIntegrationEvent @event, ILogger<Program> logger) =>
    {
        //todo: 模拟添加商品到缓存
        logger.LogInformation("添加商品到缓存, {Event}", @event);
    });

app.UseRouting();
app.UseCloudEvents();
app.UseEndpoints(endpoint =>
{
    endpoint.MapSubscribeHandler();
});

app.Run();
