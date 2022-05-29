// See https://aka.ms/new-console-template for more information

using Assignment.Masa.Mapster.Domain.Aggregate;
using Masa.BuildingBlocks.Data.Mapping;
using Masa.Contrib.Data.Mapping.Mapster;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello Masa Mapster!");

IServiceCollection services = new ServiceCollection();
services.AddMapping();

var request = new
{
    Name = "Teach you to learn Dapr ……",
    OrderItem = new OrderItem("Teach you to learn Dapr hand by hand", 49.9m)
};
var serviceProvider = services.BuildServiceProvider();
var mapper = serviceProvider.GetRequiredService<IMapper>();
var order = mapper.Map<Order>(request);

Console.WriteLine($"{nameof(Order.TotalPrice)} is {order.TotalPrice}");

Console.ReadKey();
