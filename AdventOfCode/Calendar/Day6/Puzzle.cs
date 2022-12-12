using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day6;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(6)
    { }

    protected override void Part1()
    {
        Print(1, FindStartOfAPacket(Input.Lines().ToArray()[0], 4));
    }

    protected override void Part2()
    {
        Print(2, FindStartOfAPacket(Input.Lines().ToArray()[0], 14));
    }

    private static int FindStartOfAPacket(string input, int signalLength)
    {
        return Enumerable.Range(0, input.Length)
                   .First(i => input
                       .Skip(i)
                       .Take(signalLength)
                       .ToHashSet().Count == signalLength)
               + signalLength;
    }
}
