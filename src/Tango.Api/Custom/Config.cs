using Microsoft.Extensions.DependencyInjection.Extensions;
using Tango.Api.Custom.Repository;

namespace Tango.Api.Custom;

public static class Config
{
    public static IServiceCollection AddCustom(this IServiceCollection services)
    {
        services.TryAddSingleton<ICustomSourceRepository, InMemoryCustomSourceRepository>();
        return services;
    }
}