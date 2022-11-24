using Assignment.CqrsDemo.Application.Goods.Commands;
using Assignment.CqrsDemo.IntegrationEvents;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
using Masa.Contrib.Dispatcher.Events;

namespace Assignment.CqrsDemo.Application.Goods;

public class CommandHandler
{
    /// <summary>
    /// 将商品添加到Db，并发送跨进程事件，用于处理读数据
    /// </summary>
    /// <param name="command"></param>
    /// <param name="integrationEventBus"></param>
    [EventHandler]
    public async Task AddGoods(AddGoodsCommand command, IIntegrationEventBus integrationEventBus)
    {
        //todo: 模拟添加商品到db并发送添加商品集成事件

        var goodsId = Guid.NewGuid(); //模拟添加到db后并获取商品id
        await integrationEventBus.PublishAsync(new AddGoodsIntegrationEvent(goodsId, command.Name, command.Cover, command.Price,
            command.Count));
    }
}
