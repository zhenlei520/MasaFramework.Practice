var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus(eventBusBuilder =>
{
    eventBusBuilder.UseIsolationUoW<UserDbContext, int>(
        isolationBuilder => isolationBuilder.UseMultiTenant(),
        dbOptions => dbOptions.UseSqlServer());
});
var app = builder.Build();

app.MapPost("Register", async ([FromServices] IEventBus eventBus, RegisterUserEvent @event)
    => await eventBus.PublishAsync(@event));

app.Run();
