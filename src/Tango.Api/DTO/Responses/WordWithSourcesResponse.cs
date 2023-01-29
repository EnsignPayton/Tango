using System.Text.Json.Serialization;

namespace Tango.Api.DTO.Responses;

public class WordWithSourcesResponse
{
    [JsonPropertyName("word")]
    [JsonPropertyOrder(0)]
    public required WordResponse Word { get; init; }

    [JsonPropertyName("custom")]
    [JsonPropertyOrder(1)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required CustomSourceResponse? CustomSource { get; init; }
    
    [JsonPropertyName("wanikani")]
    [JsonPropertyOrder(2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required WaniKaniSourceResponse? WaniKaniSource { get; init; }
}