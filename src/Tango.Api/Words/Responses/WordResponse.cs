using System.Text.Json.Serialization;

namespace Tango.Api.Words.Responses;

public class WordResponse
{
    [JsonPropertyName("value")]
    [JsonPropertyOrder(0)]
    public required string Value { get; init; }

    [JsonPropertyName("kf")]
    [JsonPropertyOrder(1)]
    public required double KnowledgeFactor { get; init; }
}