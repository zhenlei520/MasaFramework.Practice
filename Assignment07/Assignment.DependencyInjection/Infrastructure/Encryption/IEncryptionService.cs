namespace Assignment.DependencyInjection.Infrastructure.Encryption;

public interface IEncryptionService : ISingletonDependency
{
    string MethodName { get; }
}
