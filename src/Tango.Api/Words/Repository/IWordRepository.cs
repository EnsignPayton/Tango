namespace Tango.Api.Words.Repository;

public interface IWordRepository
{
    Task<bool> CreateAsync(Word word);
    Task<Word?> GetAsync(string value);
    Task<IEnumerable<Word>> GetAllAsync();
    Task<bool> UpdateAsync(Word word);
    Task<bool> DeleteAsync(string value);

    Task<int> GetCountAsync(double minFactor);
}