using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day5;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(5)
    { }

    protected override void Part1()
    {
        var procedures = Procedure.Parse(Input.Lines().Where(l => l.StartsWith("move"))).ToList();
        var crateStacks = CrateStack.Parse(Input.Lines().Where(l => !l.StartsWith("move")).ToArray()).ToList();

        foreach (var procedure in procedures)
            Procedure.Execute(procedure, ref crateStacks, false);

        Print(1, crateStacks.Select(x => x.Top).AsString());
    }

    protected override void Part2()
    {
        var procedures = Procedure.Parse(Input.Lines().Where(l => l.StartsWith("move"))).ToList();
        var crateStacks = CrateStack.Parse(Input.Lines().Where(l => !l.StartsWith("move")).ToArray()).ToList();

        foreach (var procedure in procedures)
            Procedure.Execute(procedure, ref crateStacks, true);

        Print(2, crateStacks.Select(x => x.Top).AsString());
    }
}
