using Assignment17.Ordering.API.Application.IntegrationEvents;
using Assignment17.Ordering.Domain.AggregatesModel.BuyerAggregate;
using Assignment17.Ordering.Domain.Events;
using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Ddd.Domain.Repositories;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;

namespace Assignment17.Ordering.API.Application.DomainEventHandlers;

public class OrderHandler
{
    private readonly IBuyerRepository _buyerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIntegrationEventBus _integrationEventBus;

    public OrderHandler(IBuyerRepository buyerRepository, IUnitOfWork unitOfWork, IIntegrationEventBus integrationEventBus)
    {
        _buyerRepository = buyerRepository;
        _unitOfWork = unitOfWork;
        _integrationEventBus = integrationEventBus;
    }

    public async Task ValidateOrAddBuyerAggregateWhenOrderStarted(OrderStartedDomainEvent orderStartedEvent, ILogger<OrderHandler> logger)
    {
        var cardTypeId = (orderStartedEvent.CardTypeId != 0) ? orderStartedEvent.CardTypeId : 1;
        var buyer = await _buyerRepository.FindAsync(orderStartedEvent.UserId);
        bool buyerOriginallyExisted = buyer != null;

        if (!buyerOriginallyExisted)
        {
            buyer = new Buyer(orderStartedEvent.UserId, orderStartedEvent.UserName);
        }

        buyer!.VerifyOrAddPaymentMethod(cardTypeId,
            $"Payment Method on {DateTime.UtcNow}",
            orderStartedEvent.CardNumber,
            orderStartedEvent.CardSecurityNumber,
            orderStartedEvent.CardHolderName,
            orderStartedEvent.CardExpiration,
            orderStartedEvent.Order.Id);

        var buyerUpdated = buyerOriginallyExisted ?
            _buyerRepository.Update(buyer) :
            _buyerRepository.Add(buyer);

        await _unitOfWork.SaveChangesAsync();

        var orderStatusChangedToSubmittedIntegrationEvent = new OrderStatusChangedToSubmittedIntegrationEvent(
            orderStartedEvent.Order.Id,
            orderStartedEvent.Order.OrderStatus.Name,
            buyer.Name);
        await _integrationEventBus.PublishAsync(orderStatusChangedToSubmittedIntegrationEvent);

        logger.LogTrace("Buyer {BuyerId} and related payment method were validated or updated for orderId: {OrderId}.",
            buyerUpdated.Id, orderStartedEvent.Order.Id);
    }
}
