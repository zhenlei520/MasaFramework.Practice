using Assignment.GlobalExceptionDemo.Model;
using FluentValidation;

namespace Assignment.GlobalExceptionDemo.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Name).Must(name => !string.IsNullOrEmpty(name)).WithMessage($"{nameof(User.Name)} is not empty");
        RuleFor(u => u.Age)
            .LessThanOrEqualTo(150).WithMessage($"{nameof(User.Age)} must be less than 150")
            .GreaterThanOrEqualTo(0).WithMessage($"{nameof(User.Age)} must be greater than 0");
    }
}
