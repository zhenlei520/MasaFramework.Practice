namespace Assignment.DependencyInjection.Infrastructure.Data;

public interface IData : IScopedDependency
{
    string Name { get; }
}
