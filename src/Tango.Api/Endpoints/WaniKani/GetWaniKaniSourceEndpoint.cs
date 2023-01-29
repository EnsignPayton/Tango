using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.Mapping;
using Tango.Api.Repository;

namespace Tango.Api.Endpoints.WaniKani;

public class GetWaniKaniSourceEndpoint : IEndpoint
{
    private readonly IWaniKaniRepository _waniKaniRepository;

    public GetWaniKaniSourceEndpoint(IWaniKaniRepository waniKaniRepository)
    {
        _waniKaniRepository = waniKaniRepository;
    }

    public async Task<IResult> HandleAsync(string value)
    {
        var source = await _waniKaniRepository.GetAsync(value);

        if (source == null)
            return Results.NotFound();

        var response = source.ToResponse();
        return Results.Ok(response);
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/wanikani", (GetWaniKaniSourceEndpoint endpoint, [FromQuery] string value) =>
            endpoint.HandleAsync(value))
            .WithName(nameof(GetWaniKaniSourceEndpoint));
    }
}