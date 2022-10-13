using System.Globalization;using Assignment.MinimalApiDemo.Infrastructure;
using Microsoft.Extensions.Localization;

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

builder.Services.AddLocalization();
var serviceProvider = builder.Services.BuildServiceProvider();

var localizer = serviceProvider.GetRequiredService<IStringLocalizer<TestResource>>();
var res = localizer[""];

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
app.MapGet("/api/v1/users/{id}", (Guid id) =>
{

});
app.Run();
