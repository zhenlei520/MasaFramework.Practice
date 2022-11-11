using FluentValidation;

namespace Assignment.InProcessEventBus.Events;

public class RegisterUserEventValidator : AbstractValidator<RegisterUserEvent>
{
    public RegisterUserEventValidator()
    {
        RuleFor(e => e.Account).NotNull().WithMessage("用户名不能为空");
        RuleFor(e => e.Email).NotNull().WithMessage("邮箱不能为空");
        RuleFor(e => e.Password)
            .NotNull().WithMessage("密码不能为空")
            .MinimumLength(6)
            .WithMessage("密码必须大于6位")
            .MaximumLength(20)
            .WithMessage("密码必须小于20位");
    }
}
