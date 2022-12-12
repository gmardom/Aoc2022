using AdventOfCode.Utils;
using static AdventOfCode.Calendar.Day2.RockPaperScissors;

namespace AdventOfCode.Calendar.Day2;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(2)
    { }

    protected override void Part1()
    {
        var points = 0;

        foreach (var line in Input.Lines())
        {
            var foes = ReadMove(line[0]);
            var mine = ReadMove(line[2]);

            points += CalculatePoints(foes, mine);
        }

        Print(1, points);
    }

    protected override void Part2()
    {
        var points = 0;

        foreach (var line in Input.Lines())
        {
            var foes = ReadMove(line[0]);
            var mine = MakeMove(foes, line[2]);

            points += CalculatePoints(foes, mine);
        }

        Print(2, points);
    }
}
