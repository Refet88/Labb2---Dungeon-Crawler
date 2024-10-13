using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public abstract class Enemy : LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice AttackDice { get; set; }
        public Dice DefenceDice { get; set; }


        public Enemy(Positions position, char typeOfChar, ConsoleColor color, string name, int hp, Dice attackDice, Dice defenceDice) : base(position, typeOfChar, color)
        {
            this.Name = name;
            HP = hp;
            AttackDice = attackDice;
            DefenceDice = defenceDice;        }
        public abstract void Update();
    }
}
