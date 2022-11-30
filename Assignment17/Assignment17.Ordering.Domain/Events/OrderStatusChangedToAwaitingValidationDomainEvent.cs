using Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;
using Masa.BuildingBlocks.Ddd.Domain.Events;

namespace Assignment17.Ordering.Domain.Events;

/// <summary>
/// Event used when the grace period order is confirmed
/// </summary>
public record OrderStatusChangedToAwaitingValidationDomainEvent(int OrderId,
    IEnumerable<OrderItem> OrderItems) : DomainEvent;
