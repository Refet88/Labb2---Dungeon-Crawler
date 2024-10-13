using Labb2;
using System.Data;
using System.Runtime.CompilerServices;

public class Program
{
    public static void Main()
    {
        var player = new Player(new Positions(0, 0));
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Controls:");
        Console.WriteLine();
        Console.WriteLine("Up: UpArrow");
        Console.WriteLine("Down: DownArrow");
        Console.WriteLine("Right: RightArrow");
        Console.WriteLine("Left: LeftArrow");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Press key to continue");
        Console.ReadKey();
        Console.Clear();
      
        LevelData levelData = new LevelData();
        levelData.Load("Level.txt");

        Gameloop gameloop = new Gameloop(levelData, player);
        gameloop.StartGame();

    }
}