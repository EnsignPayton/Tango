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
}