namespace Assignment.Mapster.Domain.Aggregate;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public uint Gender { get; set; }

    public DateTime BirthDay { get; set; }

    public DateTime CreationTime { get; set; }

    public User()
    {
        CreationTime = DateTime.Now;
    }
}
