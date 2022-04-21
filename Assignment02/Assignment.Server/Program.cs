using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Assignment.Server!");

app.MapGet("/User", ([FromQuery] int id) => new
{
    Id = id,
    Name = "John Doe"
});

app.MapPost("/User", ([FromBody] AddUserRequest request) => $"用户名为：{request.Name}");

app.Run();

public class AddUserRequest
{
    public string Name { get; set; }
}
