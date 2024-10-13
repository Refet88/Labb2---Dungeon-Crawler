using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2
{
    internal class Rat : Enemy
    {
        private LevelData levelData { get; set; }
        private static Random random = new Random();

        public Rat(Positions position, LevelData levelData) : base(position, 'r', ConsoleColor.Red, "Rat", 10, new Dice(1, 6, 3), new Dice(1, 6, 1))
        {
            this.levelData = levelData;
        }
        public override void Update()
        {
            int direction = random.Next(4);
            int newX = Position.X;
            int newY = Position.Y;


            switch (direction)
            {
                case 0:
                    newY--;
                    break;
                case 1:
                    newY++;
                    break;
                case 2:
                    newX--;
                    break;
                case 3:
                    newX++;
                    break;

            }

            if (!IsCollision(newX, newY))
            {
                Console.SetCursorPosition(Position.X, Position.Y);
                Console.Write(' ');
                Position.X = newX;
                Position.Y = newY;
                Draw();
            }
        }
        private bool IsCollision(int x, int y)
        {
            foreach (var element in levelData.Elements)
            {
                if (element.Position.X == x && element.Position.Y == y)
                {
                    if (element is Wall)
                    {
                        return true;
                    }
                    else if (element is Player player)
                    {
                        
                        StartCombatWithPlayer();
                        return true;
                    }
                }
            }
            return false;
        }
        private void StartCombatWithPlayer()
        {
            Combat combat = new Combat(levelData.player, this, levelData.Elements, false);
            combat.StartCombat();
            levelData.player.HP = combat.GetPlayerHP();

            if (this.HP > 0)
            {
                MoveAwayFromPlayer(); 
            }
        }

        private void MoveAwayFromPlayer()
        {
            int playerX = levelData.player.Position.X;
            int playerY = levelData.player.Position.Y;

            int newX = Position.X;
            int newY = Position.Y;

            if (playerX < Position.X)
            {
                newX++;
            }
            else if (playerX > Position.X)
            {
                newX--;
            }

            if (playerY < Position.Y)
            {
                newY++;
            }
            else if (playerY > Position.Y)
            {
                newY--;
            }

            if (!IsCollision(newX, newY))
            {
                Console.SetCursorPosition(Position.X, Position.Y);
                Console.Write(' ');
                Position.X = newX;
                Position.Y = newY;
                Draw();
            }
        }
    }
}