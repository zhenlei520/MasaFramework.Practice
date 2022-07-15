using Assignment.CqrsDemo.Dto;
using Masa.BuildingBlocks.ReadWriteSpliting.Cqrs.Queries;

namespace Assignment.CqrsDemo.Application.Goods.Queries;

public record GoodsItemQuery : Query<GoodsItemDto>
{
    public Guid Id { get; set; }

    public override GoodsItemDto Result { get; set; }
}
