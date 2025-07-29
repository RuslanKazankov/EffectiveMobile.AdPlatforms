namespace EffectiveMobile.AdPlatforms.Domain.Services.Implications;

public sealed class PlatformsService : IPlatformsService
{
    public Task<bool> UpdateLocations(Stream locationsFile)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<string>> SearchLocations(string query)
    {
        throw new NotImplementedException();
    }
}