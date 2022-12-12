namespace AdventOfCode.Calendar.Day12;

public record WayPoint(int X, int Y)
{
    private WayPoint? Last { get; set; }

    private WayPoint Up => new(X, Y - 1);
    private WayPoint Down => new(X, Y + 1);
    private WayPoint Left => new(X - 1, Y);
    private WayPoint Right => new(X + 1, Y);

    public IEnumerable<WayPoint> Seen()
    {
        var p = Last;
        while (p != null)
        {
            yield return p;
            p = p.Last;
        }
    }

    private static bool Traversable(Location[][] terrain, WayPoint from, WayPoint to, Func<char, char, bool> rule)
    {
        if (to.X < 0 || to.X >= terrain[0].Length || to.Y < 0 || to.Y >= terrain.Length || terrain[from.Y][from.X].Visited)
            return false;

        char NoMarker(char c) => c switch { 'S' => 'a', 'E' => 'z', _ => c };
        return rule(
            NoMarker(terrain[from.Y][from.X].Height),
            NoMarker(terrain[to.Y][to.X].Height)
        );
    }

    public static WayPoint Navigate(Location[][] terrain, char beginAt, char end, Func<char, char, bool> rule)
    {
        var start = terrain
            .SelectMany((l, y) => l.Select((h, x) => new WayPoint(h.Height == beginAt ? x : -1, y)))
            .First(p => p.X > -1);

        var path = new Queue<WayPoint>(new[] {start});
        var p = new WayPoint(-1, -1);

        while (path.Any())
        {
            p = path.Dequeue();
            if (terrain[p.Y][p.X].Height == end)
                break;

            new[] { p.Up, p.Down, p.Left, p.Right }.Where(n => Traversable(terrain, p, n, rule)).ToList().ForEach(n =>
            {
                n.Last = p;
                path.Enqueue(n);
            });

            terrain[p.Y][p.X].Visited = true;
        }

        return p;
    }
}
