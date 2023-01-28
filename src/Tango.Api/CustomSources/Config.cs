using Microsoft.Extensions.DependencyInjection.Extensions;
using Tango.Api.CustomSources.Repository;

namespace Tango.Api.CustomSources;

public static class Config
{
    public static IServiceCollection AddCustomSources(this IServiceCollection services)
    {
        services.TryAddSingleton<ICustomSourceRepository, InMemoryCustomSourceRepository>();
        return services;
    }
}