using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day12;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(12)
    { }

    protected override void Part1()
    {
        var terrain = Input.Lines().Select(l => l.Select(x => new Location(x)).ToArray()).ToArray();
        var path = WayPoint.Navigate(terrain, 'S', 'E', (f, t) => f - t >= -1);

        Print(1, path.Seen().Count());
    }

    protected override void Part2()
    {
        var terrain = Input.Lines().Select(l => l.Select(x => new Location(x)).ToArray()).ToArray();
        var path = WayPoint.Navigate(terrain, 'E', 'a', (f, t) => f - t <= 1);

        Print(2, path.Seen().Count());
    }
}
