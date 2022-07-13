var builder = WebApplication.CreateBuilder(args);
var app = builder.AddServices();

app.MapGet("/", () => "Hello Assignment.MinimalApiDemo!");

app.MapGet("/test", () => "Test Success!").AllowAnonymous().RequireAuthorization("");

app.Run();
