using Microsoft.Extensions.Configuration;
using Spectre.Console.Cli;

namespace CapacitiesCli;

public static class Program
{
    public static int Main(string[] args)
    {
        
        CommandApp app = new();
        app.Configure(config =>
        {
            config.AddCommand<AddDailyNoteCommand>("daily");
            config.AddCommand<AddWeblinkCommand>("weblink");
            config.AddCommand<ShowConfigCommand>("show-config");
            config.AddCommand<SetConfigCommand>("set-config");
        });
        return app.Run(args);
    }
}