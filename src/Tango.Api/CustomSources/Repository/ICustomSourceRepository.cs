namespace Tango.Api.CustomSources.Repository;

public interface ICustomSourceRepository
{
    Task<bool> CreateAsync(CustomSource customSource);
    Task<CustomSource?> GetAsync(string value);
    Task<bool> UpdateAsync(CustomSource customSource);
    Task<bool> DeleteAsync(string value);
}