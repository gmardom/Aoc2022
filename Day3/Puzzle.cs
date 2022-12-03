using static System.IO.File;

namespace Day3;

public class Puzzle
{
    private readonly List<string> _rucksacks = new();

    public Puzzle(string file)
    {
        foreach (var rucksack in ReadLines(file))
            _rucksacks.Add(rucksack);
    }

    public void Part1()
    {
        var sumOfPriorities = _rucksacks
            .Select(FindSameItemInCompartments)
            .Select(GetItemPriority)
            .Sum();

        Console.WriteLine(sumOfPriorities);
    }

    public void Part2()
    {
        const int elfGroupSize = 3;
        var sumOfPriorities = 0;

        for (var i = 0; i < _rucksacks.Count; i += elfGroupSize)
        {
            var rucksacks = _rucksacks.GetRange(i, elfGroupSize);
            var sameItem = FindSameItemInThreeRucksacks(rucksacks);

            sumOfPriorities += GetItemPriority(sameItem);
        }

        Console.WriteLine(sumOfPriorities);
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
