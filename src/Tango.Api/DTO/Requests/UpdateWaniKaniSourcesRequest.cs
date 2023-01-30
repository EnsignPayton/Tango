using System.Text.Json.Serialization;

namespace Tango.Api.DTO.Requests;

public class UpdateWaniKaniSourcesRequest
{
    [JsonPropertyName("sources")]
    public required IEnumerable<UpdateWaniKaniSourceRequest> Sources { get; init; }
}
