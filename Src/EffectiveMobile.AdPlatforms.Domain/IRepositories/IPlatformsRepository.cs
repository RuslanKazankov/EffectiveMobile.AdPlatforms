namespace EffectiveMobile.AdPlatforms.Domain.IRepositories;

public interface IPlatformsRepository
{
    void SaveChanges();
    void AddPlatform(string platform, string[] locations);
    string[] GetPlatforms(string location);
}