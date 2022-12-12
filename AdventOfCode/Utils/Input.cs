using System.Net;
#pragma warning disable SYSLIB0014

namespace AdventOfCode.Utils;

public sealed class Input
{
    private const string InputBaseUrl = "https://adventofcode.com/2022/day";
    private const string InputSuffixUrl = "input";

    private readonly string _input;

    public Input(int day)
    {
        _input = Download(day);
    }

    public IEnumerable<string> Lines(StringSplitOptions splitOptions = StringSplitOptions.RemoveEmptyEntries)
    {
        return _input.Split(new[] { '\r', '\n' }, splitOptions)
            .AsEnumerable();
    }

    private static string Download(int day)
    {
        var cacheFile = $"input_day_{day}.txt";

        string output;

        if (File.Exists(cacheFile))
        {
            output = File.ReadAllText(cacheFile);
        }
        else
        {
            Console.Out.WriteLine($"Downloading input file for day {day}");

            var client = new WebClient();
            client.Headers.Add(HttpRequestHeader.UserAgent, $"{Cookies.UserAgent}");
            client.Headers.Add(HttpRequestHeader.Cookie, $"session={Cookies.SessionId}");

            output = client.DownloadString($"{InputBaseUrl}/{day}/{InputSuffixUrl}");
            File.WriteAllText(cacheFile, output);
        }

        return output;
    }
}
