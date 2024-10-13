using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public abstract class LevelElement
    {
        public Positions Position { get; set; }
        protected char TypeOfChar { get; set; }
        protected ConsoleColor Color { get; set; }
        public bool IsVisible { get; set; }


        public LevelElement(Positions position, char typeOfChar, ConsoleColor color)
        {
            Position = position;
            TypeOfChar = typeOfChar;
            Color = color;
            IsVisible = false;
        }
        public void Draw()
        {
            if (IsVisible)
            {
                Console.SetCursorPosition(Position.X, Position.Y);
                Console.ForegroundColor = Color;
                Console.Write(TypeOfChar);
                Console.ResetColor();
            }
        }
    }
}
