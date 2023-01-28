using System.Text.Json.Serialization;

namespace Tango.Api.CustomSources.Responses;

public class CustomSourceResponse
{
    [JsonPropertyName("value")]
    [JsonPropertyOrder(0)]
    public string Value { get; init; } = default!;

    [JsonPropertyName("kf")]
    [JsonPropertyOrder(1)]
    public double KnowledgeFactor { get; init; }
}