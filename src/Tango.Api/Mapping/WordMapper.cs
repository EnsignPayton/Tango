using Tango.Api.DTO.Responses;
using Tango.Api.Entities;

namespace Tango.Api.Mapping;

public static class WordMapper
{
    public static WordResponse ToResponse(this Word word) => new()
    {
        Value = word.Value,
        KnowledgeFactor = word.KnowledgeFactor
    };

    public static GetWordsResponse ToResponse(this IEnumerable<Word> words) => new()
    {
        Words = words.Select(ToResponse)
    };
}