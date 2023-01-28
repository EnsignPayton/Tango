using Tango.Api.CustomSources.Responses;

namespace Tango.Api.CustomSources;

public static class Mapping
{
    public static CustomSourceResponse ToResponse(this CustomSource customSource) => new()
    {
        Value = customSource.Value,
        KnowledgeFactor = customSource.KnowledgeFactor
    };
}