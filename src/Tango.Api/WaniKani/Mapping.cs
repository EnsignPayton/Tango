using Tango.Api.WaniKani.Responses;

namespace Tango.Api.WaniKani;

public static class Mapping
{
    public static WaniKaniSourceResponse ToResponse(this WaniKaniSource source) => new()
    {
        Value = source.Value,
        SrsStage = source.SrsStage
    };
}