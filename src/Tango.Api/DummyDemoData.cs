using Microsoft.AspNetCore.Mvc;

namespace Tango.Api;

public static class DummyDemoData
{
    private static readonly List<Word> _words = new()
    {
        new("one", 1.0f),
        new("two", 0.8f),
        new("three", 0.5f),
        new("four", 0.2f),
        new("five", 0.0f),
    };

    public static void MapDummyDemoData(this IEndpointRouteBuilder app)
    {
        app.MapGet("/words", () => _words);

        app.MapGet("/words/count", ([FromQuery] float? threshold) => _words
            .Count(x => x.Factor >= (threshold ?? 0.0f)));

        app.MapPost("/words/custom", (Word value) =>
        {
            _words.Add(value);
        });

        app.MapDelete("/words", ([FromQuery] string value) =>
        {
            var entry = _words.FirstOrDefault(x => x.Value == value);
            if (entry == null) return;
            _words.Remove(entry);
        });
    }

    private record Word(string Value, float Factor);
}
