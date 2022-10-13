using Assignment.GlobalFilterDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.GlobalFilterDemo.Controllers;

[ApiController]
[Route("[Action]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public void Register(User user)
    {
        if (string.IsNullOrEmpty(user.Name))
            throw new ArgumentNullException(nameof(user.Name));

        //todo: Impersonate a registered user
    }
}
