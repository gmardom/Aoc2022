using AdventOfCode.Utils;

namespace AdventOfCode.Calendar.Day8;

[PuzzleSolution] public class Puzzle : Solution
{
    public Puzzle() : base(8)
    { }

    protected override void Part1()
    {
        var trees = Input.Lines().ToArray()
            .Select(x => x.ToCharArray().AsInt().ToList())
            .ToList();

        int rows = trees.Count, cols = trees[0].Count;
        var visibleTrees = rows * 2 + cols * 2 - 4;

        for (var y = 1; y < rows - 1; y++)
            for (var x = 1; x < cols - 1; x++)
                visibleTrees += IsTreeVisible(x, y, trees) ? 1 : 0;

        Print(1, visibleTrees);
    }

    protected override void Part2()
    {
        var trees = Input.Lines().ToArray()
            .Select(x => x.ToCharArray().AsInt().ToList())
            .ToList();

        int rows = trees.Count, cols = trees[0].Count;
        var highestScore = 0;

        for (var y = 1; y < rows - 1; y++)
            for (var x = 1; x < cols - 1; x++)
            {
                var score = CalculateScore(x, y, trees);
                if (score > highestScore)
                    highestScore = score;
            }

        Print(2, highestScore);
    }

    private bool IsTreeVisible(int x, int y, IReadOnlyList<List<int>> trees)
    {
        var tree = trees[y][x];
        var visible = true;

        for (var j = 0; visible && j < x; j++)
            if (trees[y][j] >= tree)
                visible = false;

        if (visible) return true;

        visible = true;
        for (var j = trees[y].Count - 1; visible && x < j; j--)
            if (trees[y][j] >= tree)
                visible = false;

        if (visible) return true;

        visible = true;
        for (var j = 0; visible && j < y; j++)
            if (trees[j][x] >= tree)
                visible = false;

        if (visible) return true;

        visible = true;
        for (var j = trees.Count - 1; visible && y < j; j--)
            if (trees[j][x] >= tree)
                visible = false;

        if (visible) return true;

        return false;
    }

    private int CalculateScore(int x, int y, IReadOnlyList<List<int>> trees)
    {
        var height = trees[y][x];
        var product = 1;

        var cnt = 0;
        for (var j = x - 1; j >= 0; j--)
        {
            cnt++;
            if (trees[y][j] >= height)
                break;
        }
        product *= cnt;

        cnt = 0;
        for (var j = x + 1; j < trees[y].Count; j++)
        {
            cnt++;
            if (trees[y][j] >= height)
                break;
        }
        product *= cnt;

        cnt = 0;
        for (var j = y - 1; j >= 0; j--)
        {
            cnt++;
            if (trees[j][x] >= height)
                break;
        }
        product *= cnt;

        cnt = 0;
        for (var j = y + 1; j < trees.Count; j++)
        {
            cnt++;
            if (trees[j][x] >= height)
                break;
        }
        product *= cnt;

        return product;
    }
}
