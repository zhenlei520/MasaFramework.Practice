using Assignment.CqrsDemo.Application.Goods.Commands;
using Assignment.CqrsDemo.Application.Goods.Queries;
using Assignment.CqrsDemo.Dto;
using Assignment.CqrsDemo.IntegrationEvents;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
using Masa.Contrib.Dispatcher.Events;

namespace Assignment.CqrsDemo.Application.Goods;

public class GoodsHandler
{
    private readonly IIntegrationEventBus _integrationEventBus;

    public GoodsHandler(IIntegrationEventBus integrationEventBus)
    {
        _integrationEventBus = integrationEventBus;
    }

    /// <summary>
    /// 将商品添加到Db，并发送跨进程事件，用于处理读数据
    /// </summary>
    /// <param name="command"></param>
    [EventHandler]
    public async Task AddGoods(AddGoodsCommand command)
    {
        //todo: 模拟添加商品到db并发送添加商品集成事件

        var goodsId = Guid.NewGuid(); //模拟添加到db后并获取商品id
        await _integrationEventBus.PublishAsync(new AddGoodsIntegrationEvent(goodsId, command.Name, command.Cover, command.Price,
            command.Count));
    }

    /// <summary>
    /// 从缓存查询商品信息
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [EventHandler]
    public Task GetGoods(GoodsItemQuery query)
    {
        //todo: 模拟从cache获取商品
        var goods = new GoodsItemDto();

        query.Result = goods;
        return Task.CompletedTask;
    }
}
