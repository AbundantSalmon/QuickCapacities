using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CapacitiesCli;

public class AddDailyNoteCommand : AsyncCommand<AddDailyNoteCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Note, enclose in quotes for notes with spaces")]
        [CommandArgument(0, "[note]")]
        public required string? Note { get; init; }

        [CommandOption("-s|--spaces-id")] public string? SpacesId { get; init; }
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        string note = "";
        note = settings.Note ?? AnsiConsole.Prompt(new TextPrompt<string>("Note: "));

        return CapacitiesApiService.SendDailyNote(note,
            settings.SpacesId ?? ConfigurationService.GetDefaultSpacesId());
    }
}