using Tango.Api.Common;
using Tango.Api.Words.Repository;
using Tango.Api.Words.Requests;

namespace Tango.Api.Words.Endpoints;

public class GetWordEndpoint : IEndpoint
{
    private readonly IWordRepository _wordRepository;

    public GetWordEndpoint(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task<IResult> HandleAsync(GetWordRequest request)
    {
        var word = await _wordRepository.GetAsync(request.Value);

        if (word == null)
            return Results.NotFound();

        var response = word.ToResponse();
        return Results.Ok(response);
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words", (GetWordsEndpoint endpoint) => endpoint.HandleAsync());
    }
}