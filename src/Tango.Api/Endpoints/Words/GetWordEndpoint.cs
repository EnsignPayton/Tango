using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.DTO.Responses;
using Tango.Api.Mapping;
using Tango.Api.Repository;

namespace Tango.Api.Endpoints.Words;

public class GetWordEndpoint : IEndpoint
{
    private readonly IWordRepository _wordRepository;
    private readonly ICustomSourceRepository _customSourceRepository;
    private readonly IWaniKaniRepository _waniKaniRepository;

    public GetWordEndpoint(
        IWordRepository wordRepository,
        ICustomSourceRepository customSourceRepository,
        IWaniKaniRepository waniKaniRepository)
    {
        _wordRepository = wordRepository;
        _customSourceRepository = customSourceRepository;
        _waniKaniRepository = waniKaniRepository;
    }

    public async Task<IResult> HandleAsync(string value)
    {
        var word = await _wordRepository.GetAsync(value);

        if (word == null)
            return Results.NotFound();

        var customSource = await _customSourceRepository.GetAsync(value);
        var waniKaniSource = await _waniKaniRepository.GetAsync(value);

        var response = new WordWithSourcesResponse
        {
            Word = word.ToResponse(),
            CustomSource = customSource?.ToResponse(),
            WaniKaniSource = waniKaniSource?.ToResponse()
        };

        return Results.Ok(response);
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words", (GetWordEndpoint endpoint, [FromQuery] string value) =>
            endpoint.HandleAsync(value))
            .WithName(nameof(GetWordEndpoint));
    }
}