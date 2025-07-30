using System.Text;
using EffectiveMobile.AdPlatforms.Domain.IRepositories;
using EffectiveMobile.AdPlatforms.Domain.Models;

namespace EffectiveMobile.AdPlatforms.Domain.Services.Implications;

public sealed class PlatformsService : IPlatformsService
{
    private readonly IPlatformsRepository _platformsRepository;

    public PlatformsService(IPlatformsRepository platformsRepository)
    {
        _platformsRepository = platformsRepository;
    }
    
    public Task<Result> UpdateLocations(Stream locationsFile, CancellationToken ct)
    {
        return Task.Run(Result () =>
        {
            var streamReader = new StreamReader(locationsFile);
            
            var line = streamReader.ReadLine();
            var lineNumber = 1;
            while (line != null)
            {
                if (ct.IsCancellationRequested)
                {
                    return new Result(Errors.TaskCanceled);
                }

                if (line.Count(c => c == ':') != 1)
                {
                    return new Result(Errors.InvalidFileSplitSymbol(lineNumber));
                }
                
                var splitLine = line.Split(':');
                var platform = splitLine[0];
                var locations = splitLine[1].Split(',');
                
                if (!locations.All(x => x.StartsWith('/')))
                {
                    return new Result(Errors.InvalidFileFormatLocation(lineNumber));
                }

                _platformsRepository.AddPlatform(platform, locations);

                lineNumber++;
                line = streamReader.ReadLine();
            }
            _platformsRepository.SaveChanges();
            return new Result();
        }, ct);
    }

    public Task<Result<IReadOnlyList<string>>> SearchPlatforms(string location,  CancellationToken ct)
    {
        return Task.Run(Result<IReadOnlyList<string>> () =>
        {
            HashSet<string> result = [];
        
            var splitLocation = location.Split('/');
            var sBuilder = new StringBuilder();
            for (int i = 1; i < splitLocation.Length; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    return new Result<IReadOnlyList<string>>([], Errors.TaskCanceled);
                }
                sBuilder.Append('/');
                sBuilder.Append(splitLocation[i]);
                var platforms = _platformsRepository.GetPlatforms(sBuilder.ToString());
                result.UnionWith(platforms);
            }

            return new Result<IReadOnlyList<string>>(result.ToArray());
        }, ct);
    }
}