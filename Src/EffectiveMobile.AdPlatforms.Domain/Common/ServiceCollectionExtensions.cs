using EffectiveMobile.AdPlatforms.Domain.Services;
using EffectiveMobile.AdPlatforms.Domain.Services.Implications;
using Microsoft.Extensions.DependencyInjection;

namespace EffectiveMobile.AdPlatforms.Domain.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IPlatformsService, PlatformsService>();
        
        return services;
    }
}