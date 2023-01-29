using Tango.Api.DTO.Responses;
using Tango.Api.Entities;

namespace Tango.Api.Mapping;

public static class CustomSourceMapper
{
    public static CustomSourceResponse ToResponse(this CustomSource source) => new()
    {
        Value = source.Value,
        KnowledgeFactor = source.KnowledgeFactor
    };
}