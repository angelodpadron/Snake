using System.Text;

namespace Snake;
internal static class Renderer
{

    internal static void Render(List<Snake> snakes, Food food)
    {
        StringBuilder stringBuilder = new();
        List<List<char>> screen = [];

        int width = Console.WindowWidth;
        int height = Console.WindowHeight - 1;

        for (int col = 0; col < height; col++)
        {
            List<char> line = [];

            for (int row = 0; row < width; row++)
            {
                line.Add(' ');
            }

            screen.Add(line);
        }

        snakes
            .ForEach(snake => snake.Body()
                .ForEach(point => screen[point.Y][point.X] = snake.Shape()));

        screen[food.Position.Y][food.Position.X] = food.Shape();

        Console.SetCursorPosition(0, 0);

        foreach (List<char> line in screen)
        {
            stringBuilder.AppendLine(new string(line.ToArray()));
        }

        Console.Write(stringBuilder.ToString());


    }
}
