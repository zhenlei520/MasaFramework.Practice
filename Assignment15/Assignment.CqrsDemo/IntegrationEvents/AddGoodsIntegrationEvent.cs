using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;

namespace Assignment.CqrsDemo.IntegrationEvents;

/// <summary>
/// 跨进程事件，发送添加商品事件，用于处理读数据的问题
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Cover"></param>
/// <param name="Price"></param>
/// <param name="Count"></param>
public record AddGoodsIntegrationEvent(Guid Id, string Name, string Cover, decimal Price, int Count) : IntegrationEvent
{
    public Guid Id { get; set; } = Id;

    public string Name { get; set; } = Name;

    public string Cover { get; set; } = Cover;

    public decimal Price { get; set; } = Price;

    public int Count { get; set; } = Count;

    public override string Topic { get; set; } = nameof(AddGoodsIntegrationEvent);
}
