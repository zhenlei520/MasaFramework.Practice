namespace Assignment.CqrsDemo.Dto;

/// <summary>
/// 用于返回商品信息
/// </summary>
public class GoodsItemDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Cover { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }

    public DateTime DateTime { get; set; }
}
