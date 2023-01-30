using Tango.Api.Entities;
using Tango.Api.Repository;

namespace Tango.Api.Services;

public class WordService
{
    private readonly IWordRepository _wordRepository;
    private readonly ICustomSourceRepository _customSourceRepository;
    private readonly IWaniKaniRepository _waniKaniRepository;

    public WordService(
        IWordRepository wordRepository,
        ICustomSourceRepository customSourceRepository,
        IWaniKaniRepository waniKaniRepository)
    {
        _wordRepository = wordRepository;
        _customSourceRepository = customSourceRepository;
        _waniKaniRepository = waniKaniRepository;
    }

    public async Task UpdateWordAsync(string value)
    {
        var word = await _wordRepository.GetAsync(value);
        var customSource = await _customSourceRepository.GetAsync(value);
        var waniKaniSource = await _waniKaniRepository.GetAsync(value);

        var customFactor = customSource != null ? GetKnowledgeFactor(customSource) : 0.0;
        var waniKaniFactor = waniKaniSource != null ? GetKnowledgeFactor(waniKaniSource) : 0.0;

        var knowledgeFactor = Math.Max(customFactor, waniKaniFactor);

        if (knowledgeFactor == 0.0 && word is not null)
        {
            // No factors, remove the word
            await _wordRepository.DeleteAsync(value);
        }
        else
        {
            var newWord = new Word
            {
                Value = value,
                KnowledgeFactor = knowledgeFactor
            };

            if (word is not null)
                await _wordRepository.UpdateAsync(newWord);
            else
                await _wordRepository.CreateAsync(newWord);
        }
    }

    private static double GetKnowledgeFactor(CustomSource source) => source.KnowledgeFactor;

    private static double GetKnowledgeFactor(WaniKaniSource source) => source.SrsStage switch
    {
        1 => 0.05,
        2 => 0.1,
        3 => 0.15,
        4 => 0.2,
        5 => 0.35,
        6 => 0.5,
        7 => 0.7,
        8 => 0.9,
        9 => 1.0,
        _ => 0.0
    };
}
