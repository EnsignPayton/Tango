namespace Tango.Api.Words.Repository;

public class InMemoryWordRepository : IWordRepository
{
    private readonly Dictionary<string, Word> _words = new()
    {
        // Dummy test data
        { "本", new() { Value = "本", KnowledgeFactor = 1.0 } },
        { "日", new() { Value = "日", KnowledgeFactor = 1.0 } },
        { "日本", new() { Value = "日本", KnowledgeFactor = 0.8 } },
    };
    
    public Task<bool> CreateAsync(Word word)
    {
        _words[word.Value] = word;
        return Task.FromResult(true);
    }

    public Task<Word?> GetAsync(string value)
    {
        return _words.TryGetValue(value, out var result)
            ? Task.FromResult<Word?>(result)
            : Task.FromResult<Word?>(null);
    }

    public Task<IEnumerable<Word>> GetAllAsync()
    {
        return Task.FromResult(_words.Values.AsEnumerable());
    }

    public Task<bool> UpdateAsync(Word word)
    {
        _words[word.Value] = word;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(string value)
    {
        _words.Remove(value);
        return Task.FromResult(true);
    }

    public Task<int> GetCountAsync(double minFactor)
    {
        var result = _words.Values.AsEnumerable().Count(x => x.KnowledgeFactor >= minFactor);
        return Task.FromResult(result);
    }
}