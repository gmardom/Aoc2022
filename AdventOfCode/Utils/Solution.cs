using System.Diagnostics;

namespace AdventOfCode.Utils;

public abstract class Solution
{
    protected Input Input { get; }

    protected bool NoOutput = false;

    public int Day { get; } = 0;

    protected Solution(int day)
    {
        Day = day;
        Input = new Input(day);
    }

    protected abstract void Part1();
    protected abstract void Part2();

    public void Execute()
    {
        Part1();
        Part2();
    }

    public void Benchmark()
    {
        var sw1 = new Stopwatch();
        var sw2 = new Stopwatch();

        NoOutput = true;

        sw1.Start();
        Part1();
        sw1.Stop();

        sw2.Start();
        Part2();
        sw2.Stop();

        Console.Out.WriteLine($"Day {Day} benchmark:");
        Console.Error.WriteLine($"  Part 1: {sw1.ElapsedMilliseconds} ms");
        Console.Error.WriteLine($"  Part 2: {sw2.ElapsedMilliseconds} ms");
    }

    // TODO: Change how output is handled, ie. make parts return output,
    //       and pretty print them in Execute function.
    protected void Print(int part, string output)
    {
        if (NoOutput) return;
        Console.Out.WriteLine($"Day {Day.ToString("00")} Part {part}: {output}");
    }

    protected void Print(int part, int output) => Print(part, output.ToString());

    protected void Print(int part) => Print(part, string.Empty);

}
