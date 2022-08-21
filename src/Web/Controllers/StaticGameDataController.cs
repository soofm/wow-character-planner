using CharacterPlanner.Application.Profile.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CharacterPlanner.Web.Controllers;

[ApiController]
[Route("api/static")]
public class StaticGameDataController : ControllerBase
{
    private readonly IMediator _mediator;
    public StaticGameDataController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("mounts")]
    public async Task<ActionResult> ListMounts()
    {
        var result = await _mediator.Send(new ListMountsQuery());
        return Ok(result);
    }
}
