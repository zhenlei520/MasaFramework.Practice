using Assignment.MinimalApiDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IData, Data>();
var app = builder.AddServices();
builder.Build();
app.MapGet("/", () => "Hello Assignment.MinimalApiDemo!");

app.MapGet("/test", () => "Test Success!").AllowAnonymous().RequireAuthorization();

app.Run();
