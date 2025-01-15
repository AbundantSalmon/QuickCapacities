using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

namespace QuickCapacities;

public class ShowConfigCommand : Command<ShowConfigCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        ConfigurationService configurationService = new();
        ConfigurationService.DisplayConfigurationPath();
        ConfigurationService.DisplayConfig();
        return 0;
    }
}