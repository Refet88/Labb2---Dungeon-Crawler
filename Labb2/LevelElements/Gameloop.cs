using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2
{
    internal class Gameloop
    {
        private LevelData levelData;
        public Player Player;
        private bool _isRunning;
        private VisionRange visionRange;


        public Gameloop(LevelData level_Data, Player player)
        {
            levelData = level_Data;
            Player = player;
            _isRunning = true;
            visionRange = new VisionRange(5);
        }

        public void StartGame()
        {
            Movement playerMovment = new Movement(levelData.player, levelData.Elements);
            int turns = 0;
            bool playerMoved = false;

            levelData.player.DrawPlayer();

            while (_isRunning)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 0);
                Console.Write($"{Player.Name} | Health: {levelData.player.HP} | Turn: {turns}");


                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    playerMovment.Movements(key);
                    turns++;

                    levelData.UpdateVisibility(visionRange);

                    levelData.player.DrawPlayer();
                    


                    for (int i = 0; i < levelData.Elements.Count; i++)
                    {
                        var element = levelData.Elements[i];

                        element.Draw();

                        playerMoved = true;

                        if (element is Enemy enemy && playerMoved)
                        {
                            enemy.Update();
                        }
                    }
                }

                Thread.Sleep(100);
                if (levelData.player.HP <= 0)
                {
                    _isRunning = false;
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("GAME OVER!");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }
    }
}