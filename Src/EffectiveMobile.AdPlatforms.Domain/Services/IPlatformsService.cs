namespace EffectiveMobile.AdPlatforms.Domain.Services;

public interface IPlatformsService
{
    Task<bool> UpdateLocations(Stream locationsFile, CancellationToken ct);
    Task<IReadOnlyList<string>> SearchPlatforms(string location, CancellationToken ct);
}