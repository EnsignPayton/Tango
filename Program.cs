// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// 1. Grab Anki vocab cards
// 2. Group by words
// 3. Select most advanced card per word
// 4. Translate each to a knowledge factor
// 5. Display count of words with a knowledge factor over some threshold

var cards = Enumerable.Empty<dynamic>();

const float threshold = 1.0f;

var knownWordCount = cards
    .GroupBy(c => c.Word)
    .Select(w => w
        .Where(c => !c.IsSuspended)
        .MaxBy(c => c.Interval)?
        .Interval
        .ToFactor() ?? 0)
    .Count(f => f >= threshold);

Console.WriteLine($"You know {knownWordCount} words");
