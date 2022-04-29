namespace Assignment.Server.Infrastructure;

public class UserDbContext : IsolationDbContext<int>
{
    public DbSet<User> User { get; set; } = default!;

    public UserDbContext(MasaDbContextOptions options) : base(options)
    {
    }
}
