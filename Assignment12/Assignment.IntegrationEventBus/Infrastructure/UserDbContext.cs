using Masa.Contrib.Data.EntityFrameworkCore;

namespace Assignment.IntegrationEventBus.Infrastructure;

public class UserDbContext : MasaDbContext
{
    public UserDbContext(MasaDbContextOptions options) : base(options)
    {
    }
}
