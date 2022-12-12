using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day11;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(11)
    { }

    protected override void Part1()
    {
        var monkeys = Input.Lines()
            .Chunk(6).Select(m => new Monkey(m, true)).ToList();

        SimulateMonkeys(ref monkeys, 20);

        Print(2, $"{monkeys[0].Inspections * monkeys[1].Inspections}");
    }

    protected override void Part2()
    {
        var monkeys = Input.Lines()
            .Chunk(6).Select(m => new Monkey(m, false)).ToList();

        SimulateMonkeys(ref monkeys, 10_000);

        Print(2, $"{monkeys[0].Inspections * monkeys[1].Inspections}");
    }

    private static void SimulateMonkeys(ref List<Monkey> monkeys, int rounds)
    {
        var divisorLimit = monkeys.Aggregate(1, (c, m) => c * m.Test);

        for (var i = 0; i < rounds; i++)
            foreach (var monkey in monkeys)
            foreach (var (to, item) in monkey.Throw(divisorLimit))
                monkeys[to].CatchItem(item);

        monkeys.Sort((a, b) => b.Inspections.CompareTo(a.Inspections));
    }
}
