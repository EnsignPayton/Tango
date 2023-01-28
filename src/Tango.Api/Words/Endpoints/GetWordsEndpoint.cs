using Tango.Api.Common;
using Tango.Api.Words.Repository;
using Tango.Api.Words.Responses;

namespace Tango.Api.Words.Endpoints;

public class GetWordsEndpoint : IEndpoint
{
    private readonly IWordRepository _wordRepository;

    public GetWordsEndpoint(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task<GetWordsResponse> HandleAsync()
    {
        var words = await _wordRepository.GetAllAsync();
        var response = words.ToResponse();
        return response;
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/all", (GetWordsEndpoint endpoint) => endpoint.HandleAsync())
            .WithName(nameof(GetWordsEndpoint));
    }
}