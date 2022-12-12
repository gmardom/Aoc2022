namespace AdventOfCode.Calendar.Day5;

public record Procedure(int Count, int From, int To)
{
    public static IEnumerable<Procedure> Parse(IEnumerable<string> input)
    {
        return input.Select(ParseSingle);
    }

    private static Procedure ParseSingle(string line)
    {
        var parts = line.Split(" ").ToArray();
        return new Procedure(
            int.Parse(parts[1]),
            int.Parse(parts[3]) - 1,
            int.Parse(parts[5]) - 1);
    }

    public static void Execute(Procedure procedure, ref List<CrateStack> stacks, bool multipleAtOnce)
    {
        if (multipleAtOnce)
        {
            stacks[procedure.To].Push(stacks[procedure.From].Pop(procedure.Count));
            return;
        }

        for (var i = 0; i < procedure.Count; i++)
        {
            stacks[procedure.To].Push(stacks[procedure.From].Pop());
        }
    }
}
