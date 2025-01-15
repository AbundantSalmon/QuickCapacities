using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CapacitiesCli;

public class AddWeblinkCommand : AsyncCommand<AddWeblinkCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Weblink")]
        [CommandArgument(0, "[link]")]
        public required string? Link { get; init; }

        [CommandOption("-s|--spaces-id")] public string? SpacesId { get; init; }
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        string link = "";
        link = settings.Link ?? AnsiConsole.Prompt(new TextPrompt<string>("Link: "));
        return CapacitiesApiService.SendWeblink(link,
            settings.SpacesId ?? ConfigurationService.GetDefaultSpacesId());
    }
}