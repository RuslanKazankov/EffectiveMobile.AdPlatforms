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
    
    public Task<bool> UpdateLocations(Stream locationsFile)
    {
        return Task.Run(() =>
        {
            var streamReader = new StreamReader(locationsFile);
            _platformsRepository.ClearContext();

            var line = streamReader.ReadLine();
            while (line != null)
            {
                var splitLine = line.Split(':');
                var platform = splitLine[0];
                var locations = splitLine[1].Split(',');

                _platformsRepository.AddPlatform(platform, locations);

                line = streamReader.ReadLine();
            }

            return Task.FromResult(true);
        });
    }

    public Task<IReadOnlyList<string>> SearchPlatforms(string location)
    {
        return Task.Run(() =>
        {
            HashSet<string> result = [];
        
            var splitLocation = location.Split('/');
            var sBuilder = new StringBuilder();
            for (int i = 1; i < splitLocation.Length; i++)
            {
                sBuilder.Append('/');
                sBuilder.Append(splitLocation[i]);
                var platforms = _platformsRepository.GetPlatforms(sBuilder.ToString());
                result.UnionWith(platforms);
            }
        
            return Task.FromResult<IReadOnlyList<string>>(result.ToList());
        });
    }
}