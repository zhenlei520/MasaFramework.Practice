using Masa.BuildingBlocks.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalDistributedLock();
var app = builder.Build();

app.MapGet("/", () => "Hello DistributedLocking.Local!");

app.MapGet("lock", (IDistributedLock distributedLock) =>
{
    using var lockObj = distributedLock.TryGet("test", TimeSpan.FromSeconds(3));
    if (lockObj != null)
    {
        //todo: 获取锁成功
        return "success";
    }
    return "获取超时";
});

app.Run();
