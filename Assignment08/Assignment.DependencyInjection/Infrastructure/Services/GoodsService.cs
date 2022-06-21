namespace Assignment.DependencyInjection.Infrastructure.Services;

public class GoodsService : GoodsBaseService
{
    public static int GoodsCount { get; set; } = 0;

    public GoodsService()
    {
        GoodsCount++;
    }
}
