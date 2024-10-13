using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class Movement
    {
        private Player player;
        private List<LevelElement> elements;
        public Movement(Player player, List<LevelElement> elements)
        {
            this.player = player;
            this.elements = elements;
            Console.CursorVisible = false;
        }

        public void Movements(ConsoleKey key)
        {
            int newX = player.Position.X;
            int newY = player.Position.Y;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    newY -= 1;
                    break;
                case ConsoleKey.DownArrow:
                    newY += 1;
                    break;
                case ConsoleKey.LeftArrow:
                    newX -= 1;
                    break;
                case ConsoleKey.RightArrow:
                    newX += 1;
                    break;
            }

            if (!IsCollision(newX, newY))
            {
                Console.SetCursorPosition(player.Position.X, player.Position.Y);
                Console.Write(' ');
                player.Position.X = newX;
                player.Position.Y = newY;
            }
        }

        private bool IsCollision(int x, int y)
        {
            foreach (var element in elements)
            {
                if (element.Position.X == x && element.Position.Y == y)
                {
                    if (element is Wall)
                    {
                        return true;
                    }
                    else if (element is Enemy enemy)
                    {
                        Combat combat = new Combat(player, enemy, elements, true);
                        combat.StartCombat();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
