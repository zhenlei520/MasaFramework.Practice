namespace Assignment.DependencyInjection.Infrastructure.Cache;

public class MemoryCache : ICache
{
    private readonly ConcurrentDictionary<string, Lazy<string>> _dicCache = new();

    public void Set(string key, string value)
    {
        _ = _dicCache.AddOrUpdate
        (
            key,
            k => new Lazy<string>(() => value, LazyThreadSafetyMode.ExecutionAndPublication),
            (_, _) => new Lazy<string>(() => value, LazyThreadSafetyMode.ExecutionAndPublication)
        ).Value;
    }
}
