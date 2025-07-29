using EffectiveMobile.AdPlatforms.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveMobile.AdPlatforms.Api.Controllers;

[ApiController]
[Route("api/platforms")]
public sealed class PlatformsController : ControllerBase
{
    private readonly IPlatformsService _platformsService;

    public PlatformsController(IPlatformsService platformsService)
    {
        _platformsService = platformsService;
    }
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile locations)
    {
        var result = await _platformsService.UpdateLocations(locations.OpenReadStream());
        
        return result ? Ok() : BadRequest();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string location)
    {
        var result = await _platformsService.SearchPlatforms(location);
        return Ok(result);
    }
}