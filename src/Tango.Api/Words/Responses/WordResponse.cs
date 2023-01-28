using System.Text.Json.Serialization;

namespace Tango.Api.Words.Responses;

public class WordResponse
{
    [JsonPropertyName("value")]
    [JsonPropertyOrder(0)]
    public string Value { get; init; } = default!;
    
    [JsonPropertyName("kf")]
    [JsonPropertyOrder(1)]
    public double KnowledgeFactor { get; init; }
}