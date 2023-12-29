namespace Snake;

internal class Snake(List<Point> body, Direction direction, char shape, ControlScheme controlScheme)
{
    private readonly List<Point> body = body;
    private readonly char shape = shape;
    private readonly ControlScheme controlScheme = controlScheme;
    private Direction direction = direction;

    internal void Move()
    {
        Point head = body.First();
        int x = head.X;
        int y = head.Y;

        _ = direction switch
        {
            Direction.Left => x--,
            Direction.Right => x++,
            Direction.Up => y--,
            Direction.Down => y++
        };

        body.Insert(0, new(x, y));
        body.RemoveAt(body.Count - 1);
    }

    internal void HandleInput(ConsoleKey key)
    {
        if (controlScheme == ControlScheme.Player1)
        {
            direction = key switch
            {
                ConsoleKey.LeftArrow => Direction.Left,
                ConsoleKey.RightArrow => Direction.Right,
                ConsoleKey.UpArrow => Direction.Up,
                ConsoleKey.DownArrow => Direction.Down,
                _ => direction
            };

            return;
        }

        if (controlScheme == ControlScheme.Player2)
        {
            direction = key switch
            {
                ConsoleKey.A => Direction.Left,
                ConsoleKey.D => Direction.Right,
                ConsoleKey.W => Direction.Up,
                ConsoleKey.S => Direction.Down,
                _ => direction
            };
        }
    }

    internal bool Collision()
    {
        bool bodyCollision = body.FindAll(point => point.Equals(body.First())).Count > 1;

        int headXPos = HeadPosition().X;
        int headYPos = HeadPosition().Y;

        int height = Console.WindowHeight - 1;
        int width = Console.WindowWidth;

        bool withinBound = headXPos >= 0 && headYPos >= 0 && headXPos < width && headYPos < height;

        return bodyCollision || !withinBound;
    }

    internal Point HeadPosition()
    {
        return body.First();
    }

    internal void Grow()
    {
        Point head = body.First();

        int x = head.X;
        int y = head.Y;


        _ = direction switch
        {
            Direction.Left => x--,
            Direction.Right => x++,
            Direction.Up => y--,
            Direction.Down => y++
        };

        body.Insert(0, new(x, y));
    }

    internal int Size()
    {
        return body.Count;
    }

    internal List<Point> Body()
    {
        return body;
    }

    internal char Shape()
    {
        return shape;
    }
}
