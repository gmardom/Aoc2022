using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day10;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(10)
    { }

    protected override void Part1()
    {
        var instructions = Input.Lines();
        var signalStrengths = Execute(instructions).Select(
                (x, i) => (tick: i + 1, x))
            .Where(a => (a.tick - 20) % 40 == 0)
            .Select(a => (a.tick, strength: a.x * a.tick));

        Print(1, signalStrengths.Sum(x => x.strength));
    }

    protected override void Part2()
    {
        const int columns = 40;
        const int rows = 6;
        var screen = new char[columns * rows];
        var output = Execute(Input.Lines()).Select((x, i) => (tick: i, x));

        foreach (var (tick, x) in output)
        {
            var index = tick % screen.Length;
            var column = index % columns;

            if (column >= x - 1 && column <= x + 1)
                screen[index] = '#';
            else
                screen[index] = '.';
        }

        Print(2, "");

        if (NoOutput) return;
        for (var row = 0; row < rows; ++row)
            Console.WriteLine(new string(screen, row * columns, columns));
    }

    private static IEnumerable<int> Execute(IEnumerable<string> instructions)
    {
        var x = 1;
        foreach (var instruction in instructions)
        {
            if (instruction.StartsWith("noop"))
            {
                yield return x;
                continue;
            }

            yield return x;
            yield return x;
            x += int.Parse(instruction.Split(" ")[1]);
        }
    }
}
