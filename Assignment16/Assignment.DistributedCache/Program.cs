using Assignment.DistributedCache.Model;
using Masa.BuildingBlocks.Caching;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedCache(distributedCacheOptions =>
{
    distributedCacheOptions.UseStackExchangeRedisCache();
});

var app = builder.Build();

app.MapGet("/", () => "Hello Distributed Cache!");

app.MapPost("/set/{id}", async (IDistributedCacheClient distributedCacheClient, [FromRoute] string id, [FromBody] User user) =>
{
    await distributedCacheClient.SetAsync(id, user);
    return Results.Accepted();
});

app.MapGet("/get/{id}", async (IDistributedCacheClient distributedCacheClient, [FromRoute] string id) =>
{
    var value = await distributedCacheClient.GetAsync<User>(id);
    return Results.Ok(value);
});

app.Run();
