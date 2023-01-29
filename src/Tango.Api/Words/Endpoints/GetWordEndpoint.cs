using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.Custom;
using Tango.Api.Custom.Repository;
using Tango.Api.WaniKani;
using Tango.Api.WaniKani.Repository;
using Tango.Api.Words.Repository;
using Tango.Api.Words.Requests;
using Tango.Api.Words.Responses;

namespace Tango.Api.Words.Endpoints;

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

    public async Task<IResult> HandleAsync(GetWordRequest request)
    {
        var word = await _wordRepository.GetAsync(request.Value);

        if (word == null)
            return Results.NotFound();

        var customSource = await _customSourceRepository.GetAsync(request.Value);
        var waniKaniSource = await _waniKaniRepository.GetAsync(request.Value);

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
            endpoint.HandleAsync(new GetWordRequest { Value = value }))
            .WithName(nameof(GetWordEndpoint));
    }
}