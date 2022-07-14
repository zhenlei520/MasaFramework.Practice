namespace Assignment.MinimalApiDemo.Request;

public class RegisterUserRequest
{
    public string Name { get; set; } = default!;

    public int Age { get; set; }
}
