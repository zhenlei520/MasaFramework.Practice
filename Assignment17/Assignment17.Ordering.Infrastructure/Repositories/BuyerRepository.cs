using Assignment17.Ordering.Domain.AggregatesModel.BuyerAggregate;
using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment17.Ordering.Infrastructure.Repositories;

public class BuyerRepository : Repository<OrderingContext, Buyer>, IBuyerRepository
{
    public BuyerRepository(OrderingContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public Buyer Add(Buyer buyer)
    {
        return Context.Buyers
            .Add(buyer)
            .Entity;
    }

    public Buyer Update(Buyer buyer)
    {
        return Context.Buyers
            .Update(buyer)
            .Entity;
    }

    public async Task<Buyer?> FindAsync(string buyerIdentityGuid)
    {
        var buyer = await Context.Buyers
            .Include(b => b.PaymentMethods)
            .Where(b => b.IdentityGuid == buyerIdentityGuid)
            .SingleOrDefaultAsync();

        return buyer;
    }

    public async Task<Buyer?> FindByIdAsync(string id)
    {
        var buyer = await Context.Buyers
            .Include(b => b.PaymentMethods)
            .Where(b => b.Id == int.Parse(id))
            .SingleOrDefaultAsync();

        return buyer;
    }
}
