using Masa.Contrib.Isolation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus(eventBusBuilder =>
{
    eventBusBuilder.UseIsolationUoW<UserDbContext, int>(
        isolationBuilder => isolationBuilder.UseMultiTenant(),
        dbOptions => dbOptions.UseSqlServer());
});
var app = builder.Build();
// app.UseIsolation(); // todo: 如果未使用EventBus发布事件，则需要使用此方法

app.MapPost("Register", async ([FromServices] IEventBus eventBus, RegisterUserEvent @event)
    => await eventBus.PublishAsync(@event));

app.Run();
