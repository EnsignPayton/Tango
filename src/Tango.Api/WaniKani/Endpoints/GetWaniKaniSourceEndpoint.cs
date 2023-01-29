using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.WaniKani.Repository;
using Tango.Api.WaniKani.Requests;

namespace Tango.Api.WaniKani.Endpoints;

public class GetWaniKaniSourceEndpoint : IEndpoint
{
    private readonly IWaniKaniRepository _waniKaniRepository;

    public GetWaniKaniSourceEndpoint(IWaniKaniRepository waniKaniRepository)
    {
        _waniKaniRepository = waniKaniRepository;
    }

    public async Task<IResult> HandleAsync(GetWaniKaniSourceRequest request)
    {
        var source = await _waniKaniRepository.GetAsync(request.Value);

        if (source == null)
            return Results.NotFound();

        var response = source.ToResponse();
        return Results.Ok(response);
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/wanikani", (GetWaniKaniSourceEndpoint endpoint, [FromQuery] string value) =>
            endpoint.HandleAsync(new GetWaniKaniSourceRequest { Value = value }))
            .WithName(nameof(GetWaniKaniSourceEndpoint));
    }
}