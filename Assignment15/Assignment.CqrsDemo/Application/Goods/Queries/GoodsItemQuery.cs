using Assignment.CqrsDemo.Dto;
using Masa.BuildingBlocks.ReadWriteSpliting.Cqrs.Queries;

namespace Assignment.CqrsDemo.Application.Goods.Queries;

/// <summary>
/// 用于接收查询商品信息参数
/// </summary>
public record GoodsItemQuery : Query<GoodsItemDto>
{
    public Guid Id { get; set; } = default!;

    public override GoodsItemDto Result { get; set; }

    public GoodsItemQuery(Guid id)
    {
        Id = id;
    }
}
