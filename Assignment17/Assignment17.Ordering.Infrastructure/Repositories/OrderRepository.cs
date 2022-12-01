using Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;
using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Microsoft.EntityFrameworkCore;
using EntityState = Masa.BuildingBlocks.Data.UoW.EntityState;

namespace Assignment17.Ordering.Infrastructure.Repositories;

public class OrderRepository : Repository<OrderingContext, Order, int>, IOrderRepository
{
    public OrderRepository(OrderingContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public override async ValueTask<Order> AddAsync(Order entity, CancellationToken cancellationToken = default)
    {
        EntityState = EntityState.Changed;
        var order = (await Context.Orders.AddAsync(entity, cancellationToken)).Entity;
        await Context.Entry(order).Reference(o => o.OrderStatus).LoadAsync(cancellationToken);
        return order;
    }

    public override async Task<Order?> FindAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await Context
            .Orders
            .Include(x => x.Address)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken: cancellationToken);
        if (order == null)
        {
            order = Context
                .Orders
                .Local
                .FirstOrDefault(o => o.Id == id);
        }
        if (order != null)
        {
            await Context.Entry(order)
                .Collection(i => i.OrderItems).LoadAsync(cancellationToken);
            await Context.Entry(order)
                .Reference(i => i.OrderStatus).LoadAsync(cancellationToken);
        }

        return order;
    }
}
