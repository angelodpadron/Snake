namespace Snake;
internal class Program
{
    static void Main(string[] args)
    {
        int posX = 0;
        int posY = 0;

        int movX = 1;
        int movY = 0;

        var random = new Random();

        int foodX;
        int foodY;

        foodX = random.Next(0, Console.WindowWidth);
        foodY = random.Next(0, Console.WindowHeight);

        while (true)
        {
            if (posX == foodX && posY == foodY)
            {
                foodX = random.Next(0, Console.WindowWidth);
                foodY = random.Next(0, Console.WindowHeight);
            }

            posX += movX;
            posY += movY;

            Draw(ref posX, ref posY, foodX, foodY);
            
            if (!Console.KeyAvailable) continue;

            var input = Console.ReadKey();

            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    movX = 0;
                    movY = -1;
                    break;
                case ConsoleKey.DownArrow:
                    movX = 0;
                    movY = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    movX = -1;
                    movY = 0;
                    break;
                case ConsoleKey.RightArrow:
                    movX = 1;
                    movY = 0;
                    break;
            }

        }

    }

    private static void Draw(ref int posX, ref int posY, int foodX, int foodY)
    {
        Console.Clear();

        Console.SetCursorPosition(posX, posY);
        Console.Write("*");
        Console.SetCursorPosition(foodX, foodY);
        Console.WriteLine("+");

        Thread.Sleep(100);
    }
}