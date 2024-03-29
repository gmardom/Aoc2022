﻿using System.Drawing;

namespace AdventOfCode.Calendar.Day9;

public sealed class Rope
{
    private readonly List<Point> _knots;
    public readonly HashSet<Point> UniqueTailPositions;

    public Rope(int knots)
    {
        if (knots < 2)
            throw new ArgumentException("Rope must have at least two knots.", nameof(knots));

        _knots = Enumerable.Range(0, knots).Select(_ => new Point()).ToList();
        UniqueTailPositions = new HashSet<Point> { _knots[^1] };
    }

    public void PerformMotion(Motion motion)
    {
        for (var i = 0; i < motion.Count; ++i)
        {
            var headIndex = 0;

            _knots[headIndex] = Move(_knots[headIndex], motion.Direction);

            for (var tailIndex = 1; tailIndex < _knots.Count; ++tailIndex)
            {
                headIndex = tailIndex - 1;

                if (!KnotsTouching(_knots[headIndex], _knots[tailIndex]))
                    _knots[tailIndex] = Follow(_knots[headIndex], _knots[tailIndex]);
            }

            UniqueTailPositions.Add(_knots[^1]);
        }
    }

    private static Point Move(Point original, Direction direction) => direction switch
    {
        Direction.Up    => original with { Y = original.Y + 1 },
        Direction.Down  => original with { Y = original.Y - 1 },
        Direction.Left  => original with { X = original.X - 1 },
        Direction.Right => original with { X = original.X + 1 },
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unsupported direction")
    };

    private static Point Follow(Point head, Point tail)
    {
        var nextTail = head.X switch
        {
            _ when head.X > tail.X => tail with { X = tail.X + 1 },
            _ when head.X < tail.X => tail with { X = tail.X - 1 },
            _ => tail,
        };

        return head.Y switch
        {
            _ when head.Y > nextTail.Y => nextTail with { Y = nextTail.Y + 1 },
            _ when head.Y < nextTail.Y => nextTail with { Y = nextTail.Y - 1 },
            _ => nextTail,
        };
    }

    private static bool KnotsTouching(Point head, Point tail)
    {
        var overlapping = head == tail;

        var deltaX = Math.Abs(head.X - tail.X);
        var deltaY = Math.Abs(head.Y - tail.Y);

        var touchingX    = deltaX == 1 && deltaY == 0;
        var touchingY    = deltaY == 1 && deltaX == 0;
        var touchingDiag = deltaX == 1 && deltaY == 1;

        return overlapping || touchingX || touchingY || touchingDiag;
    }
}
