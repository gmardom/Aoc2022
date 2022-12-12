using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day7;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(7)
    { }

    protected override void Part1()
    {
        var root = CreateNodeTree();

        var directorySizes = new List<int>();
        GetDirectorySizes(root, directorySizes);

        Print(1, directorySizes.Where(x => x <= 100_000).Sum());
    }

    protected override void Part2()
    {
        var root = CreateNodeTree();

        var directorySizes = new List<int>();
        var totalSizes = GetDirectorySizes(root, directorySizes);
        var spaceNeeded = 30_000_000 - (70_000_000 - totalSizes);

        Print(2, directorySizes.FirstOrDefault(x => x >= spaceNeeded));
    }

    private Node CreateNodeTree()
    {
        var root = new Node { Name = "/", IsDir = true };
        var currentDir = root;

        foreach (var line in Input.Lines())
        {
            if (line.StartsWith("$"))
            {
                if (line[2..].StartsWith("ls")) continue;

                currentDir = ChangeDirectory(currentDir, line);
                continue;
            }

            if (line.StartsWith("dir"))
                currentDir.AddDir(line.AsSpan());
            else
                currentDir.AddFile(line.AsSpan());
        }

        return root;
    }

    private static Node ChangeDirectory(Node currentDir, ReadOnlySpan<char> line)
    {
        var name = line[5..].ToString();

        if (name == "..")
            return currentDir.Parent;

        if (currentDir.Contents.TryGetValue(name, out var c))
            return c;

        return currentDir.Contents[name] = new Node
        {
            Name = name,
            Parent = currentDir,
            IsDir = true,
        };
    }

    private static int GetDirectorySizes(Node item, ICollection<int> dirSizes)
    {
        if (!item.IsDir) return item.Size;

        item.Size = 0;
        foreach (var i in item.Contents)
            item.Size += GetDirectorySizes(i.Value, dirSizes);

        dirSizes.Add(item.Size);
        return item.Size;
    }
}
