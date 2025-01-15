using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CapacitiesCli;

public class SetConfigCommand : Command<SetConfigCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Capacities API Key")]
        [CommandOption("-a|--api-key")]
        public string? ApiKey { get; init; }

        [Description("Default Spaces Id to use if non is supplied")]
        [CommandOption("-s|--default-spaces-id")]
        public string? DefaultSpacesId { get; init; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        ConfigurationService config = new();
        if (settings.ApiKey is not null)
        {
            ConfigurationService.SetConfigApiKey(settings.ApiKey);
        }
        if (settings.DefaultSpacesId is not null)
        {
            ConfigurationService.SetConfigDefaultSpacesId(settings.DefaultSpacesId);
        }

        ConfigurationService.DisplayConfig();
        return 0;
    }
}