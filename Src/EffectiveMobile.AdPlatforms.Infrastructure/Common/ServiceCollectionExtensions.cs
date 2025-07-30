using EffectiveMobile.AdPlatforms.Domain.IRepositories;
using EffectiveMobile.AdPlatforms.Infrastructure.Persistence;
using EffectiveMobile.AdPlatforms.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EffectiveMobile.AdPlatforms.Infrastructure.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<AppDbContext>();
        services.AddScoped<IPlatformsRepository, PlatformsRepository>();
        
        return services;
    }
}