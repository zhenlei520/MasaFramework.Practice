using Masa.Utils.Development.Dapr.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDaprStarter(option =>
{
    option.AppId = "Assignment-Server";
    option.DaprGrpcPort = 7007;
    option.DaprHttpPort = 7008;
    option.AppIdSuffix = string.Empty;
});

var app = builder.Build();

app.MapGet("/", () => "Hello Assignment.Server!");

app.MapGet("/User", ([FromQuery] int id) =>
{
    //todo: 模拟根据id查询用户信息
    return new
    {
        Id = id,
        Name = "John Doe"
    };
});

app.MapPost("/User", ([FromBody] AddUserRequest request) =>
{
    //todo: 模拟添加用户，并返回用户名称
    return request.Name;
});

app.Run();

public class AddUserRequest
{
    public string Name { get; set; }
}
