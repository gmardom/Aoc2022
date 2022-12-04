using static System.IO.File;

namespace Day4;

public class Puzzle
{
    private readonly List<Elf> _elves = new();

    public Puzzle(string file)
    {
        foreach (var line in ReadLines(file))
            foreach (var section in line.Split(','))
                _elves.Add(new Elf(section));
    }

    public void Part1()
    {
        var sum = 0;

        for (var i = 0; i < _elves.Count; i++)
        {
            var elf1 = _elves[i];
            var elf2 = _elves[++i];

            if (elf1.FullyOverlaps(elf2) || elf2.FullyOverlaps(elf1))
                sum += 1;
        }

        Console.WriteLine(sum);
    }

    public void Part2()
    {
        var sum = 0;

        for (var i = 0; i < _elves.Count; i++)
        {
            var elf1 = _elves[i];
            var elf2 = _elves[++i];

            if (elf1.OverlapsAny(elf2) || elf2.OverlapsAny(elf1))
                sum += 1;
        }

        Console.WriteLine(sum);
    }
}
