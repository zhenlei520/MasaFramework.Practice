using System.Reflection;
using Assignment.InProcessEventBus.Events;
using Assignment.InProcessEventBus.Middlewares;
using FluentValidation;
using Masa.BuildingBlocks.Dispatcher.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(Assembly.GetEntryAssembly());
builder.Services.AddEventBus(eventBusBuilder=>eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)));


// builder.Services.AddEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
//     .AddFluentValidation(options =>
//     {
//         options.RegisterValidatorsFromAssemblyContaining<Program>();
//     });

var app = builder.Build();

app.MapGet("/", () => "Hello EventBus!");

app.MapPost("/register", async (RegisterUserEvent @event, IEventBus eventBus) =>
{
    await eventBus.PublishAsync(@event);
});


app.Run();
