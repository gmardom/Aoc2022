using static System.IO.File;

namespace Day6;

public class Puzzle
{
    private readonly string _input;

    public Puzzle(string file)
    {
        _input = ReadLines(file).ToArray()[0];
    }

    public void Part1()
    {
        Console.WriteLine(FindStartOfAPacket(_input, 4));
    }

    public void Part2()
    {
        Console.WriteLine(FindStartOfAPacket(_input, 14));
    }

    private static int FindStartOfAPacket(string input, int signalLength)
    {
        return Enumerable.Range(0, input.Length)
                   .First(i => input
                       .Skip(i)
                       .Take(signalLength)
                       .ToHashSet().Count == signalLength)
               + signalLength;
    }
}
