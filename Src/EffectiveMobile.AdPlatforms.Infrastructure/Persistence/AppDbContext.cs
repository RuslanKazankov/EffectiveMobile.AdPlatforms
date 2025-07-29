using System.Collections.Concurrent;

namespace EffectiveMobile.AdPlatforms.Infrastructure.Persistence;

public sealed class AppDbContext
{
    public ConcurrentDictionary<string, HashSet<string>> LocationPlatforms { get; } = [];
}