using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace QuickCapacities;

public class SearchCommand : AsyncCommand<SearchCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Phrase, enclose in quotes for phrase with spaces")]
        [CommandArgument(0, "[phrase]")]
        public required string? Phrase { get; init; }

        [CommandOption("-s|--spaces-id")] public string? SpacesId { get; init; }
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        string phrase = "";
        phrase = settings.Phrase ?? AnsiConsole.Prompt(new TextPrompt<string>("Phrase: "));

        return CapacitiesApiService.Search(phrase,
            settings.SpacesId ?? ConfigurationService.GetDefaultSpacesId());
    }
}