using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day1;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(1)
    { }

    protected override void Part1()
    {
        Print(1, GetElves()[0]);
    }

    protected override void Part2()
    {
        Print(2, GetElves().GetRange(0, 3).Sum());
    }

    private List<int> GetElves()
    {
        var elves = new List<int>();

        var sumOfCalories = 0;
        foreach (var line in Input.Lines(StringSplitOptions.None))
        {
            if (line == string.Empty)
            {
                elves.Add(sumOfCalories);
                sumOfCalories = 0;
                continue;
            }

            sumOfCalories += int.Parse(line);
        }

        elves.Sort();
        elves.Reverse();

        return elves;
    }
}
