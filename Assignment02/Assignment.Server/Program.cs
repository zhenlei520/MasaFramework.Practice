var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Assignment.Server!");

app.MapGet("Hello1", () => "Hello World1!");

app.MapPost("Hello2", () => "Hello World2!");

app.MapGet("/Service/Hello3", () => "Hello World3!");

app.MapPost("/Service/Hello4", () => "Hello World4!");

app.Run();
