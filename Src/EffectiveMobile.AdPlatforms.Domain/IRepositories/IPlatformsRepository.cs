namespace EffectiveMobile.AdPlatforms.Domain.IRepositories;

public interface IPlatformsRepository
{
    void ClearContext();
    void AddPlatform(string platform, string[] locations);
    string[] GetPlatforms(string location);
}