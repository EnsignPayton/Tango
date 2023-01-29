using Microsoft.Extensions.DependencyInjection.Extensions;
using Tango.Api.WaniKani.Repository;

namespace Tango.Api.WaniKani;

public static class Config
{
    public static IServiceCollection AddWaniKani(this IServiceCollection services)
    {
        services.TryAddSingleton<IWaniKaniRepository, InMemoryWaniKaniRepository>();
        return services;
    }
}