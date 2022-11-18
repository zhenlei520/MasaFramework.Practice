using Microsoft.EntityFrameworkCore;

namespace Assignment.IntegrationEventBus.Infrastructure;

public class UserDbContext : MasaDbContext
{
    public UserDbContext(MasaDbContextOptions<UserDbContext> options) : base(options)
    {
    }
}
