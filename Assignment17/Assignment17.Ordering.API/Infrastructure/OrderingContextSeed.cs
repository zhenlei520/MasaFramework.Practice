using Assignment17.Ordering.Domain.AggregatesModel.BuyerAggregate;
using Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;
using Assignment17.Ordering.Infrastructure;
using Masa.BuildingBlocks.Ddd.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Assignment17.Ordering.API.Infrastructure;

public class OrderingContextSeed
{
    public async Task SeedAsync(OrderingContext context)
    {
        await using (context)
        {
            await context.Database.MigrateAsync();

            if (!context.CardTypes.Any())
            {
                context.CardTypes.AddRange(GetPredefinedCardTypes());

                await context.SaveChangesAsync();
            }

            if (!context.OrderStatus.Any())
            {
                context.OrderStatus.AddRange(GetPredefinedOrderStatus());
            }

            await context.SaveChangesAsync();
        }
    }

    private IEnumerable<CardType> GetPredefinedCardTypes()
    {
        return Enumeration.GetAll<CardType>();
    }

    private IEnumerable<OrderStatus> GetPredefinedOrderStatus()
    {
        return new List<OrderStatus>()
        {
            OrderStatus.Submitted,
            OrderStatus.AwaitingValidation,
            OrderStatus.StockConfirmed,
            OrderStatus.Paid,
            OrderStatus.Shipped,
            OrderStatus.Cancelled
        };
    }
}
