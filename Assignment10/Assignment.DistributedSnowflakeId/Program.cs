using Masa.BuildingBlocks.Data;
using Masa.Contrib.Data.IdGenerator.Snowflake.Distributed.Redis;
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
builder.Services.AddSnowflake(option => option.UseRedis());

var app = builder.Build();

app.MapGet("/", () => "Hello DistributedSnowflakeId!");

app.MapGet("/id/generator/ioc", (ISnowflakeGenerator snowflakeGenerator)
    => snowflakeGenerator.Create()); //生成id

app.MapGet("/id/generator", ()
    => IdGeneratorFactory.SnowflakeGenerator.Create()); //生成id

app.Run();
