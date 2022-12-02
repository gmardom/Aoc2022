using static Day2.RockPaperScissors;
using static System.IO.File;

namespace Day2;

public class Puzzle
{
    private readonly List<string> _lines = new();

    public Puzzle(string file)
    {
        foreach (var line in ReadLines(file))
            _lines.Add(line);
    }

    public void Part1()
    {
        var points = 0;

        foreach (var line in _lines)
        {
            var foes = ReadMove(line[0]);
            var mine = ReadMove(line[2]);

            points += CalculatePoints(foes, mine);
        }

        Console.WriteLine(points);
    }

    public void Part2()
    {
        var points = 0;

        foreach (var line in _lines)
        {
            var foes = ReadMove(line[0]);
            var mine = MakeMove(foes, line[2]);

            points += CalculatePoints(foes, mine);
        }

        Console.WriteLine(points);
    }
}
