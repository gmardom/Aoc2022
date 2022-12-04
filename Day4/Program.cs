namespace Day4;

public static class Program
{
    private static int Main(string[] args)
    {
        var puzzle = new Puzzle("input.txt");

        puzzle.Part1();
        puzzle.Part2();

        return 0;
    }
}
