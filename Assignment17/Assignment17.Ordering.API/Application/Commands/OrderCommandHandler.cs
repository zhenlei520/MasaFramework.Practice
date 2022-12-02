using Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;
using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Dispatcher.Events;

namespace Assignment17.Ordering.API.Application.Commands;

public class OrderCommandHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderCommandHandler> _logger;

    public OrderCommandHandler(IOrderRepository orderRepository, ILogger<OrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    [EventHandler]
    public async Task CreateOrderCommandHandler(CreateOrderCommand message, CancellationToken cancellationToken)
    {
        var address = new Address(message.Street, message.City, message.State, message.Country, message.ZipCode);
        var order = new Order(message.UserId, message.UserName, address, message.CardTypeId, message.CardNumber, message.CardSecurityNumber,
            message.CardHolderName, message.CardExpiration);

        foreach (var item in message.OrderItems)
        {
            order.AddOrderItem(item.ProductId, item.ProductName, item.UnitPrice, item.Discount, item.PictureUrl, item.Units);
        }

        _logger.LogInformation("----- Creating Order - Order: {@Order}", order);

        await _orderRepository.AddAsync(order, cancellationToken);
    }
}
