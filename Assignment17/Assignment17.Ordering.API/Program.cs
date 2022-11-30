using Assignment17.Ordering.Domain.Exceptions;
using Assignment17.Ordering.Infrastructure;
using Masa.Contrib.Data.UoW.EFCore;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;
using Masa.Contrib.Dispatcher.Events;
using Masa.Contrib.Dispatcher.IntegrationEvents;
using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddMasaDbContext<OrderingContext>(options => options.UseSqlServer());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
assemblies.Add(typeof(OrderingContext).Assembly);
assemblies.Add(typeof(OrderingDomainException).Assembly);

builder.Services
    .AddDomainEventBus(assemblies.Distinct().ToArray(),options =>
    {
        options
            .UseIntegrationEventBus(dispatcherOptions => dispatcherOptions.UseDapr().UseEventLog<OrderingContext>())
            .UseEventBus(eventBuilder => eventBuilder.UseMiddleware(typeof(ValidatorMiddleware<>)))
            .UseUoW<OrderingContext>(dbContextBuilder => dbContextBuilder.UseSqlServer())
            .UseRepository<OrderingContext>();
    })
    .AddControllers();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
