using System.Collections.Concurrent;

namespace EffectiveMobile.AdPlatforms.Infrastructure.Persistence;

public sealed class AppDbContext
{
    public Dictionary<string, HashSet<string>> LocationPlatforms { get; set; } = [];
}