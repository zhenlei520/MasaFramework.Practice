using Masa.BuildingBlocks.Ddd.Domain.Repositories;

namespace Assignment17.Ordering.Domain.AggregatesModel.OrderAggregate;

public interface IOrderRepository : IRepository<Order, int>
{
}
