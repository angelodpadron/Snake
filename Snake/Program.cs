using System.Diagnostics;

namespace Snake;
internal class Program
{
    static void Main(string[] args)
    {
        var snake = new Snake();
        var food = new Food();

        Stopwatch stopwatch = new();
        stopwatch.Start();

        while (true)
        {
            Console.Clear();

            if (Console.KeyAvailable)
            {
                snake.HandleDirection(Console.ReadKey());
            }

            snake.Move();

            if (snake.Collision())
            {
                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;

                Console.WriteLine("Game Over");
                Console.WriteLine($"Final size: {snake.Size()}");
                Console.WriteLine("Time wasted: {0:00}:{1:00}:{2:00}.{3}",
                        ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.WriteLine("Press a key to exit...");

                while(!Console.KeyAvailable) { }

                return;
            }

            snake.Draw();

            food.Draw();

            if (snake.HeadPosition().Equals(food.Position))
            {
                food.Respawn();
                snake.Grow();
            }

            

            Thread.Sleep(100);

        }

    }
}

class Food
{
    private readonly Random random;
    private readonly Point position;

    public Food() 
    {
        random = new Random();
        int posX = random.Next(0, Console.WindowWidth);
        int posY = random.Next(0, Console.WindowHeight);
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

    public Point Position { get { return position; } }

}

class Snake
{
    private readonly List<Point> body;
    private Direction direction;

    public Snake()
    {
        body = [new(3,0), new(2,0), new(1,0), new(0,0)];
        direction = Direction.Right;
    }

    public void Move()
    {
        Point head = body.First();
        Point newHead = new(head.X, head.Y);

        _ = direction switch
        {
            Direction.Right => newHead.X++,
            Direction.Left => newHead.X--,
            Direction.Up => newHead.Y--,
            Direction.Down => newHead.Y++
        };

        body.Insert(0, newHead);
        body.RemoveAt(body.Count - 1);

    }

    public void Draw()
    {
        body.ForEach(point =>
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.WriteLine("°");
        });
    }

    public void HandleDirection(ConsoleKeyInfo input)
    {
        direction = input.Key switch
        {
            ConsoleKey.UpArrow => Direction.Up,
            ConsoleKey.DownArrow => Direction.Down,
            ConsoleKey.LeftArrow => Direction.Left,
            ConsoleKey.RightArrow => Direction.Right
        };
    }

    public bool Collision()
    {
        bool bodyCollision = body.FindAll(point => point.Equals(body.First())).Count > 1;

        int posX = HeadPosition().X;
        int posY = HeadPosition().Y;

        int windowHeight = Console.WindowHeight;
        int windowWidth = Console.WindowWidth;

        bool withinBound = posX >= 0 && posY >= 0 && posX < windowWidth && posY < windowHeight;

        return bodyCollision || !withinBound;
    }

    public Point HeadPosition()
    {
        return body.First();
    }

    internal void Grow()
    {
        Point head = body.First();
        Point newHead = new(head.X, head.Y);

        _ = direction switch
        {
            Direction.Right => newHead.X++,
            Direction.Left => newHead.X--,
            Direction.Up => newHead.Y--,
            Direction.Down => newHead.Y++
        };

        body.Insert(0, newHead);
    }

    internal object Size()
    {
        return body.Count;
    }
}

enum Direction
{
    Left,
    Right,
    Up,
    Down
}

class Point
{
    private int x;
    private int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X
    {
        get => x; set => x = value;
    }

    public int Y
    {
        get => y; set => y = value;
    }

    public bool Equals(Point point)
    {
        return X == point.X && Y == point.Y;
    }
}