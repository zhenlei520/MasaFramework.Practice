using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
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
