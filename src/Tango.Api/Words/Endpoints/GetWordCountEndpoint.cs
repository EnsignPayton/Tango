using Microsoft.AspNetCore.Mvc;
using Tango.Api.Common;
using Tango.Api.Words.Repository;
using Tango.Api.Words.Requests;
using Tango.Api.Words.Responses;

namespace Tango.Api.Words.Endpoints;

public class GetWordCountEndpoint : IEndpoint
{
    private readonly IWordRepository _wordRepository;

    public GetWordCountEndpoint(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task<WordCountResponse> HandleAsync(GetWordCountRequest request)
    {
        var count = await _wordRepository.GetCountAsync(request.MinimumKnowledgeFactor);
        var response = new WordCountResponse
        {
            Count = count
        };
        return response;
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/count", (GetWordCountEndpoint endpoint, [FromQuery] double? min) =>
            endpoint.HandleAsync(new GetWordCountRequest { MinimumKnowledgeFactor = min ?? 0 }))
            .WithName(nameof(GetWordCountEndpoint));
    }
}