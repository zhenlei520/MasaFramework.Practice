namespace Assignment.DependencyInjection.Infrastructure.Cache;

public interface ICache : ISingletonDependency
{
    void Set(string key, string value);
}
