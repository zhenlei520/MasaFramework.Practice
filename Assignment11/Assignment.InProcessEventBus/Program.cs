using Assignment.InProcessEventBus.Events;
using Assignment.InProcessEventBus.Middlewares;
using FluentValidation.AspNetCore;
using Masa.BuildingBlocks.Dispatcher.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<Program>();
    });
var app = builder.Build();

app.MapGet("/", () => "Hello InProcessEventBus!");

app.MapPost("/transfer", async (TransferEvent @event, IEventBus eventBus)
    => await eventBus.PublishAsync(@event));

app.Run();
