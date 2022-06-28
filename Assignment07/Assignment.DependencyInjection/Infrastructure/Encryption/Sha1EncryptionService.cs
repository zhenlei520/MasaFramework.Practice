namespace Assignment.DependencyInjection.Infrastructure.Encryption;

[Dependency(ReplaceServices = true)]
public class Sha1EncryptionService : IEncryptionService
{
    public string MethodName => "Sha1";
}
