using Microsoft.AspNetCore.Authentication;

namespace Tango.Api.Auth;

public static class Config
{
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddAuthentication().AddScheme<AuthenticationSchemeOptions, AuthHandler>("MY_AUTH_SCHEME", _ => { });
        return services;
    }
}
