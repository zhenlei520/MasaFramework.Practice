using Assignment.MinimalApiDemo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

#region swagger

if (builder.Environment.IsDevelopment())
{
    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();
}

#endregion

builder.Services.AddSingleton<IData, Data>();
var app = builder.AddServices(options =>
{
    options.Prefix = string.Empty;
    options.Version = string.Empty;
    options.RouteHandlerBuilder = routeHandlerBuilder => routeHandlerBuilder.RequireAuthorization();
});

#region swagger

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion

app.MapGet("/", () => "Hello Assignment.MinimalApiDemo!");

app.MapGet("/test", () => "Test Success!").RequireAuthorization();

app.Run();
