using Masa.BuildingBlocks.Data;
using Masa.BuildingBlocks.Data.Options;

namespace Assignment.MasaEntityFramework.Infrastructure;

public class CustomizeDbConnectionStringProvider : IDbConnectionStringProvider
{
    public List<MasaDbContextConfigurationOptions> DbContextOptionsList { get; } = new()
    {
        new MasaDbContextConfigurationOptions("test3")
    };
}
