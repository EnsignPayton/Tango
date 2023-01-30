using Tango.Api.DTO.Responses;
using Tango.Api.Mapping;
using Tango.Api.Repository;

namespace Tango.Api.Endpoints.Words;

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