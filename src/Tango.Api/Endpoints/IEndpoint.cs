namespace Tango.Api.Endpoints;

public interface IEndpoint
{
    static abstract void Register(IEndpointRouteBuilder app);
}
