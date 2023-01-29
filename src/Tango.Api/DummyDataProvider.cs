using Tango.Api.Custom.Repository;
using Tango.Api.Words.Repository;

namespace Tango.Api;

public class DummyDataProvider
{
    private readonly IWordRepository _wordRepository;
    private readonly ICustomSourceRepository _customSourceRepository;

    public DummyDataProvider(
        IWordRepository wordRepository,
        ICustomSourceRepository customSourceRepository)
    {
        _wordRepository = wordRepository;
        _customSourceRepository = customSourceRepository;
    }

    public async Task ExecuteAsync()
    {
        await _wordRepository.CreateAsync(new() { Value = "本", KnowledgeFactor = 1.0 });
        await _wordRepository.CreateAsync(new() { Value = "日", KnowledgeFactor = 1.0 });
        await _wordRepository.CreateAsync(new() { Value = "日本", KnowledgeFactor = 0.8 });
        await _customSourceRepository.CreateAsync(new() { Value = "本", KnowledgeFactor = 1.0 });
        await _customSourceRepository.CreateAsync(new() { Value = "日", KnowledgeFactor = 1.0 });
        await _customSourceRepository.CreateAsync(new() { Value = "日本", KnowledgeFactor = 0.8 });
    }
}