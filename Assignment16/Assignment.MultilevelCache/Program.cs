using Assignment.MultilevelCache.Model;
using Masa.BuildingBlocks.Caching;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMultilevelCache(distributedCacheOptions =>
{
    distributedCacheOptions.UseStackExchangeRedisCache();//使用分布式Redis缓存
});
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//设置缓存
app.MapPost("/set/{id}", async (IMultilevelCacheClient multilevelCacheClient, [FromRoute] string id, [FromBody] User user) =>
{
    await multilevelCacheClient.SetAsync(id, user);
    return Results.Accepted();
});

//获取缓存
app.MapGet("/get/{id}", async (IMultilevelCacheClient multilevelCacheClient, [FromRoute] string id) =>
{
    var value = await multilevelCacheClient.GetAsync<User>(id);
    return Results.Ok(value);
});

app.Run();
