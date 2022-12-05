using static System.IO.File;

namespace Day5;

public class Puzzle
{
    private List<CrateStack> _stacksPart1 = new();
    private List<CrateStack> _stacksPart2 = new();
    private readonly List<string> _procedures = new();

    public Puzzle(string file)
    {
        var lines = new List<string>();

        foreach (var line in ReadLines(file))
        {
            ParseInput(line, ref lines);
        }

        var stackCount = lines[^1].Split("   ").Length;

        for (var i = 0; i < stackCount; i++)
        {
            _stacksPart1.Add(new CrateStack());
            _stacksPart2.Add(new CrateStack());
        }

        for (var i = lines.Count - 2; i >= 0; i--)
        {
            ParseCrates(stackCount, lines[i]);
        }
    }

    private void ParseInput(string line, ref List<string> stacksLines)
    {
        if (line.StartsWith("move"))
        {
            _procedures.Add(line);
            return;
        }

        if (line != string.Empty)
        {
            stacksLines.Add(line);
        }
    }

    private void ParseCrates(int stackCount, string line)
    {
        for (var s = 0; s < stackCount; s++)
        {
            var crate = line[..3];
            line = line.Remove(0, 3);

            if (crate[0] == '[')
            {
                _stacksPart1[s].Push(crate[1]);
                _stacksPart2[s].Push(crate[1]);
            }

            if (line.Length != 0)
                line = line.Remove(0, 1);
        }
    }

    public void Part1()
    {
        foreach (var procedure in _procedures)
            ExecuteProcedure(procedure, ref _stacksPart1, false);

        foreach (var stack in _stacksPart1)
            Console.Write(stack.Top);
        Console.Write("\n");
    }

    public void Part2()
    {
        foreach (var procedure in _procedures)
            ExecuteProcedure(procedure, ref _stacksPart2, true);

        foreach (var stack in _stacksPart2)
            Console.Write(stack.Top);
        Console.Write("\n");
    }

    private static void ExecuteProcedure(string procedure, ref List<CrateStack> stacks, bool multipleAtOnce)
    {
        var words = procedure.Split(" ");

        var move = int.Parse(words[1]);
        var from = int.Parse(words[3]) - 1;
        var to   = int.Parse(words[5]) - 1;

        if (multipleAtOnce)
        {
            stacks[to].Push(stacks[from].Pop(move));
            return;
        }

        for (var i = 0; i < move; i++)
        {
            stacks[to].Push(stacks[from].Pop());
        }
    }
}
