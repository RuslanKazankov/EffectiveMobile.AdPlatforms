using System.Text;
using EffectiveMobile.AdPlatforms.Domain.IRepositories;

namespace EffectiveMobile.AdPlatforms.Domain.Services.Implications;

public sealed class PlatformsService : IPlatformsService
{
    private readonly IPlatformsRepository _platformsRepository;

    public PlatformsService(IPlatformsRepository platformsRepository)
    {
        _platformsRepository = platformsRepository;
    }
    
    public Task<bool> UpdateLocations(Stream locationsFile, CancellationToken ct)
    {
        return Task.Run(bool () =>
        {
            var streamReader = new StreamReader(locationsFile);
            _platformsRepository.ClearContext();

            var line = streamReader.ReadLine();
            while (line != null)
            {
                if (ct.IsCancellationRequested)
                {
                    return false;
                }
                var splitLine = line.Split(':');
                var platform = splitLine[0];
                var locations = splitLine[1].Split(',');

                _platformsRepository.AddPlatform(platform, locations);

                line = streamReader.ReadLine();
            }

            return true;
        }, ct);
    }

    public Task<IReadOnlyList<string>> SearchPlatforms(string location,  CancellationToken ct)
    {
        return Task.Run<IReadOnlyList<string>>(() =>
        {
            HashSet<string> result = [];
        
            var splitLocation = location.Split('/');
            var sBuilder = new StringBuilder();
            for (int i = 1; i < splitLocation.Length; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    return [];
                }
                sBuilder.Append('/');
                sBuilder.Append(splitLocation[i]);
                var platforms = _platformsRepository.GetPlatforms(sBuilder.ToString());
                result.UnionWith(platforms);
            }
        
            return result.ToList();
        }, ct);
    }
}