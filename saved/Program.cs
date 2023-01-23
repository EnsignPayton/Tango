// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using AnkiConnect.Net;
using AnkiConnect.Net.Models;

Console.WriteLine("Hello, World!");

// 1. Grab Anki vocab cards
// 2. Group by words
// 3. Select most advanced card per word
// 4. Translate each to a knowledge factor
// 5. Display count of words with a knowledge factor over some threshold

// Always a single word
// Mining-475ae -> front (Mining)
// jp.takoboto -> Japanese (Takoboto)

// Sometimes a word, sometimes a whole phrase
// JLPT Tango N5 Japanese -> Expression (Premade::Tango N5)
// JLPT Tango N4 Japanese Omega -> Expression (Premade::Tango N4)
// JLPT Tango N3 Japanese Alpha -> Expression (Premade::Tango N3)

// Always a whole phrase
// subs2srs Japanese -> Expression (SRS::*)
// Japanese Morphman Manga Ankiweb -> Expression (Yotsuba)

var client = new AnkiClient();

// Lets start out by just listing off the front of each card from my mining deck.
// 1. Get all the node ids for that type
var noteIds = await client.FindNotesAsync("note:Mining-475ae");
Console.WriteLine($"Got back {noteIds?.Count} notes.");

// 2. Get all the note info
var noteInfo = await client.NotesInfoAsync(new NotesParams(noteIds!));

var words = new HashSet<string>();
foreach (var info in noteInfo!)
{
    var text = info.Fields["front"].Value;
    // remove everything between brackets
    text = Regex.Replace(text, @"\[[^)]*\]", "");
    words.Add(text);
}

Console.WriteLine($"Found {words.Count} unique words.");

File.WriteAllLines("out.txt", words);
