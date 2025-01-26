using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Spectre.Console;

namespace QuickCapacities;

public class CapacitiesApiService
{
    public static async Task<int> SendDailyNote(string note, string spacesId)
    {
        const string url = "https://api.capacities.io/save-to-daily-note";

        var data = new
        {
            mdText = note,
            spaceId = spacesId
        };

        string json = JsonSerializer.Serialize(data);
        using HttpClient client = new();
        try
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ConfigurationService.GetApiKey());
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
            return 0;
        }
        catch (HttpRequestException e)
        {
            AnsiConsole.WriteLine($"Request error: {e.Message}");
            return 1;
        }
    }

    public static async Task<int> SendWeblink(string link, string spacesId)
    {
        const string url = "https://api.capacities.io/save-weblink";

        var data = new
        {
            url = link,
            spaceId = spacesId
        };

        string json = JsonSerializer.Serialize(data);
        using HttpClient client = new();
        try
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ConfigurationService.GetApiKey());
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);

            response.EnsureSuccessStatusCode();
            return 0;
        }
        catch (HttpRequestException e)
        {
            AnsiConsole.WriteLine($"Request error: {e.Message}");
            return 1;
        }
    }

    public static async Task<int> Search(string phrase, string spacesId)
    {
        const string url = "https://api.capacities.io/search";

        var data = new
        {
            mode = "fullText",
            spaceIds = (List<string>) [spacesId],
            searchTerm = phrase
        };

        string json = JsonSerializer.Serialize(data);
        using HttpClient client = new();
        try
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ConfigurationService.GetApiKey());
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            // AnsiConsole.WriteLine(PrettyJson(response.Content.ReadAsStringAsync().Result));
            SearchResponse? responseData =
                JsonSerializer.Deserialize<SearchResponse>(response.Content.ReadAsStringAsync().Result);
            if (responseData is null)
            {
                return 1;
            }

            responseData.Print();
            return 0;
        }
        catch (HttpRequestException e)
        {
            AnsiConsole.WriteLine($"Request error: {e.Message}");
            return 1;
        }
    }

    private static string PrettyJson(string unPrettyJson)
    {
        JsonSerializerOptions options = new()
        {
            WriteIndented = true
        };

        JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

        return JsonSerializer.Serialize(jsonElement, options);
    }
}

public class SearchResult
{
    [JsonPropertyName("id")] public required string Id { get; set; }
    [JsonPropertyName("spaceId")] public required string SpaceId { get; set; }
    [JsonPropertyName("structureId")] public required string StructureId { get; set; }
    [JsonPropertyName("title")] public required string Title { get; set; }
    [JsonPropertyName("highlights")] public required List<Highlights> Highlights { get; set; }

    public void Print()
    {
        foreach (Highlights highlight in Highlights)
        {
            AnsiConsole.MarkupLine($"[bold]{Title}[/]");
            highlight.Print(Id, SpaceId);
        }
    }
}

public class Highlights
{
    [JsonPropertyName("context")] public required ResponseContext ResponseContext { get; set; }
    [JsonPropertyName("snippets")] public required List<string> Snippets { get; set; }

    public void Print(string id, string spaceId)
    {
        foreach (string snippet in Snippets)
        {
            AnsiConsole.WriteLine("\t" + snippet);
        }

        string link = "\thttps://app.capacities.io/";
        link += spaceId;
        link += "/";
        link += id;
        if (ResponseContext.BlockId is not null)
        {
            link += $"?bid={ResponseContext.BlockId}";
        }

        AnsiConsole.WriteLine(link);
    }
}

public class ResponseContext
{
    [JsonPropertyName("field")] public required string Field { get; set; }
    [JsonPropertyName("blockId")] public string? BlockId { get; set; }
}

public class SearchResponse
{
    [JsonPropertyName("results")] public required List<SearchResult> Results { get; init; }

    public void Print()
    {
        foreach (SearchResult result in Results)
        {
            result.Print();
        }
    }
}