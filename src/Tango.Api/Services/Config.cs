using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tango.Api.Services;

public static class Config
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.TryAddSingleton<WordService>();
        return services;
    }
}
