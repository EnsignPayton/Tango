namespace Tango.Api.Repository;

public interface IRepository<in TKey, TValue>
{
    Task<bool> CreateAsync(TValue value);
    Task<TValue?> GetAsync(TKey key);
    Task<bool> UpdateAsync(TValue value);
    Task<bool> DeleteAsync(TKey key);
}