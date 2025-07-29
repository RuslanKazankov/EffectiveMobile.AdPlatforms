using Microsoft.AspNetCore.Mvc;

namespace EffectiveMobile.AdPlatforms.Api.Controllers;

[ApiController]
[Route("api/platforms")]
public sealed class PlatformsController : ControllerBase
{
    [HttpPost("upload")]
    public IActionResult Upload(IFormFile locations)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string location)
    {
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}