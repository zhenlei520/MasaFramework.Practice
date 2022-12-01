using FluentValidation;

namespace Assignment17.Ordering.API.Application.Commands;

public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.Country).NotNull().WithMessage("收件人信息有误");
        RuleFor(o => o.City).NotNull().WithMessage("收件人信息有误");
        RuleFor(o => o.Street).NotNull().WithMessage("收件人信息有误");
        RuleFor(o => o.ZipCode).NotNull().WithMessage("收件人邮政编码信息有误");
    }
}
