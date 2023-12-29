namespace Snake;

internal class Food
{
    private readonly Random random;
    private Point position;
    private readonly char shape;

    internal Food(char shape)
    {
        random = new Random();
        int x = random.Next(0, Console.WindowWidth);
        int y = random.Next(0, Console.WindowHeight - 1);
        position = new Point(x, y);
        this.shape = shape;
    }

    internal void Respawn()
    {
        int x = random.Next(0, Console.WindowWidth);
        int y = random.Next(0, Console.WindowHeight - 1);

        position = new Point(x, y);
    }

    internal char Shape()
    {
        return shape;
    }

    internal Point Position
    {
        get { return position; }
    }
}
