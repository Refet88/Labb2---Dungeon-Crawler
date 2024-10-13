using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public class Dice
    {
        private int NumberOfDice { get; set; }
        private int SidesPerDice { get; set; }
        private int Modifier { get; set; }

        private Random _random;

        public Dice(int numberOfDice, int sidesPerDice, int modifier)
        {
            NumberOfDice = numberOfDice;
            SidesPerDice = sidesPerDice;
            Modifier = modifier;
            _random = new Random();
        }
        public int Throw()
        {
            int total = 0;
            for (int i = 0; i < NumberOfDice; i++)
            {
                total += _random.Next(1, SidesPerDice + 1);
            }
            total += Modifier;
            return total;
        }

        public override string ToString()
        {
            return $"{NumberOfDice}d{SidesPerDice}+{Modifier}";
        }
    }
}