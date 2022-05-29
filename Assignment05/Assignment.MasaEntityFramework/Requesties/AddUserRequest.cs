namespace Assignment.MasaEntityFramework.Requesties;

public class AddUserRequest
{
    public string Name { get; set; }

    public uint Gender { get; set; }

    public DateTime BirthDay { get; set; }
}
