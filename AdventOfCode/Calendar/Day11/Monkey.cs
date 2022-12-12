namespace AdventOfCode.Calendar.Day11;

public class Monkey
{
    private readonly Queue<long> _items;

    private readonly string[] _operation;
    private readonly bool _divideWorryLevel;

    public readonly int Test;
    private readonly int _testIfTrue;
    private readonly int _testIfFalse;

    public long Inspections { get; private set; }

    public Monkey(IReadOnlyList<string> input, bool divideWorryLevel)
    {
        _items = new Queue<long>(input[1][17..].Split(", ").Select(long.Parse));

        _operation = input[2][19..].Split(" ");
        _divideWorryLevel = divideWorryLevel;

        Test = int.Parse(input[3].Split(" ")[^1]);
        _testIfTrue = int.Parse(input[4].Split(" ")[^1]);
        _testIfFalse = int.Parse(input[5].Split(" ")[^1]);
    }

    public List<ValueTuple<int, long>> Throw(int divisorLimit)
    {
        var items = new List<(int, long)>();

        while (_items.Any())
        {
            Inspections += 1;

            var worryLevel = _items.Dequeue();
            worryLevel = Operation(worryLevel);
            worryLevel %= divisorLimit;

            var targetMonkey = worryLevel % Test == 0 ? _testIfTrue : _testIfFalse;

            items.Add((targetMonkey, worryLevel));
        }

        return items;
    }

    public void CatchItem(long item)
    {
        _items.Enqueue(item);
    }

    private long Operation(long item)
    {
        var op = _operation[1];
        var lhs = _operation[0] == "old" ? item : int.Parse(_operation[0]);
        var rhs = _operation[2] == "old" ? item : int.Parse(_operation[2]);

        var value = op switch
        {
            "+" => lhs + rhs,
            "*" => lhs * rhs,
            _ => throw new ArgumentOutOfRangeException()
        };

        return _divideWorryLevel ? value / 3 : value;
    }
}
