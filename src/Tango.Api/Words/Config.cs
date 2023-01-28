using Tango.Api.Words.Repository;

namespace Tango.Api.Words;

public static class Config
{
    public static IServiceCollection AddWords(this IServiceCollection services)
    {
        services.AddSingleton<IWordRepository, InMemoryWordRepository>();

        return services;
    }
}