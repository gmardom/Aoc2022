namespace AdventOfCode.Calendar.Day2;

public static class RockPaperScissors
{
    public enum Move
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    private const int PointsForWin = 6;
    private const int PointsForDraw = 3;
    private const int PointsForLose = 0;

    public static Move ReadMove(char character) => character switch
    {
        'A' or 'X' => Move.Rock,
        'B' or 'Y' => Move.Paper,
        'C' or 'Z' => Move.Scissors,
        _ => throw new ArgumentOutOfRangeException(nameof(character), character, null)
    };

    public static Move MakeMove(Move foes, char cond) => foes switch
    {
        // X - lose, Y - draw, Z -> win

        Move.Rock     when cond == 'X' => Move.Scissors,
        Move.Rock     when cond == 'Y' => Move.Rock,
        Move.Rock     when cond == 'Z' => Move.Paper,

        Move.Paper    when cond == 'X' => Move.Rock,
        Move.Paper    when cond == 'Y' => Move.Paper,
        Move.Paper    when cond == 'Z' => Move.Scissors,

        Move.Scissors when cond == 'X' => Move.Paper,
        Move.Scissors when cond == 'Y' => Move.Scissors,
        Move.Scissors when cond == 'Z' => Move.Rock,

        _ => throw new ArgumentOutOfRangeException(nameof(foes), foes, null)
    };

    public static int CalculatePoints(Move foes, Move mine)
    {
        if (DidFirstWin(mine, foes))
            return (int)(mine + PointsForWin);

        if (DidFirstWin(foes, mine))
            return (int)(mine + PointsForLose);

        if (IsDraw(foes, mine))
            return (int)(mine + PointsForDraw);

        return 0;
    }

    private static bool DidFirstWin(Move first, Move second) => first switch
    {
        Move.Rock     when second == Move.Scissors => true,
        Move.Paper    when second == Move.Rock     => true,
        Move.Scissors when second == Move.Paper    => true,
        _ => false
    };

    private static bool IsDraw(Move first, Move second) => first switch
    {
        Move.Rock     when second == Move.Rock     => true,
        Move.Paper    when second == Move.Paper    => true,
        Move.Scissors when second == Move.Scissors => true,
        _ => false
    };
}
