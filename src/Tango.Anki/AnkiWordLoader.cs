using System.Text.RegularExpressions;
using AnkiConnect.Net;
using AnkiConnect.Net.Models;
using Tango.Core;

namespace Tango.Anki;

public partial class AnkiWordLoader
{
    public async Task<IEnumerable<Word>> LoadWordsAsync()
    {
        var client = new AnkiClient();
        var noteIds = await client.FindNotesAsync("note:Mining-475ae");
        var noteInfos = await client.NotesInfoAsync(new NotesParams(noteIds!));

        var result = new HashSet<string>();
        foreach (var noteInfo in noteInfos!)
        {
            var text = noteInfo.Fields["front"].Value;
            // remove everything between brackets
            text = BracketRegex().Replace(text, "");
            text = WhitespaceRegex().Replace(text, "");
            result.Add(text);
        }

        return result
            .Select(x => new Word
            {
                Value = x,
                KnowledgeFactor = 1.0
            });
    }

    [GeneratedRegex("\\[[^)]*\\]")]
    private static partial Regex BracketRegex();

    [GeneratedRegex("\\s+")]
    private static partial Regex WhitespaceRegex();
}