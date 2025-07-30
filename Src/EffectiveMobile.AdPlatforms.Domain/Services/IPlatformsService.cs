using EffectiveMobile.AdPlatforms.Domain.Models;

namespace EffectiveMobile.AdPlatforms.Domain.Services;

public interface IPlatformsService
{
    Task<Result> UpdateLocations(Stream locationsFile, CancellationToken ct);
    Task<Result<IReadOnlyList<string>>> SearchPlatforms(string location, CancellationToken ct);
}