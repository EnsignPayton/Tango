using System.Text.Json.Serialization;

namespace Tango.Api.WaniKani.Responses;

public class WaniKaniSourceResponse
{
    [JsonPropertyName("value")]
    [JsonPropertyOrder(0)]
    public required string Value { get; init; }
    
    [JsonPropertyName("srs_stage")]
    [JsonPropertyOrder(1)]
    public required int SrsStage { get; init; }
}