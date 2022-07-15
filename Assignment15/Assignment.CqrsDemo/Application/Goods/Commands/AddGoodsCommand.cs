using Masa.BuildingBlocks.ReadWriteSpliting.Cqrs.Commands;

namespace Assignment.CqrsDemo.Application.Goods.Commands;

public record AddGoodsCommand : Command
{
    public string Name { get; set; }

    public string Cover { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }
}
