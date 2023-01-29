using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Tango.Api.Entities;
using Tango.Api.Repository;

namespace Tango.Api;

// TODO: Big refactor.
public class TempWaniKaniQuery
{
    private readonly ILogger<TempWaniKaniQuery> _logger;
    private readonly HttpClient _client;
    private readonly IWaniKaniRepository _waniKaniRepository;
    private readonly IWordRepository _wordRepository;

    public TempWaniKaniQuery(
        ILogger<TempWaniKaniQuery> logger,
        IHttpClientFactory clientFactory,
        IWaniKaniRepository waniKaniRepository,
        IWordRepository wordRepository)
    {
        _logger = logger;
        _client = clientFactory.CreateClient("WaniKani");
        _waniKaniRepository = waniKaniRepository;
        _wordRepository = wordRepository;

        var apiKey = Environment.GetEnvironmentVariable("WK_API_KEY");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        _client.DefaultRequestHeaders.Add("WaniKani-Revision", "20170710");
    }

    public async Task ExecuteAsync()
    {
        try
        {
            _logger.LogInformation("Starting WaniKani API query using key {Key}",
                _client.DefaultRequestHeaders.Authorization!.Parameter);

            // Restrict to level 1 for demo purposes
            var subjectsResponse = await _client.GetStringAsync(
                "https://api.wanikani.com/v2/subjects" +
                "?types=vocabulary" +
                "&levels=1");

            var subjectsResponseJson = JsonNode.Parse(subjectsResponse)!;
            var subjectsArray = subjectsResponseJson["data"]!.AsArray();

            var subjectsMap = new Dictionary<int, string>();
            foreach (var subjectJson in subjectsArray)
            {
                var subjectId = subjectJson!["id"]!.AsValue();
                var subjectData = subjectJson["data"]!;
                var subjectSlug = subjectData["slug"]!.AsValue();

                var subjectIdInt = subjectId.GetValue<int>();
                var subjectSlugString = subjectSlug.GetValue<string>();
                subjectsMap[subjectIdInt] = subjectSlugString;
            }
            
            _logger.LogInformation("Got {Count} subjects", subjectsMap.Count);

            var assignmentResponse = await _client.GetStringAsync(
                "https://api.wanikani.com/v2/assignments" +
                "?subject_types=vocabulary" +
                "&levels=1");

            var assignmentResponseJson = JsonNode.Parse(assignmentResponse)!;
            var assignmentArray = assignmentResponseJson["data"]!.AsArray();

            var assignmentsMap = new Dictionary<int, int>();
            foreach (var assignmentJson in assignmentArray)
            {
                var assignmentData = assignmentJson!["data"]!;
                var subjectId = assignmentData["subject_id"]!.AsValue();
                var srsStage = assignmentData["srs_stage"]!.AsValue();

                var subjectIdInt = subjectId.GetValue<int>();
                var srsStageInt = srsStage.GetValue<int>();
                assignmentsMap[subjectIdInt] = srsStageInt;
            }
            
            _logger.LogInformation("Got {Count} assignments", assignmentsMap.Count);

            foreach (var (subjectId, srsStage) in assignmentsMap)
            {
                var value = subjectsMap[subjectId];
                await _waniKaniRepository.CreateAsync(new WaniKaniSource
                {
                    Value = value,
                    SubjectId = subjectId,
                    SrsStage = srsStage
                });

                await _wordRepository.CreateAsync(new Word
                {
                    Value = value,
                    KnowledgeFactor = SrsStageToKnowledgeFactor(srsStage)
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error querying WaniKani");
        }
    }

    private static double SrsStageToKnowledgeFactor(int value) => value switch
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