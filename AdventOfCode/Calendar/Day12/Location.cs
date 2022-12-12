namespace AdventOfCode.Calendar.Day12;

public record Location(char Height)
{
    public bool Visited { get; set; } = false;
}
