using Assignment17.Ordering.API.Application.Commands;
using Masa.BuildingBlocks.Dispatcher.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment17.Ordering.API.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IEventBus _eventBus;

    public OrdersController(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    [Route("draft")]
    [HttpPost]
    public async Task<IActionResult> CreateOrderDraftFromBasketDataAsync(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(command, cancellationToken);
        return Ok();
    }
}
