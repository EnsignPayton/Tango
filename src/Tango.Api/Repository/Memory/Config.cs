using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tango.Api.Repository.Memory;

public static class Config
{
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        services.TryAddSingleton<IWordRepository, InMemoryWordRepository>();
        services.TryAddSingleton<ICustomSourceRepository, InMemoryCustomSourceRepository>();
        services.TryAddSingleton<IWaniKaniRepository, InMemoryWaniKaniRepository>();
        return services;
    }
}