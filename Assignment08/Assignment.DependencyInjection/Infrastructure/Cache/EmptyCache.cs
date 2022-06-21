namespace Assignment.DependencyInjection.Infrastructure.Cache;

/// <summary>
/// 空实现
/// </summary>
[Dependency(TryRegister = true)]
public class EmptyCache : ICache
{
    public void Set(string key, string value)
    {
        throw new NotSupportedException($"暂不支持{nameof(Set)}方法");
    }
}
