using System.Text.Json;
using Spectre.Console;

namespace QuickCapacities;

public class ConfigurationService
{
    private static readonly string ConfigFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".capacities-cli.json");


    private static Dictionary<string, string> GetConfig()
    {
        Dictionary<string, string>? configData =
            File.Exists(ConfigFilePath)
                ? JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(ConfigFilePath))
                : null;

        return configData ?? new Dictionary<string, string>(
        )
        {
            { "api_key", "" },
            { "default_spaces_id", "" }
        };
    }

    public static string GetDefaultSpacesId()
    {
        string id = GetConfig()["default_spaces_id"];
        if (id == "")
        {
            throw new Exception("Default Spaces Id not set");
        }

        return id;
    }

    public static string GetApiKey()
    {
        string key = GetConfig()["api_key"];
        if (key == "")
        {
            throw new Exception("Api Key not set");
        }

        return key;
    }

    public static void DisplayConfig()
    {
        Table table = new();
        table.AddColumn("Setting");
        table.AddColumn("Value");
        foreach ((string key, string value) in GetConfig())
        {
            table.AddRow(key, value == "" ? "NOT SET" : value);
        }

        AnsiConsole.Write(table);
    }

    private static void WriteConfig(Dictionary<string, string> config)
    {
        string json = JsonSerializer.Serialize(config);
        File.WriteAllText(ConfigFilePath, json);
    }

    public static void SetConfigApiKey(string key)
    {
        Dictionary<string, string> config = GetConfig();
        config["api_key"] = key;
        WriteConfig(config);
    }

    public static void SetConfigDefaultSpacesId(string id)
    {
        Dictionary<string, string> config = GetConfig();
        config["default_spaces_id"] = id;
        WriteConfig(config);
    }

    public static void DisplayConfigurationPath()
    {
        AnsiConsole.WriteLine(ConfigFilePath);
    }
}