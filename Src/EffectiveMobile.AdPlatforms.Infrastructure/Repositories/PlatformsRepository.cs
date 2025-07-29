using EffectiveMobile.AdPlatforms.Domain.IRepositories;
using EffectiveMobile.AdPlatforms.Infrastructure.Persistence;

namespace EffectiveMobile.AdPlatforms.Infrastructure.Repositories;

public sealed class PlatformsRepository : IPlatformsRepository
{
    private AppDbContext _db;

    public PlatformsRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public void ClearContext()
    {
        _db.LocationPlatforms.Clear();
    }

    public void AddPlatform(string platform, string[] locations)
    {
        foreach (var location in locations)
        {
            _db.LocationPlatforms.TryAdd(location, []);
            _db.LocationPlatforms[location].Add(platform);
        }
    }

    public string[] GetPlatforms(string location)
    {
        var success = _db.LocationPlatforms.TryGetValue(location, out var platforms);
        return platforms?.ToArray() ?? [];
    }
}