using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day3;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(3)
    { }

    protected override void Part1()
    {
        var sumOfPriorities = Input.Lines()
            .Select(FindSameItemInCompartments)
            .Select(GetItemPriority)
            .Sum();

        Print(1, sumOfPriorities);
    }

    protected override void Part2()
    {
        const int elfGroupSize = 3;
        var rucksacks = Input.Lines().ToList();
        var sumOfPriorities = 0;

        for (var i = 0; i < rucksacks.Count; i += elfGroupSize)
        {
            var rucksacksTmp = rucksacks.GetRange(i, elfGroupSize);
            var sameItem = FindSameItemInThreeRucksacks(rucksacksTmp);

            sumOfPriorities += GetItemPriority(sameItem);
        }

        Print(2, sumOfPriorities);
    }

    private static char FindSameItemInCompartments(string items)
    {
        var firstCompartment = items[..(items.Length / 2)];
        var secondCompartment = items[(items.Length / 2)..];

        return FindSameItem(firstCompartment, secondCompartment)[0];
    }

    private static char FindSameItemInThreeRucksacks(IReadOnlyList<string> rucksacks)
    {
        var sameItemsInFirstTwo = FindSameItem(rucksacks[0], rucksacks[1]);
        return sameItemsInFirstTwo.First(item => rucksacks[2].Contains(item));
    }

    private static string FindSameItem(string first, string second) =>
        string.Concat(second.Where(first.Contains).Distinct());

    private static int GetItemPriority(char item)
    {
        const string letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return letters.IndexOf(item) + 1;
    }
}
