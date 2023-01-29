using Tango.Api.Custom.Repository;
using Tango.Api.WaniKani.Repository;
using Tango.Api.Words;
using Tango.Api.Words.Repository;

namespace Tango.Api;

public class DummyDataProvider
{
    private readonly IWordRepository _wordRepository;
    private readonly ICustomSourceRepository _customSourceRepository;
    private readonly IWaniKaniRepository _waniKaniRepository;

    public DummyDataProvider(
        IWordRepository wordRepository,
        ICustomSourceRepository customSourceRepository,
        IWaniKaniRepository waniKaniRepository)
    {
        _wordRepository = wordRepository;
        _customSourceRepository = customSourceRepository;
        _waniKaniRepository = waniKaniRepository;
    }

    public async Task ExecuteAsync()
    {
        await _wordRepository.CreateAsync(new() { Value = "本", KnowledgeFactor = 1.0 });
        await _wordRepository.CreateAsync(new() { Value = "日", KnowledgeFactor = 1.0 });
        await _wordRepository.CreateAsync(new() { Value = "日本", KnowledgeFactor = 0.8 });
        await _customSourceRepository.CreateAsync(new() { Value = "本", KnowledgeFactor = 1.0 });
        await _customSourceRepository.CreateAsync(new() { Value = "日", KnowledgeFactor = 1.0 });
        await _customSourceRepository.CreateAsync(new() { Value = "日本", KnowledgeFactor = 0.8 });

        await _wordRepository.CreateAsync(new() { Value = "本日", KnowledgeFactor = 0.5 });
        await _waniKaniRepository.CreateAsync(new() { Value = "本日", SrsStage = 6, SubjectId = 100 });
        await _waniKaniRepository.CreateAsync(new() { Value = "日本", SrsStage = 4, SubjectId = 101 });
    }
}