using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Labb2
{
    public class Player : LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenceDice { get; set; }

        public Player(Positions position) : base(position, '@', ConsoleColor.DarkCyan)
        {
            Name = "Player";
            HP = 100;
            AttackDice = new Dice(1, 6, 3);
            DefenceDice = new Dice(1, 6, 3);
            Color = ConsoleColor.DarkCyan;
            Position = position;
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.ForegroundColor = Color;
            Console.Write(TypeOfChar);
            Console.ResetColor();
        }
    }
}
        
    

