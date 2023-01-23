namespace Tango.Core;

public class Word
{
    /// <summary>
    /// The actual text of the word
    /// </summary>
    public required string Value { get; set; }
    
    /// <summary>
    /// How well a word is known from 0 to 1
    /// </summary>
    public required double KnowledgeFactor { get; set; }
}