using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tango.Api.Common;

public static class EndpointLoader
{
    private static readonly Type[] Endpoints = Assembly.GetExecutingAssembly().GetTypes()
        .Where(x => x is { IsClass: true, IsAbstract: false })
        .Where(x => x.GetInterfaces().Contains(typeof(IEndpoint)))
        .ToArray();

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        foreach (var endpoint in Endpoints)
        {
            services.TryAddScoped(endpoint);
        }

        return services;
    }

    public static void AddEndpoints(this IEndpointRouteBuilder app)
    {
        foreach (var endpoint in Endpoints)
        {
            var regMethod = endpoint.GetMethod(nameof(IEndpoint.Register), BindingFlags.Public | BindingFlags.Static);
            regMethod!.Invoke(null, new object?[] { app });
        }
    }
}