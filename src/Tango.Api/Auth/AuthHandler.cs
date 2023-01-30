using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Tango.Api.Auth;

public class AuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public AuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // I have no idea what I'm doing here
        var authHeader = Context.Request.Headers[HeaderNames.Authorization].ToString();
        var token = authHeader.Split(" ")[1];
        var identity = new GenericIdentity(token);
        var principal = new GenericPrincipal(identity, new[] { "MY_ROLE" });
        var ticket = new AuthenticationTicket(principal, "MY_AUTH_SCHEME");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}
