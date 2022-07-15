namespace Assignment.CqrsDemo.Dto;

public class GoodsItemDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Cover { get; set; }

    public decimal Price { get; set; }

    public int Count { get; set; }

    public DateTime DateTime { get; set; }
}
