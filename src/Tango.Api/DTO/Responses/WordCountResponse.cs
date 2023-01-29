using System.Text.Json.Serialization;

namespace Tango.Api.DTO.Responses;

public class WordCountResponse
{
    [JsonPropertyName("count")]
    public int Count { get; init; }
}