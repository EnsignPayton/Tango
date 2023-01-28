namespace Tango.Api.Common;

public interface IEndpoint
{
    static abstract void Register(IEndpointRouteBuilder app);
}