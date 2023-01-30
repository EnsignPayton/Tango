using Microsoft.AspNetCore.Mvc;
using Tango.Api.DTO.Requests;
using Tango.Api.Mapping;
using Tango.Api.Repository;
using Tango.Api.Services;

namespace Tango.Api.Endpoints.Custom;

public class CreateCustomSourceEndpoint : IEndpoint
{
    private readonly ICustomSourceRepository _customSourceRepository;
    private readonly WordService _wordService;

    public CreateCustomSourceEndpoint(ICustomSourceRepository customSourceRepository, WordService wordService)
    {
        _customSourceRepository = customSourceRepository;
        _wordService = wordService;
    }

    public async Task<IResult> HandleAsync(CreateCustomSourceRequest request)
    {
        var existing = await _customSourceRepository.GetAsync(request.Value);

        if (existing is not null)
            return Results.Conflict();

        await _customSourceRepository.CreateAsync(request.ToEntity());

        await _wordService.UpdateWordAsync(request.Value);

        return Results.CreatedAtRoute(nameof(GetCustomSourceEndpoint), new { value = request.Value });
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapPost("/sources/custom", (CreateCustomSourceEndpoint endpoint, [FromBody] CreateCustomSourceRequest request) =>
            endpoint.HandleAsync(request))
            .WithName(nameof(CreateCustomSourceEndpoint));
    }
}
