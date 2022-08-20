using Microsoft.AspNetCore.Mvc;

namespace CharacterPlanner.Web.Controllers;

[ApiController]
[Route("profile")]
public class ProfileController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProfile()
    {
        return Ok("Hello");
    }
}