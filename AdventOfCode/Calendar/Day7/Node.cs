namespace AdventOfCode.Calendar.Day7;

public class Node
{
    public string Name { get; init; } = string.Empty;
    public bool IsDir { get; init; }
    public int Size { get; set; }

    public Node Parent { get; init; } = null!;
    public Dictionary<string, Node> Contents { get; } = new();

    public void AddFile(ReadOnlySpan<char> line)
    {
        var index = line.IndexOf(' ');

        var size = int.Parse(line[..index]);
        var name = line.Slice(index + 1, line.Length - index - 1).ToString();

        Contents.TryAdd(name, new Node
        {
            Name = name,
            Parent = this,
            Size = size,
            IsDir = false,
        });
    }

    public void AddDir(ReadOnlySpan<char> line)
    {
        var name = line[4..].ToString();

        Contents.TryAdd(name, new Node
        {
            Name = name,
            Parent = this,
            IsDir = true,
        });
    }
}
