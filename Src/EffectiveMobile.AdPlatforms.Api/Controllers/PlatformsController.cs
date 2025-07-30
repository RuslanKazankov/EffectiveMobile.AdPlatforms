using EffectiveMobile.AdPlatforms.Domain.Models;
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
    public async Task<IActionResult> Upload(IFormFile locations, CancellationToken ct)
    {
        var result = await _platformsService.UpdateLocations(locations.OpenReadStream(), ct);
        
        return result.IsSuccess ? Ok(result.Response) : BadRequest(result.Error);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string location, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(location))
        {
            return BadRequest(Errors.MissingLocationParameter);
        }
        
        var result = await _platformsService.SearchPlatforms(location, ct);
        return result.IsSuccess ? Ok(result.Response) : BadRequest(result.Error);;
    }
}