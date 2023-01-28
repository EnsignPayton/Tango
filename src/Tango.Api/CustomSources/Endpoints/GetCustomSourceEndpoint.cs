using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.CustomSources.Repository;
using Tango.Api.CustomSources.Requests;

namespace Tango.Api.CustomSources.Endpoints;

public class GetCustomSourceEndpoint : IEndpoint
{
    private readonly ICustomSourceRepository _customSourceRepository;

    public GetCustomSourceEndpoint(ICustomSourceRepository customSourceRepository)
    {
        _customSourceRepository = customSourceRepository;
    }

    public async Task<IResult> HandleAsync(GetCustomSourceRequest request)
    {
        var customSource = await _customSourceRepository.GetAsync(request.Value);

        if (customSource == null)
            return Results.NotFound();

        var response = customSource.ToResponse();
        return Results.Ok(response);
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/custom", (GetCustomSourceEndpoint endpoint, [FromQuery] string value) =>
            endpoint.HandleAsync(new GetCustomSourceRequest { Value = value }))
            .WithName(nameof(GetCustomSourceEndpoint));
    }
}