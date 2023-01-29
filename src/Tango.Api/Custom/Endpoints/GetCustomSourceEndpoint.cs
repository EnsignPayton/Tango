using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.Custom.Repository;
using Tango.Api.Custom.Requests;

namespace Tango.Api.Custom.Endpoints;

public class GetCustomSourceEndpoint : IEndpoint
{
    private readonly ICustomSourceRepository _customSourceRepository;

    public GetCustomSourceEndpoint(ICustomSourceRepository customSourceRepository)
    {
        _customSourceRepository = customSourceRepository;
    }

    public async Task<IResult> HandleAsync(GetCustomSourceRequest request)
    {
        var source = await _customSourceRepository.GetAsync(request.Value);

        if (source == null)
            return Results.NotFound();

        var response = source.ToResponse();
        return Results.Ok(response);
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/custom", (GetCustomSourceEndpoint endpoint, [FromQuery] string value) =>
            endpoint.HandleAsync(new GetCustomSourceRequest { Value = value }))
            .WithName(nameof(GetCustomSourceEndpoint));
    }
}