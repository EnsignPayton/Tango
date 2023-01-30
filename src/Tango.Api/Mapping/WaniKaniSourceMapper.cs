using Tango.Api.DTO.Requests;
using Tango.Api.DTO.Responses;
using Tango.Api.Entities;

namespace Tango.Api.Mapping;

public static class WaniKaniSourceMapper
{
    public static WaniKaniSourceResponse ToResponse(this WaniKaniSource source) => new()
    {
        Value = source.Value,
        SrsStage = source.SrsStage
    };

    public static WaniKaniSource ToEntity(this UpdateWaniKaniSourceRequest request) => new()
    {
        Value = request.Value,
        SubjectId = request.SubjectId,
        SrsStage = request.SrsStage
    };
}
