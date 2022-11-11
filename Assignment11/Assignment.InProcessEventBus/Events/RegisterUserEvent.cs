using Masa.BuildingBlocks.Dispatcher.Events;

namespace Assignment.InProcessEventBus.Events;

public record RegisterUserEvent : Event
{
    public string Account { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
