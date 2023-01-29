namespace Tango.Api.WaniKani.Repository;

public class InMemoryWaniKaniRepository : IWaniKaniRepository
{
    private readonly Dictionary<string, WaniKaniSource> _sources = new();

    public Task<bool> CreateAsync(WaniKaniSource value)
    {
        _sources[value.Value] = value;
        return Task.FromResult(true);
    }

    public Task<WaniKaniSource?> GetAsync(string key)
    {
        return _sources.TryGetValue(key, out var result)
            ? Task.FromResult<WaniKaniSource?>(result)
            : Task.FromResult<WaniKaniSource?>(null);
    }

    public Task<bool> UpdateAsync(WaniKaniSource value)
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