using Spectre.Console.Cli;

namespace QuickCapacities;

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
            config.AddCommand<SearchCommand>("search");
        });
        return app.Run(args);
    }
}