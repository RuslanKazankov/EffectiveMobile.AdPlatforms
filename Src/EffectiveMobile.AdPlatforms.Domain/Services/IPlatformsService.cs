namespace EffectiveMobile.AdPlatforms.Domain.Services;

public interface IPlatformsService
{
    Task<bool> UpdateLocations(Stream locationsFile);
    Task<IReadOnlyCollection<string>> SearchLocations(string query);
}