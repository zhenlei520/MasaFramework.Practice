using Microsoft.AspNetCore.Mvc;

namespace Assignment17.Ordering.API.Controllers;

[Route("[controller]")]
[ApiController]
public class HomeController : Controller
{
    [Route("index")]
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("Hello Assignment17");
    }
}
