using Tango.Api.Entities;

namespace Tango.Api.Repository.Memory;

public class CustomSourceRepository : ICustomSourceRepository
{
    private readonly Dictionary<string, CustomSource> _sources = new();
    
    public Task<bool> CreateAsync(CustomSource value)
    {
        _sources[value.Value] = value;
        return Task.FromResult(true);
    }

    public Task<CustomSource?> GetAsync(string key)
    {
        return _sources.TryGetValue(key, out var result)
            ? Task.FromResult<CustomSource?>(result)
            : Task.FromResult<CustomSource?>(null);
    }

    public Task<bool> UpdateAsync(CustomSource value)
    {
        _sources[value.Value] = value;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(string key)
    {
        _sources.Remove(key);
        return Task.FromResult(true);
    }
}