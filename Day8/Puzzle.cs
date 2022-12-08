using static System.IO.File;

namespace Day8;

public class Puzzle
{
    private readonly List<List<int>> _trees;

    public Puzzle(string file)
    {
        _trees = ReadLines(file).ToList()
            .Select(x => x.Select(t => int.Parse(t.ToString())).ToList())
            .ToList();

        Part1();
        Part2();
    }

    private void Part1()
    {
        int rows = _trees.Count, cols = _trees[0].Count;
        var visibleTrees = rows * 2 + cols * 2 - 4;

        for (var y = 1; y < rows - 1; y++)
            for (var x = 1; x < cols - 1; x++)
                visibleTrees += IsVisible(x, y) ? 1 : 0;

        Console.Out.WriteLine(visibleTrees);
    }

    private void Part2()
    {
        int rows = _trees.Count, cols = _trees[0].Count;
        var highestScore = 0;

        for (var y = 1; y < rows - 1; y++)
            for (var x = 1; x < cols - 1; x++)
            {
                var score = CalculateScore(x, y);
                if (score > highestScore)
                    highestScore = score;
            }

        Console.Out.WriteLine(highestScore);
    }

    private bool IsVisible(int x, int y)
    {
        var tree = _trees[y][x];
        var visible = true;

        for (var j = 0; visible && j < x; j++)
            if (_trees[y][j] >= tree)
                visible = false;

        if (visible) return true;

        visible = true;
        for (var j = _trees[y].Count - 1; visible && x < j; j--)
            if (_trees[y][j] >= tree)
                visible = false;

        if (visible) return true;

        visible = true;
        for (var j = 0; visible && j < y; j++)
            if (_trees[j][x] >= tree)
                visible = false;

        if (visible) return true;

        visible = true;
        for (var j = _trees.Count - 1; visible && y < j; j--)
            if (_trees[j][x] >= tree)
                visible = false;

        if (visible) return true;

        return false;
    }

    private int CalculateScore(int x, int y)
    {
        var height = _trees[y][x];
        var product = 1;

        var cnt = 0;
        for (var j = x - 1; j >= 0; j--)
        {
            cnt++;
            if (_trees[y][j] >= height)
                break;
        }
        product *= cnt;

        cnt = 0;
        for (var j = x + 1; j < _trees[y].Count; j++)
        {
            cnt++;
            if (_trees[y][j] >= height)
                break;
        }
        product *= cnt;

        cnt = 0;
        for (var j = y - 1; j >= 0; j--)
        {
            cnt++;
            if (_trees[j][x] >= height)
                break;
        }
        product *= cnt;

        cnt = 0;
        for (var j = y + 1; j < _trees.Count; j++)
        {
            cnt++;
            if (_trees[j][x] >= height)
                break;
        }
        product *= cnt;

        return product;
    }
}
