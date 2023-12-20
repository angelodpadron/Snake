namespace Snake;

class Point(int x, int y)
{
    private int x = x;
    private int y = y;

    public int X
    {
        get => x;
        set => x = value;
    }

    public int Y
    {
        get => y;
        set => y = value;
    }

    public bool Equals(Point point)
    {
        return X == point.X && Y == point.Y;
    }
}
