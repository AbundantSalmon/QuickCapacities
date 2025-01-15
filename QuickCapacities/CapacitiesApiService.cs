using System.Text;
using System.Text.Json;
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
}