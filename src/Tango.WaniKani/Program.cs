using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

var waniKaniClient = new HttpClient();

var apiKey = Environment.GetEnvironmentVariable("WK_API_KEY");
waniKaniClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
waniKaniClient.DefaultRequestHeaders.Add("WaniKani-Revision", "20170710");

var tangoClient = new HttpClient();

try
{
    await Console.Out.WriteLineAsync("Starting WaniKani API query using key " +
                                     waniKaniClient.DefaultRequestHeaders.Authorization!.Parameter);

    // Restrict to level 1 for demo purposes
    var subjectsResponse = await waniKaniClient.GetStringAsync(
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

    await Console.Out.WriteLineAsync($"Got {subjectsMap.Count} subjects");

    var assignmentResponse = await waniKaniClient.GetStringAsync(
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

    await Console.Out.WriteLineAsync($"Got {assignmentsMap.Count} assignments");

    var sources = new List<Source>();
    foreach (var (subjectId, srsStage) in assignmentsMap)
    {
        var value = subjectsMap[subjectId];
        sources.Add(new Source
        {
            Value = value,
            SubjectId = subjectId,
            SrsStage = srsStage
        });
    }

    var request = new Request { Sources = sources };
    var requestJson = JsonSerializer.Serialize(request);
    var requestObject = new StringContent(requestJson, Encoding.UTF8, "application/json");
    await tangoClient.PostAsync("http://localhost:5161/sources/wanikani", requestObject);
}
catch (Exception ex)
{
    await Console.Error.WriteLineAsync("Error querying WaniKani: " + ex);
}

internal class Source
{
    [JsonPropertyName("value")]
    public required string Value { get; init; }

    [JsonPropertyName("subject_id")]
    public required int SubjectId { get; init; }

    [JsonPropertyName("srs_stage")]
    public required int SrsStage { get; init; }
}

internal class Request
{
    [JsonPropertyName("sources")]
    public required IEnumerable<Source> Sources { get; init; }
}
