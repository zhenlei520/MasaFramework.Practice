namespace Assignment.Masa.Mapster.Domain.Aggregate;

public class Order
{
    public string Name { get; set; }

    public decimal TotalPrice { get; set; }

    public List<OrderItem> OrderItems { get; set; }

    public Order(string name)
    {
        Name = name;
    }

    public Order(string name, OrderItem orderItem) : this(name)
    {
        OrderItems = new List<OrderItem> { orderItem };
        TotalPrice = OrderItems.Sum(item => item.Price * item.Number);
    }
}
