using Assignment17.Ordering.Domain.AggregatesModel.BuyerAggregate;
using Masa.BuildingBlocks.Ddd.Domain.Events;

namespace Assignment17.Ordering.Domain.Events;

public record BuyerAndPaymentMethodVerifiedDomainEvent(Buyer Buyer, PaymentMethod Payment, int OrderId) : DomainEvent
{
    public Buyer Buyer { get; private set; } = Buyer;
    public PaymentMethod Payment { get; private set; } = Payment;
    public int OrderId { get; private set; } = OrderId;
}
