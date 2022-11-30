using Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;
using Masa.BuildingBlocks.Ddd.Domain.Events;

namespace Assignment17.Ordering.Domain.Events;

/// <summary>
/// Event used when the order is paid
/// </summary>
public record OrderStatusChangedToPaidDomainEvent(int OrderId,
    IEnumerable<OrderItem> OrderItems) : DomainEvent;
