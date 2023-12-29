namespace Snake;

internal struct Point
{
    internal int X { get; }
    internal int Y { get; }

    internal Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    internal bool Equals(Point point)
    {
        return X == point.X && Y == point.Y;
    }
}
