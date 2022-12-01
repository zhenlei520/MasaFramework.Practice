using Assignment17.Ordering.API.Dto;
using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Assignment17.Ordering.API.Application.Commands;

public record CreateOrderCommand : Command
{
    private readonly List<OrderItemDto> _orderItems;

    public string UserId { get;  set; }

    public string UserName { get;  set; }

    public string City { get;  set; }

    public string Street { get;  set; }

    public string State { get;  set; }

    public string Country { get;  set; }

    public string ZipCode { get;  set; }

    public string CardNumber { get;  set; }

    public string CardHolderName { get;  set; }

    public DateTime CardExpiration { get;  set; }

    public string CardSecurityNumber { get;  set; }

    public int CardTypeId { get;  set; }

    public IEnumerable<OrderItemDto> OrderItems { get; set; }
}
