namespace Snake;

class Food
{
    private readonly Random random;
    private readonly Point position;

    public Food()
    {
        random = new Random();
        int posX = random.Next(0, Console.WindowWidth);
        int posY = random.Next(0, Console.WindowHeight - 1);
        position = new Point(posX, posY);
    }

    public void Draw()
    {
        Console.SetCursorPosition(position.X, position.Y);
        Console.Write("*");
    }

    public void Respawn()
    {
        position.X = random.Next(0, Console.WindowWidth);
        position.Y = random.Next(0, Console.WindowHeight);
    }

    public Point Position
    {
        get { return position; }
    }
}
