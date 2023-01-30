using Microsoft.AspNetCore.Mvc;
using Tango.Api.DTO.Requests;
using Tango.Api.Mapping;
using Tango.Api.Repository;
using Tango.Api.Services;

namespace Tango.Api.Endpoints.WaniKani;

public class UpdateWaniKaniSourcesEndpoint : IEndpoint
{
    private readonly IWaniKaniRepository _waniKaniRepository;
    private readonly WordService _wordService;

    public UpdateWaniKaniSourcesEndpoint(IWaniKaniRepository waniKaniRepository, WordService wordService)
    {
        _waniKaniRepository = waniKaniRepository;
        _wordService = wordService;
    }

    public async Task<IResult> HandleAsync(UpdateWaniKaniSourcesRequest request)
    {
        foreach (var source in request.Sources)
        {
            var existing = await _waniKaniRepository.GetAsync(source.Value);

            if (existing is not null)
                await _waniKaniRepository.UpdateAsync(source.ToEntity());
            else
                await _waniKaniRepository.CreateAsync(source.ToEntity());

            await _wordService.UpdateWordAsync(source.Value);
        }

        return Results.Ok();
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapPost("/sources/wanikani", (UpdateWaniKaniSourcesEndpoint endpoint, [FromBody] UpdateWaniKaniSourcesRequest request) =>
            endpoint.HandleAsync(request))
            .WithName(nameof(UpdateWaniKaniSourcesEndpoint));
    }
}
