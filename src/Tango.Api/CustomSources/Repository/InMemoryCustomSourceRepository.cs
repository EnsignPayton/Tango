namespace Tango.Api.CustomSources.Repository;

public class InMemoryCustomSourceRepository : ICustomSourceRepository
{
    private readonly Dictionary<string, CustomSource> _sources = new()
    {
        // Dummy test data
        { "本", new() { Value = "本", KnowledgeFactor = 1.0 } },
        { "日", new() { Value = "日", KnowledgeFactor = 1.0 } },
        { "日本", new() { Value = "日本", KnowledgeFactor = 0.8 } },
    };
    
    public Task<bool> CreateAsync(CustomSource customSource)
    {
        _sources[customSource.Value] = customSource;
        return Task.FromResult(true);
    }

    public Task<CustomSource?> GetAsync(string value)
    {
        return _sources.TryGetValue(value, out var result)
            ? Task.FromResult<CustomSource?>(result)
            : Task.FromResult<CustomSource?>(null);
    }

    public Task<bool> UpdateAsync(CustomSource customSource)
    {
        _sources[customSource.Value] = customSource;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(string value)
    {
        _sources.Remove(value);
        return Task.FromResult(true);
    }
}