namespace Assignment.DependencyInjection.Infrastructure.Services;

public class BaseService : ISingletonDependency
{
    public static int Count { get; set; } = 0;

    public BaseService()
    {
        Count++;
    }

    public BaseService(bool isChildren)
    {

    }
}
