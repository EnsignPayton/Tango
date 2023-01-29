using Tango.Api.Common;

namespace Tango.Api.Words.Repository;

public interface IWordRepository : IRepository<string, Word>
{
    Task<IEnumerable<Word>> GetAllAsync();
    Task<int> GetCountAsync(double minFactor);
}