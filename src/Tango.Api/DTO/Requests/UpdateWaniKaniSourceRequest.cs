using System.Text.Json.Serialization;

namespace Tango.Api.DTO.Requests;

public class UpdateWaniKaniSourceRequest
{
    [JsonPropertyName("value")]
    public required string Value { get; init; }

    [JsonPropertyName("subject_id")]
    public required int SubjectId { get; init; }

    [JsonPropertyName("srs_stage")]
    public required int SrsStage { get; init; }
}
