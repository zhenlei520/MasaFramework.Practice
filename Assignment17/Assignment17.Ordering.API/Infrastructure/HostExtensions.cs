using Microsoft.EntityFrameworkCore;

namespace Assignment17.Ordering.API.Infrastructure;

public static class HostExtensions
{
    public static async Task MigrateDbContext<TContext>(this IHost host, Func<TContext, IServiceProvider,Task> seeder)
        where TContext : DbContext
    {
        await using var scope = host.Services.CreateAsyncScope();
        var serviceProvider = scope.ServiceProvider;
        var context = serviceProvider.GetRequiredService<TContext>();
        await seeder(context, serviceProvider);
    }
}
