using Assignment.GlobalFilterDemo.Model;
using Assignment.GlobalFilterDemo.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.GlobalFilterDemo.Controllers;

[ApiController]
[Route("[Action]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public void Register(User user)
    {
        var validator = new UserValidator();
        var results = validator.Validate(user);
        if (!results.IsValid)
            throw new ValidationException(results.Errors);

        //todo: Impersonate a registered user
    }
}
