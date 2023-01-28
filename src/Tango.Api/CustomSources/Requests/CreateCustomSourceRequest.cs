using System.Text.Json.Serialization;

namespace Tango.Api.CustomSources.Requests;

public class CreateCustomSourceRequest
{
    [JsonPropertyName("value")]
    public string Value { get; init; } = default!;

    [JsonPropertyName("kf")]
    public double KnowledgeFactor { get; init; } = default!;
}