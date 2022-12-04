namespace Day4;

public class Elf
{
    private readonly int _start;
    private readonly int _end;

    public Elf(string section)
    {
        var numbers = section.Split('-');
        _start = int.Parse(numbers[0]);
        _end = int.Parse(numbers[1]);
    }

    public bool FullyOverlaps(Elf other) =>
        other._start <= _start && other._end >= _end;

    public bool OverlapsOnEnd(Elf other) =>
        other._start <= _start && (other._end >= _start && other._end <= _end);

    public bool OverlapsOnStart(Elf other) =>
        other._end >= _end && (other._start >= _start && other._end <= _end);

    public bool OverlapsAny(Elf other) =>
        FullyOverlaps(other) || OverlapsOnEnd(other) || OverlapsOnStart(other);
}
