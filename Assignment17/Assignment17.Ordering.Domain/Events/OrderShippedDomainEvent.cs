using Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;
using Masa.BuildingBlocks.Ddd.Domain.Events;

namespace Assignment17.Ordering.Domain.Events;

public record OrderShippedDomainEvent(Order Order) : DomainEvent;
