using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day5;

public class CrateStack
{
    private readonly Stack<char> _crates = new();

    public char Top => _crates.Peek();
    public int Count => _crates.Count;

    public List<char> Pop(int count = 1)
    {
        var crates = new List<char>();

        for (var i = 0; i < count; i++)
            crates.Add(_crates.Pop());

        crates.Reverse();
        return crates;
    }

    public void Push(char crate)
    {
        _crates.Push(crate);
    }

    public void Push(List<char> crates)
    {
        foreach (var crate in crates) _crates.Push(crate);
    }

    public static IEnumerable<CrateStack> Parse(string[] input)
    {
        var count = input.ToArray()[^1].Trim().Split("   ").Length;
        var lines = input.Range(0, ^1).ToArray();

        var stacks = new List<CrateStack>(count);
        for (var i = 0; i < count; i++)
            stacks.Add(new CrateStack());

        for (var i = lines.Length - 1; i >= 0; i--)
            ParseLine(lines[i], ref stacks);

        return stacks;
    }

    private static void ParseLine(string line, ref List<CrateStack> stacks)
    {
        foreach (var stack in stacks)
        {
            var crate = line[..3];
            line = line.Remove(0, 3);

            if (crate[0] == '[')
                stack.Push(crate[1]);

            if (line.Length != 0)
                line = line.Remove(0, 1);
        }
    }
}
