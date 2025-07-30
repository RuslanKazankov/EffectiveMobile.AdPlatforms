using System.Collections.Concurrent;
using EffectiveMobile.AdPlatforms.Domain.IRepositories;
using EffectiveMobile.AdPlatforms.Infrastructure.Persistence;

namespace EffectiveMobile.AdPlatforms.Infrastructure.Repositories;

public sealed class PlatformsRepository : IPlatformsRepository
{
    private AppDbContext _db;
    private Dictionary<string, HashSet<string>> _scopeLocationPlatforms = [];

    public PlatformsRepository(AppDbContext db)
    {
        _db = db;
    }
    
    public void SaveChanges()
    {
        _db.LocationPlatforms = _scopeLocationPlatforms;
        _scopeLocationPlatforms = new Dictionary<string, HashSet<string>>();
    }

    public void AddPlatform(string platform, string[] locations)
    {
        foreach (var location in locations)
        {
            _scopeLocationPlatforms.TryAdd(location, []);
            _scopeLocationPlatforms[location].Add(platform);
        }
    }

    public string[] GetPlatforms(string location)
    {
        var success = _db.LocationPlatforms.TryGetValue(location, out var platforms);
        return platforms?.ToArray() ?? [];
    }
}