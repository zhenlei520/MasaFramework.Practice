using Assignment.Client.DaprClientWeb.V2;
using Masa.Utils.Caller.Core;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCaller();

var app = builder.Build();

app.MapGet("/", () => "Hello HttpClientWeb.V2!");

app.MapGet("/Test/User/Get", async ([FromServices] UserCaller caller) =>
{
    var id = new Random().Next(1, 10);//默认用户id
    var user = await caller.GetUserAsync(id);
    return $"获取用户信息成功：用户名称为：{user!.Name}";
});

app.MapGet("/Test/User/Add", async ([FromServices] UserCaller caller) =>
{
    var dateTimeOffset = new DateTimeOffset(DateTime.UtcNow);
    string timeSpan = dateTimeOffset.ToUnixTimeSeconds().ToString();
    var userName = "ss_" + timeSpan; //模拟一个用户名
    string? response= await caller.AddUserAsync(userName);
    return $"创建用户成功了，用户名称为：{response}";
});

app.Run();
