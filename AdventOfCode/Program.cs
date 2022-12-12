using AdventOfCode.Calendar;
using AdventOfCode.Utils;

namespace AdventOfCode;

// TODO: Add more comments to this project.
internal static class Program
{
    private static int Day { get; set; }
    private static bool All { get; set; }
    private static bool Benchmark { get; set; }

    private static void Main(string[] args)
    {
        Cookies.TryGetFiles();

        ParseArgs(args);
        CheckDay();

        Execute();
    }

    private static void Execute()
    {
        var manager = new Manager();

        if (Benchmark)
        {
            if (All) manager.BenchmarkAll();
            else     manager.Benchmark(Day);
        }
        else
        {
            if (All) manager.ExecuteAll();
            else     manager.Execute(Day);
        }
    }

    private static void ParseArgs(IReadOnlyList<string> args)
    {
        for (var i = 0; i < args.Count; i++)
        {
            if (args[i] is ("-d" or "--day"))
            {
                var value = args[++i];
                Day = value is not ("current" or "c") ? int.Parse(value) : Day;
            }

            if (args[i] is ("-b" or "--benchmark"))
                Benchmark = true;

            if (args[i] is ("-a" or "--all"))
                All = true;
        }
    }

    private static void CheckDay()
    {
        // Get current EST timezone date since AoC's day starts at 6AM my time.
        var estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        var estTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, estZone);

        // Run day one solution if month is not december.
        if (estTime.Month != 12) Day = 1;

        // Use current EST day if day parameter is not provided.
        if (Day == 0) Day = estTime.Day;
    }
}

