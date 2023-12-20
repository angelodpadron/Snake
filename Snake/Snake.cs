namespace Snake;

class Snake(List<Point> body, Direction direction, String shape, ControlScheme controlScheme)
{
    private readonly List<Point> body = body;
    private readonly string shape = shape;
    private readonly ControlScheme controlScheme = controlScheme;
    private Direction direction = direction;

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

    public void HandleInput(ConsoleKey key)
    {
        if (controlScheme == ControlScheme.Player1)
        {
            direction = key switch
            {
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                _ => direction
            };

            return;
        }

        if (controlScheme == ControlScheme.Player2)
        {
            direction = key switch
            {
                ConsoleKey.W => Direction.Up,
                ConsoleKey.S => Direction.Down,
                ConsoleKey.A => Direction.Left,
                ConsoleKey.D => Direction.Right,
                _ => direction
            };
        }
    }

    public void Draw()
    {
        body.ForEach(point =>
        {
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write(shape);
        });
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

    public void Grow()
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
