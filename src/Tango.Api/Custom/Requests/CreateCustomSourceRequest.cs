using System.Text.Json.Serialization;

namespace Tango.Api.Custom.Requests;

public class CreateCustomSourceRequest
{
    [JsonPropertyName("value")]
    public required string Value { get; init; }

    [JsonPropertyName("kf")]
    public required double KnowledgeFactor { get; init; }
}