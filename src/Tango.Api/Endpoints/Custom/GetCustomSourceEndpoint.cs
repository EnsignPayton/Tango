using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.Mapping;
using Tango.Api.Repository;

namespace Tango.Api.Endpoints.Custom;

public class GetCustomSourceEndpoint : IEndpoint
{
    private readonly ICustomSourceRepository _customSourceRepository;

    public GetCustomSourceEndpoint(ICustomSourceRepository customSourceRepository)
    {
        _customSourceRepository = customSourceRepository;
    }

    public async Task<IResult> HandleAsync(string value)
    {
        var source = await _customSourceRepository.GetAsync(value);

        if (source == null)
            return Results.NotFound();

        var response = source.ToResponse();
        return Results.Ok(response);
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/custom", (GetCustomSourceEndpoint endpoint, [FromQuery] string value) =>
            endpoint.HandleAsync(value))
            .WithName(nameof(GetCustomSourceEndpoint));
    }
}