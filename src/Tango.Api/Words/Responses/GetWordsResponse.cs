namespace Tango.Api.Words.Responses;

public class GetWordsResponse
{
    public IEnumerable<WordResponse> Words { get; init; } = Enumerable.Empty<WordResponse>();
}