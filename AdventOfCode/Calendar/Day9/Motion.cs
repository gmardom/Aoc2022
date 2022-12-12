namespace AdventOfCode.Calendar.Day9;

public sealed record Motion(Direction Direction, int Count)
{
    public static List<Motion> Parse(IEnumerable<string> input) =>
        input.Select(ParseLine).ToList();

    private static Motion ParseLine(string line)
    {
        var segments = line.Split(' ');

        if (segments.Length != 2)
            throw new FormatException($"Expected direction character and number of steps separated by space. Got: \"{line}\"");

        var direction = segments[0] switch
        {
            "U" => Direction.Up,
            "D" => Direction.Down,
            "L" => Direction.Left,
            "R" => Direction.Right,
            _ => throw new Exception($"Not a valid direction: {segments[0]}")
        };
        var count = int.Parse(segments[1]);


        return new Motion(direction, count);
    }
}
