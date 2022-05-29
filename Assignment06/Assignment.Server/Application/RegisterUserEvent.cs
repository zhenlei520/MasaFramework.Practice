namespace Assignment.Server.Application;

public record RegisterUserEvent : Event
{
    public string Name { get; set; } = default!;
}
