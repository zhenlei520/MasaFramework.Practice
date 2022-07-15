using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;

namespace Assignment.CqrsDemo.IntegrationEvents;

public record AddGoodsIntegrationEvent(Guid Id, string Name, string Cover, decimal Price, int Count) : IntegrationEvent
{
    public Guid Id { get; set; } = Id;

    public string Name { get; set; } = Name;

    public string Cover { get; set; } = Cover;

    public decimal Price { get; set; } = Price;

    public int Count { get; set; } = Count;

    public override string Topic { get; set; } = nameof(AddGoodsIntegrationEvent);
}
