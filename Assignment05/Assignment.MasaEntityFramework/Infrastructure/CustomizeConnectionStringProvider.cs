using Masa.BuildingBlocks.Data;

namespace Assignment.MasaEntityFramework.Infrastructure;

public class CustomizeConnectionStringProvider : IConnectionStringProvider
{
    public Task<string> GetConnectionStringAsync(string name = "DefaultConnection") => Task.FromResult(GetConnectionString(name));

    public string GetConnectionString(string name = "DefaultConnection") => "test3";
}
