using Assignment.Mapster.Domain;
using Assignment.Mapster.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Mapster.Infrastructure;

public class UserDbContext : DbContext
{
    public DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dataBaseName = Guid.NewGuid().ToString();
        optionsBuilder.UseInMemoryDatabase(dataBaseName);
    }
}
