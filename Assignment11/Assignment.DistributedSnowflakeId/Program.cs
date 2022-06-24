using Masa.BuildingBlocks.Data;
using Masa.Utils.Caching.Redis.DependencyInjection;
using Masa.Utils.Caching.Redis.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMasaRedisCache(option =>
{
    option.Servers = new List<RedisServerOptions>()
    {
        new("localhost", 6379)
    };
    option.DefaultDatabase = 2;
    option.Password = "";
});
builder.Services.AddDistributedSnowflake();

var app = builder.Build();

app.MapGet("/", () => "Hello DistributedSnowflakeId!");

app.MapGet("/id/generator/ioc", (ISnowflakeGenerator snowflakeGenerator)
    => snowflakeGenerator.Create()); //生成id

app.MapGet("/id/generator", ()
    => IdGeneratorFactory.SnowflakeGenerator.Create()); //生成id

app.Run();
