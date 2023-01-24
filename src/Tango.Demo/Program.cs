using System.Net.Http.Headers;
using System.Text.Json;

var apiToken = args[0];
Console.WriteLine("Reading from WaniKani using API Key: " + apiToken);

using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

var assignmentsResponse = await client.GetStringAsync("https://api.wanikani.com/v2/assignments?started=true&subject_types=vocabulary");

var assignmentsJson = JsonSerializer.Deserialize<JsonDocument>(assignmentsResponse);
var count = assignmentsJson!.RootElement.GetProperty("total_count").GetInt32();

Console.WriteLine($"You know {count} words in WaniKani");

Console.ReadKey();
