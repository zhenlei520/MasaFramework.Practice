using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;

namespace Assignment17.Ordering.API.Application.IntegrationEvents;

public record OrderStatusChangedToSubmittedIntegrationEvent(int OrderId, string OrderStatus, string BuyerName) : IntegrationEvent
{
    public override string Topic { get; set; } = nameof(OrderStatusChangedToSubmittedIntegrationEvent);
}
