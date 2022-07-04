using FluentValidation;
using Masa.BuildingBlocks.Dispatcher.Events;

namespace Assignment.InProcessEventBus.Events;

/// <summary>
/// 本地事件继承 Event
/// </summary>
public record TransferEvent : Event
{
    public string Account { get; set; } = default!;

    public string ReceiveAccount { get; set; } = default!;

    public decimal Money { get; set; }
}

public class TransferEventValidator : AbstractValidator<TransferEvent>
{
    public TransferEventValidator()
    {
        RuleFor(t => t.Money).GreaterThan(0).WithMessage("转账金额必须大于0");
        RuleFor(t => t.Account).Must(account => !string.IsNullOrEmpty(account)).WithMessage("转账账户错误");
        RuleFor(t => t.ReceiveAccount).Must(account => !string.IsNullOrEmpty(account)).WithMessage("到账账户错误");
    }
}
