namespace Tango.Api.Words.Repository;

public class InMemoryWordRepository : IWordRepository
{
    private readonly Dictionary<string, Word> _words = new();
    
    public Task<bool> CreateAsync(Word value)
    {
        _words[value.Value] = value;
        return Task.FromResult(true);
    }

    public Task<Word?> GetAsync(string key)
    {
        return _words.TryGetValue(key, out var result)
            ? Task.FromResult<Word?>(result)
            : Task.FromResult<Word?>(null);
    }

    public Task<IEnumerable<Word>> GetAllAsync()
    {
        return Task.FromResult(_words.Values.AsEnumerable());
    }

    public Task<bool> UpdateAsync(Word value)
    {
        _words[value.Value] = value;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(string key)
    {
        _words.Remove(key);
        return Task.FromResult(true);
    }

    public Task<int> GetCountAsync(double minFactor)
    {
        var result = _words.Values.AsEnumerable().Count(x => x.KnowledgeFactor >= minFactor);
        return Task.FromResult(result);
    }
}