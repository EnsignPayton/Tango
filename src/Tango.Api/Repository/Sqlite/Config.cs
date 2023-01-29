using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tango.Api.Repository.Sqlite;

public static class Config
{
    public static IServiceCollection AddSqlite(this IServiceCollection services)
    {
        services.TryAddSingleton<IWordRepository, WordRepository>();
        services.TryAddSingleton<ICustomSourceRepository, CustomSourceRepository>();
        services.TryAddSingleton<IWaniKaniRepository, WaniKaniRepository>();
        return services;
    }
}