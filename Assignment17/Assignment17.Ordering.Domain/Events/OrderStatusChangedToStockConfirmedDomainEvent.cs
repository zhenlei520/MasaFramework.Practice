using Masa.BuildingBlocks.Ddd.Domain.Events;

namespace Assignment17.Ordering.Domain.Events;

/// <summary>
/// Event used when the order stock items are confirmed
/// </summary>
public record OrderStatusChangedToStockConfirmedDomainEvent(int OrderId) : DomainEvent;
