namespace EffectiveMobile.AdPlatforms.Domain.Services;

public interface IPlatformsService
{
    Task<bool> UpdateLocations(Stream locationsFile);
    Task<IReadOnlyList<string>> SearchPlatforms(string location);
}