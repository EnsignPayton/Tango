using System.Text.Json.Serialization;

namespace Tango.Api.Words.Responses;

public class GetWordsResponse
{
    [JsonPropertyName("words")]
    public required IEnumerable<WordResponse> Words { get; init; }
}