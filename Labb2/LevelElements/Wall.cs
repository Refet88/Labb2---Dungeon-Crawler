using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    internal class Wall : LevelElement
    {
        public Wall (Positions position) : base(position, '#', ConsoleColor.Gray)
        {

        }

    }
}
