using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;

namespace Assignment.IntegrationEventBus.Events;

public record RegisterUserEvent : IntegrationEvent
{
    public override string Topic { get; set; } = nameof(RegisterUserEvent);

    public string Account { get; set; }

    public string Mobile { get; set; }
}
