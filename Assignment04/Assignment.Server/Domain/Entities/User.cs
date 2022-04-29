namespace Assignment.Server.Domain.Entities;

public class User : IMultiTenant<int>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int TenantId { get; set; }

    public User()
    {

    }

    public User(string name)
    {
        this.Name = name;
    }
}
