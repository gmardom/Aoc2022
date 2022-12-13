using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day13;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(13)
    { }

    protected override void Part1()
    {
        var packetPairs = Input.Lines().Chunk(2);
        var validPackets = packetPairs
            .Select((pair, idx) => Compare(pair[0], pair[1]) == -1 ? idx + 1 : 0)
            .Where(i => i != 0);

        Print(1, validPackets.Sum());
    }

    protected override void Part2()
    {
        var packets = Input.Lines().ToList();
        packets.Add("[[2]]");
        packets.Add("[[6]]");
        packets.Sort(Compare);

        var d1 = packets.IndexOf("[[2]]") + 1;
        var d2 = packets.IndexOf("[[6]]") + 1;

        Print(2, d1 * d2);
    }

    private static (string token, string rest) GetToken(string s)
    {
        if (char.IsDigit(s[0]))
        {
            var token = new string(s.TakeWhile(char.IsDigit).ToArray());
            var next = token.Length == s.Length ? token.Length : token.Length + 1;
            return (token, s[next..]);
        }

        var depth = 0;
        var pos = 0;

        while (!(depth == 0 && (pos == s.Length || s[pos] == ',')))
        {
            if (s[pos] == '[') depth++;
            if (s[pos] == ']') depth--;
            pos++;
        }

        return pos == s.Length ? (s, "") : (s[..(pos)], s[(pos + 1)..]);
    }

    private static string Square(string s) => s.All(char.IsDigit) ? $"[{s}]" : s;

    private static int Compare(string left, string right)
    {
        while (left.Length > 0 && right.Length > 0)
        {
            var first = GetToken(left);
            var second = GetToken(right);

            if (first.token.Length == 0) return -1;
            if (second.token.Length == 0) return 1;

            if (first.token.All(char.IsDigit) && second.token.All(char.IsDigit))
            {
                var firstTokenAsInt = int.Parse(first.token);
                var secondTokenAsInt = int.Parse(second.token);

                if (firstTokenAsInt < secondTokenAsInt) return -1;
                if (firstTokenAsInt > secondTokenAsInt) return 1;
            }
            else
            {
                if (first.token[0] == '[' ^ second.token[0] == '[')
                {
                    first.token = Square(first.token);
                    second.token = Square(second.token);
                }

                var compare = Compare(first.token[1..^1], second.token[1..^1]);

                if (compare != 0) return compare;
            }

            left = first.rest;
            right = second.rest;
        }

        return right.Length > 0 ? -1 : left.Length > 0 ? 1 : 0;
    }
}
