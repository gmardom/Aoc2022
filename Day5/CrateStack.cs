namespace Day5;

public class CrateStack
{
    private readonly Stack<char> _crates = new();

    public char Top => _crates.Peek();

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
}
