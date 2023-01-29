using System.Text.Json.Serialization;

namespace Tango.Api.DTO.Responses;

public class GetWordsResponse
{
    [JsonPropertyName("words")]
    public required IEnumerable<WordResponse> Words { get; init; }
}