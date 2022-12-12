namespace AdventOfCode.Utils;

public static class Cookies
{
    public static string? SessionId { get; private set; }
    public static string? UserAgent { get; private set; }

    public static void TryGetFiles()
    {
        SessionId = TryGetFileContents("session.txt",
            "Get your session id cookie from AoC's website and paste it into 'session.txt' file.");

        UserAgent = TryGetFileContents("useragent.txt",
            "Put your socials/email links in 'useragent.txt' file separated with ' | '.");

        if (string.IsNullOrEmpty(SessionId) || string.IsNullOrEmpty(UserAgent))
            throw new Exception("Could not initialize SessionId and UserAgent.");
    }

    private static string? TryGetFileContents(string filename, string message)
    {
        try
        {
            var contents = File.ReadAllText(filename).Trim();

            if (contents == string.Empty)
                throw new Exception($"File '{filename}' is empty");

            return contents;
        }
        catch (Exception e)
        {
            Console.Out.WriteLine(e.Message);
            Console.Out.WriteLine(message);
        }

        return null;
    }
}
