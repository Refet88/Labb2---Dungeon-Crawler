using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2
{
    internal class Snake : Enemy
    {
        public static LevelData levelData { get; set; } = new LevelData();
        private static Random random = new Random();
        public Snake(Positions position, LevelData levelData) : base(position, 's', ConsoleColor.Green, "Snake", 25, new Dice(2, 6, 2), new Dice(1, 6, 2))
        {
            Snake.levelData = levelData;
        }
        public override void Update()
        {
            int distanceX = levelData.player.Position.X - this.Position.X;
            if (distanceX < 0) distanceX = -distanceX;

            int distanceY = levelData.player.Position.Y - this.Position.Y;
            if (distanceY < 0) distanceY = -distanceY;

            

            if (distanceX > 2 || distanceY > 2)
            {
                int randomDirection = random.Next(4);
                int randomNewX = this.Position.X;
                int randomNewY = this.Position.Y;

                switch (randomDirection)
                {
                    case 0:
                        randomNewY--;
                        break;
                    case 1:
                        randomNewY++;
                        break;
                    case 2:
                        randomNewX--;
                        break;
                    case 3:
                        randomNewX++;
                        break;
                }
                if (!IsCollision(randomNewX, randomNewY))
                {
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(' ');
                    Position.X = randomNewX;
                    Position.Y = randomNewY;
                    Draw();
                }
                return;

            }
            else if (distanceX <= 1 && distanceY <= 1)
            {
                StartCombatWithPlayer();
                return;
            }

            int newX = this.Position.X;
            int newY = this.Position.Y;

            if (distanceX > distanceY)
            {
                if (levelData.player.Position.X > this.Position.X)
                {
                    newX -= 1;
                }
                else
                {
                    newX += 1;
                }
            }
            else
            {
                if (levelData.player.Position.Y > this.Position.Y)
                {
                    newY -= 1;
                }
                else
                {
                    newY += 1;
                }
            }

            if (!IsCollision(newX, newY))
            {
                if (IsVisible)
                {
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(' ');
                    Position.X = newX;
                    Position.Y = newY;
                    Draw();
                }
                else
                {
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(' ');
                }
            }
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

        private bool IsCollision(int x, int y)
        {
            foreach (var element in levelData.Elements)
            {
                if (element.Position.X == x && element.Position.Y == y && (element is Wall || element is Enemy))
                {
                    return true;
                }
            }
            return false;
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

        }
    }
}
