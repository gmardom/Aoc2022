namespace AdventOfCode.Utils;

public static class Extensions
{
    public static IEnumerable<int> AsInt(this IEnumerable<string> input) =>
        input.Select(x => int.Parse(x));

    public static IEnumerable<int> AsInt(this IEnumerable<char> input) =>
        input.Select(x => int.Parse(x.ToString()));

    public static string AsString(this IEnumerable<char> input) =>
        input.Aggregate("", (current, character) => current + character);

    public static IEnumerable<(int index, T value)> WithIndexes<T>(this IEnumerable<T> values)
    {
        var index = 0;
        foreach(var val in values) yield return (index++, val);
    }

    public static IEnumerable<(int index, int value)> NonNull(this IEnumerable<(int index, int? value)> values)
    {
        return values
            .Where(x => x.value.HasValue)
            .Select(x => (x.index, x.value!.Value));
    }

    public static IEnumerable<string> SplitBy(this string contents, string splitBy)
    {
        var ix = contents.IndexOf(splitBy, StringComparison.Ordinal);
        var splitLength = splitBy.Length;
        var previousIndex = 0;

        while (ix >= 0)
        {
            yield return contents.Substring(previousIndex, ix - previousIndex);
            previousIndex = ix + splitLength;
            ix = contents.IndexOf(splitBy, previousIndex, StringComparison.Ordinal);
        }

        var remain = contents[previousIndex..];
        if (!string.IsNullOrEmpty(remain)) yield return remain;
    }

    public static ValueTuple<T1, T2> As<T, T1, T2>(this T[] input, Func<T, T1> p1, Func<T, T2> p2) =>
        (p1(input[0]), p2(input[1]));

    public static ValueTuple<T1, T2, T3> As<T1, T2, T3>(this string[] input, Func<string, T1> p1, Func<string, T2> p2, Func<string, T3> p3) =>
        (p1(input[0]), p2(input[1]), p3(input[2]));

    public static ValueTuple<T1, T2, T3, T4> As<T1, T2, T3, T4>(this string[] input, Func<string, T1> p1, Func<string, T2> p2, Func<string, T3> p3, Func<string, T4> p4) =>
        (p1(input[0]), p2(input[1]), p3(input[2]), p4(input[3]));

    public static IEnumerable<T> Range<T>(this IEnumerable<T> input, int start, int end) =>
        input.ToArray()[start..end];

    public static IEnumerable<T> Range<T>(this IEnumerable<T> input, Index start, Index end) =>
        input.ToArray()[start..end];

    public static int Length<T>(this IEnumerable<T> input) =>
        input.ToArray().Length;
}
