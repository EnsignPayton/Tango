using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.DTO.Requests;
using Tango.Api.Entities;
using Tango.Api.Repository;

namespace Tango.Api.Endpoints.Custom;

public class CreateCustomSourceEndpoint : IEndpoint
{
    private readonly ICustomSourceRepository _customSourceRepository;
    private readonly IWordRepository _wordRepository;

    public CreateCustomSourceEndpoint(ICustomSourceRepository customSourceRepository, IWordRepository wordRepository)
    {
        _customSourceRepository = customSourceRepository;
        _wordRepository = wordRepository;
    }

    public async Task<IResult> HandleAsync(CreateCustomSourceRequest request)
    {
        // TODO: Move or change
        var customSource = new CustomSource
        {
            Value = request.Value,
            KnowledgeFactor = request.KnowledgeFactor
        };

        await _customSourceRepository.CreateAsync(customSource);

        // TODO: Eventually, this will recalculate based on all sources.
        var word = new Word
        {
            Value = request.Value,
            KnowledgeFactor = request.KnowledgeFactor
        };

        await _wordRepository.CreateAsync(word);

        return Results.CreatedAtRoute(nameof(GetCustomSourceEndpoint), new { value = request.Value });
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapPost("/words/custom", (CreateCustomSourceEndpoint endpoint, [FromBody] CreateCustomSourceRequest request) =>
            endpoint.HandleAsync(request))
            .WithName(nameof(CreateCustomSourceEndpoint));
    }
}