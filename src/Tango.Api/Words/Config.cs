using Microsoft.Extensions.DependencyInjection.Extensions;
using Tango.Api.Words.Repository;

namespace Tango.Api.Words;

public static class Config
{
    public static IServiceCollection AddWords(this IServiceCollection services)
    {
        services.TryAddSingleton<IWordRepository, InMemoryWordRepository>();
        return services;
    }
}