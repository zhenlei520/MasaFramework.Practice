using Assignment17.Ordering.API.Dto;
using Assignment17.Ordering.API.Models;

namespace Assignment17.Ordering.API.Extensions;

public static class BasketItemExtensions
{
    public static IEnumerable<OrderItemDto> ToOrderItemsDTO(this IEnumerable<BasketItem> basketItems)
    {
        foreach (var item in basketItems)
        {
            yield return item.ToOrderItemDto();
        }
    }

    public static OrderItemDto ToOrderItemDto(this BasketItem item)
    {
        return new OrderItemDto()
        {
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            PictureUrl = item.PictureUrl,
            UnitPrice = item.UnitPrice,
            Units = item.Quantity
        };
    }
}
