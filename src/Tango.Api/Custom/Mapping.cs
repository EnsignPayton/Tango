using Tango.Api.Custom.Responses;

namespace Tango.Api.Custom;

public static class Mapping
{
    public static CustomSourceResponse ToResponse(this CustomSource source) => new()
    {
        Value = source.Value,
        KnowledgeFactor = source.KnowledgeFactor
    };
}