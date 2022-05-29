namespace Assignment.Server.Application;

public class UserEventHandler
{
    private readonly UserDbContext _userDbContext;

    public UserEventHandler(UserDbContext dbContext)
        => _userDbContext = dbContext;

    [EventHandler]
    public async Task RegisterUserAsync(RegisterUserEvent @event)
    {
        var user = new User(@event.Name);
        await _userDbContext.Set<User>().AddAsync(user);
        await _userDbContext.SaveChangesAsync();
    }
}
