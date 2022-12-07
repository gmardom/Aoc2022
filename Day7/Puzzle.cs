using static System.IO.File;

namespace Day7;

public class Puzzle
{
    private readonly Node _root;

    public Puzzle(string file)
    {
        _root = new Node { Name = "/", IsDir = true };
        var currentDir = _root;

        foreach (var line in ReadLines(file))
        {
            if (line.StartsWith("$"))
            {
                if (line[2..].StartsWith("ls")) continue;

                currentDir = ChangeDirectory(currentDir, line);
                continue;
            }

            if (line.StartsWith("dir"))
                currentDir.AddDir(line);
            else
                currentDir.AddFile(line);
        }
    }

    public void Part1()
    {
        var directorySizes = new List<int>();
        GetDirectorySizes(_root, directorySizes);
        Console.WriteLine(directorySizes.Where(x => x <= 100_000).Sum());
    }

    public void Part2()
    {
        var directorySizes = new List<int>();
        var totalSizes = GetDirectorySizes(_root, directorySizes);
        var spaceNeeded = 30_000_000 - (70_000_000 - totalSizes);
        Console.WriteLine(directorySizes.FirstOrDefault(x => x >= spaceNeeded));
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
