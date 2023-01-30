using Microsoft.Net.Http.Headers;

namespace Tango.Api.Auth;

public static class AuthExtensions
{
    public static Guid ParseUserId(this HttpContext context)
    {
        var authHeader = context.Request.Headers[HeaderNames.Authorization].ToString();
        var token = authHeader.Split(" ")[1];
        return Guid.TryParse(token, out var result) ? result : Guid.Empty;
    }
}
