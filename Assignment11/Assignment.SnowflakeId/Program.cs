using Masa.BuildingBlocks.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSnowflake(options =>
{
    // options.TimestampType = TimestampType.Seconds;//时间戳使用秒
    // options.EnableMachineClock = true;//启用时钟锁
});//注册雪花id
var app = builder.Build();

app.MapGet("/", () => "Hello SnowflakeId!");

app.MapGet("/id/generator/ioc", (ISnowflakeGenerator snowflakeGenerator)
    => snowflakeGenerator.Create()); //生成id

app.MapGet("/id/generator", ()
    => IdGeneratorFactory.SnowflakeGenerator.Create()); //生成id

app.Run();
