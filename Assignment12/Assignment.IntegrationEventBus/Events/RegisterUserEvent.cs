using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;

namespace Assignment.IntegrationEventBus.Events;

public record RegisterUserEvent : IntegrationEvent
{
    public override string Topic { get; set; } = nameof(RegisterUserEvent);

    public string Account { get; set; }

    public string Mobile { get; set; }
}
