using Assignment.CqrsDemo.Application.Goods.Queries;
using Assignment.CqrsDemo.Dto;
using Masa.Contrib.Dispatcher.Events;

namespace Assignment.CqrsDemo.Application.Goods;

public class QueryHandler
{
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
