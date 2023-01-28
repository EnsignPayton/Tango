using Tango.Api.Words.Responses;

namespace Tango.Api.Words;

public static class Mapping
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