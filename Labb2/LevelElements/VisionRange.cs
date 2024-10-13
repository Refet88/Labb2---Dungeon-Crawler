using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    class VisionRange
    {
        public int Radius { get; set; }

        public VisionRange(int radius)
        {
            Radius = radius;
        }

        public bool IsInVisionRange(Positions playerPosition, Positions objectPosition)
        {
            int deltaX = playerPosition.X - objectPosition.X;
            int deltaY = playerPosition.Y - objectPosition.Y;
            int distanceSquared = deltaX * deltaX + deltaY * deltaY;

            return distanceSquared <= Radius * Radius;
        }
    }
}
