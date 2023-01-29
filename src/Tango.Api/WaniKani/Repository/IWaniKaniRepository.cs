using Tango.Api.Common;

namespace Tango.Api.WaniKani.Repository;

public interface IWaniKaniRepository : IRepository<string, WaniKaniSource>
{
}