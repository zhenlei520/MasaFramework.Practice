using FluentValidation;
using Masa.BuildingBlocks.Dispatcher.Events;

namespace Assignment.InProcessEventBus.Middlewares;

public class ValidatorMiddleware<TEvent> : Middleware<TEvent>
    where TEvent : IEvent
{
    private readonly ILogger<ValidatorMiddleware<TEvent>>? _logger;
    private readonly IEnumerable<IValidator<TEvent>> _validators;

    public ValidatorMiddleware(IEnumerable<IValidator<TEvent>> validators, ILogger<ValidatorMiddleware<TEvent>>? logger = null)
    {
        _validators = validators;
        _logger = logger;
    }

    public override async Task HandleAsync(TEvent @event, EventHandlerDelegate next)
    {
        var typeName = @event.GetType().FullName;

        _logger?.LogDebug("----- Validating command {CommandType}", typeName);

        var failures = _validators
            .Select(v => v.Validate(@event))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            _logger?.LogError("Validation errors - {CommandType} - Event: {@Command} - Errors: {@ValidationErrors}",
                typeName,
                @event,
                failures);

            throw new ValidationException("Validation exception", failures);
        }

        await next();
    }
}
