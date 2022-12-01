using static System.IO.File;
using static System.Convert;

namespace Day1;

public class Puzzle
{
    private readonly List<int> _elves = new();

    public Puzzle(string file)
    {
        var sumOfCalories = 0;

        foreach (var line in ReadLines(file))
        {
            if (line == string.Empty)
            {
                _elves.Add(sumOfCalories);
                sumOfCalories = 0;
                continue;
            }

            sumOfCalories += ToInt32(line);
        }

        _elves.Sort();
        _elves.Reverse();
    }

    public void Part1()
    {
        Console.WriteLine("Elf with most calories: {0}", _elves[0]);
    }

    public void Part2()
    {
        var sumOfTopThreeElves = _elves[0] + _elves[1] + _elves[2];
        Console.WriteLine("Top 3 elves calories total: {0}", sumOfTopThreeElves);
    }
}