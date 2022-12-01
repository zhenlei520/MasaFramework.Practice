using Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;
using Assignment17.Ordering.Domain.Events;
using Masa.Contrib.Dispatcher.Events;

namespace Assignment17.Ordering.API.Application.DomainEventHandlers;

public class OrderHandler
{
    private readonly ILogger<OrderHandler> _logger;
    private readonly IOrderRepository _orderRepository;

    public OrderHandler(ILogger<OrderHandler> logger, IOrderRepository orderRepository)
    {
        _logger = logger;
        _orderRepository = orderRepository;
    }

    [EventHandler]
    public async Task UpdateOrderWhenBuyerAndPaymentMethodVerifiedDomainEventHandler(
        BuyerAndPaymentMethodVerifiedDomainEvent buyerPaymentMethodVerifiedEvent)
    {
        var orderToUpdate = await _orderRepository.FindAsync(buyerPaymentMethodVerifiedEvent.OrderId);
        if (orderToUpdate == null) throw new Exception("order error");

        orderToUpdate.SetBuyerId(buyerPaymentMethodVerifiedEvent.Buyer.Id);
        orderToUpdate.SetPaymentId(buyerPaymentMethodVerifiedEvent.Payment.Id);

        _logger.LogTrace("Order with Id: {OrderId} has been successfully updated with a payment method {PaymentMethod} ({Id})",
            buyerPaymentMethodVerifiedEvent.OrderId, nameof(buyerPaymentMethodVerifiedEvent.Payment),
            buyerPaymentMethodVerifiedEvent.Payment.Id);
    }
}
