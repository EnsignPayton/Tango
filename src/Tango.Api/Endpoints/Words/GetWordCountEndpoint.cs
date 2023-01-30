using Microsoft.AspNetCore.Mvc;
using Tango.Api.DTO.Responses;
using Tango.Api.Repository;

namespace Tango.Api.Endpoints.Words;

public class GetWordCountEndpoint : IEndpoint
{
    private readonly IWordRepository _wordRepository;

    public GetWordCountEndpoint(IWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task<WordCountResponse> HandleAsync(double min)
    {
        var count = await _wordRepository.GetCountAsync(min);
        var response = new WordCountResponse
        {
            Count = count
        };
        return response;
    }

    public static void Register(IEndpointRouteBuilder app)
    {
        app.MapGet("/words/count", (GetWordCountEndpoint endpoint, [FromQuery] double? min) =>
            endpoint.HandleAsync(min ?? 0))
            .WithName(nameof(GetWordCountEndpoint));
    }
}