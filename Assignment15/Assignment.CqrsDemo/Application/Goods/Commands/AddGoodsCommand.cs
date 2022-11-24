using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;

namespace Assignment.CqrsDemo.Application.Goods.Commands;

/// <summary>
/// 添加商品参数，用于接受商品参数
/// </summary>
public record AddGoodsCommand : Command
{
    public string Name { get; set; }

    public string Cover { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }
}
