using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day4;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(4)
    { }

    protected override void Part1()
    {
        var elfPairs = GetElfPairs();

        var sum = 0;
        foreach (var (elf1, elf2) in elfPairs)
        {
            if (elf1.FullyOverlaps(elf2) || elf2.FullyOverlaps(elf1))
                sum += 1;
        }

        Print(1, sum);
    }

    protected override void Part2()
    {
        var elfPairs = GetElfPairs();

        var sum = 0;
        foreach (var (elf1, elf2) in elfPairs)
        {
            if (elf1.OverlapsAny(elf2) || elf2.OverlapsAny(elf1))
                sum += 1;
        }

        Print(2, sum);
    }

    private IEnumerable<ValueTuple<Elf, Elf>> GetElfPairs()
    {
        return Input.Lines()
            .Select(line => line.Split(","))
            .Select(sections => new ValueTuple<Elf, Elf>(
                new Elf(sections[0]),
                new Elf(sections[1])))
            .ToList();
    }
}
