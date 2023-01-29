using Tango.Api.Entities;

namespace Tango.Api.Repository;

public interface IWordRepository : IRepository<string, Word>
{
    Task<IEnumerable<Word>> GetAllAsync();
    Task<int> GetCountAsync(double minFactor);
}