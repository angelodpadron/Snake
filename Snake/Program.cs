using System.Diagnostics;

namespace Snake;

internal class Program
{
    static void Main(string[] args)
    {
        Loop();
    }

    private static void Loop()
    {
        ConsoleKey mode;

        Console.CursorVisible = false;

        do
        {
            Console.Clear();
            Console.WriteLine("Game Mode");
            Console.WriteLine("1. Single");
            Console.WriteLine("2. VS");
            mode = Console.ReadKey().Key;
        } while (mode != ConsoleKey.D1 && mode != ConsoleKey.D2);

        List<Snake> snakes = [];
        var food = new Food(Shape.Plus);
        Stopwatch timer = new();

        switch (mode)
        {
            case ConsoleKey.D1:
                snakes.Add(
                    new(
                        [new Point(3, 1), new Point(2, 1), new Point(1, 1)],
                        Direction.Right,
                        Shape.Ball,
                        ControlScheme.Player1
                    )
                );
                break;
            case ConsoleKey.D2:
                snakes.AddRange(
                    [
                        new(
                            [new Point(3, 1), new Point(2, 1), new Point(1, 1)],
                            Direction.Right,
                            Shape.Ball,
                            ControlScheme.Player1
                        ),
                        new(
                            [new Point(3, 3), new Point(2, 3), new Point(1, 3)],
                            Direction.Right,
                            Shape.Star,
                            ControlScheme.Player2
                        )
                    ]
                );
                break;
        }

        timer.Start();
        Console.Clear();

        while (true)
        {
            //Console.Clear();

            //Console.SetCursorPosition(0, 0);

            if (Console.KeyAvailable)
            {
                var input = Console.ReadKey();

                if (input.Key == ConsoleKey.Enter)
                {
                    timer.Stop();
                    Console.Write("Paused");

                    while (!Console.KeyAvailable) { }
                    timer.Start();
                    continue;
                }

                snakes.ForEach(snake => snake.HandleInput(input.Key));
            }

            snakes.ForEach(snake => snake.Move());

            if (snakes.Any(snake => snake.Collision()))
            {
                timer.Stop();
                TimeSpan ts = timer.Elapsed;

                Console.WriteLine("Game Over");
                snakes.ForEach(
                    snake =>
                        Console.WriteLine(
                            $"Player {snakes.IndexOf(snake) + 1} size: {snake.Size()}"
                        )
                );
                Console.WriteLine(
                    "Time wasted: {0:00}:{1:00}:{2:00}.{3}",
                    ts.Hours,
                    ts.Minutes,
                    ts.Seconds,
                    ts.Milliseconds
                );
                Console.WriteLine("Press a key to exit...");

                while (!Console.KeyAvailable) { }
                
                return;
            }

            snakes.ForEach(snake =>
            {
                if (snake.HeadPosition().Equals(food.Position))
                {
                    food.Respawn();
                    snake.Grow();
                }
            });         

            Renderer.Render(snakes, food);

            Thread.Sleep(100);
        }
    }
}
