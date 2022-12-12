using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day9;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(9)
    { }

    protected override void Part1()
    {
        var rope = new Rope(2);
        var moves = Motion.Parse(Input.Lines());

        foreach (var move in moves)
            rope.PerformMotion(move);

        Print(1, rope.UniqueTailPositions.Count);
    }

    protected override void Part2()
    {
        var rope = new Rope(10);
        var moves = Motion.Parse(Input.Lines());

        foreach (var move in moves)
            rope.PerformMotion(move);

        Print(2, rope.UniqueTailPositions.Count);
    }
}
