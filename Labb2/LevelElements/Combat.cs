using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Labb2
{
    public class Combat
    {
        public Player Player;
        private Enemy Enemy;
        private List<LevelElement> Elements;
        public bool PlayerStartsCombat { get; private set; }


        public Combat(Player player, Enemy enemy, List<LevelElement> elements, bool playerStarts)
        {
            this.Player = player;
            this.Enemy = enemy;
            this.Elements = elements;
            this.PlayerStartsCombat = playerStarts;
        }

        public void StartCombat()
        {

            int originalX = Console.CursorLeft;
            int originalY = Console.CursorTop;

            Console.SetCursorPosition(60, 0);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("=== Combat ===");

            if (PlayerStartsCombat)
            {
                PerformAttack(Player, Enemy, 1);
                if (Enemy.HP > 0)
                {
                    Console.ReadKey();
                }
            }
            else
            {
                PerformAttack(Enemy, Player, 1);
                if (Player.HP > 0)
                {
                    Console.ReadKey();
                    PerformAttack(Player, Enemy, 9);
                }
            }

            if (Player.HP > 0 && Enemy.HP > 0)
            {
                Console.ReadKey();
                Console.SetCursorPosition(60, 17);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Combat has ended.");
                Console.SetCursorPosition(60, 18);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Press button to continue...");
            }

            Player.DrawPlayer();
            Console.SetCursorPosition(originalX, originalY);
            Console.ReadKey();
            ClearCombatText(0, 18);
        }

        public int GetPlayerHP()
        {
            return Player.HP;
        }

        private void PerformAttack(LevelElement attacker, LevelElement defender, int startLine)
        {
            int attackRoll = attacker is Player ? ((Player)attacker).AttackDice.Throw() : ((Enemy)attacker).AttackDice.Throw();
            int defenseRoll = defender is Player ? ((Player)defender).DefenceDice.Throw() : ((Enemy)defender).DefenceDice.Throw();
            int damage;

            if (attackRoll > defenseRoll)
            {
                damage = attackRoll - defenseRoll;
            }
            else
            {
                damage = defenseRoll - attackRoll;
            }

            string attackerName = attacker is Player ? ((Player)attacker).Name : ((Enemy)attacker).Name;
            string defenderName = defender is Player ? ((Player)defender).Name : ((Enemy)defender).Name;

            Console.SetCursorPosition(60, startLine);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{attackerName} ATTACKS {defenderName}");
            Console.SetCursorPosition(60, startLine + 1);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{attackerName} Attack Roll ({(attacker is Player ? ((Player)attacker).AttackDice.ToString() : ((Enemy)attacker).AttackDice.ToString())}): {attackRoll}");
            Console.SetCursorPosition(60, startLine + 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{defenderName} Defense Roll ({(defender is Player ? ((Player)defender).DefenceDice.ToString() : ((Enemy)defender).DefenceDice.ToString())}): {defenseRoll}");

            ShowCombatTex(attacker.Position, defender.Position);

            if (damage > 0)
            {
                if (attackRoll > defenseRoll)
                {
                    if (defender is Player)
                    {
                        ((Player)defender).HP -= damage;
                    }
                    else
                    {
                        ((Enemy)defender).HP -= damage;
                    }
                    Console.SetCursorPosition(60, startLine + 3);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{attackerName} deals {damage} damage to {defenderName}");
                    Console.SetCursorPosition(60, startLine + 4);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{defenderName} HP: {(defender is Player ? ((Player)defender).HP : ((Enemy)defender).HP)}");
                }
                else if (defenseRoll > attackRoll)
                {
                    if (attacker is Player)
                    {
                        ((Player)attacker).HP -= damage;
                    }
                    else
                    {
                        ((Enemy)attacker).HP -= damage;
                    }
                    Console.SetCursorPosition(60, startLine + 3);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{defenderName} deals {damage} damage to {attackerName}");
                    Console.SetCursorPosition(60, startLine + 4);
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"{attackerName} HP: {(attacker is Player ? ((Player)attacker).HP : ((Enemy)attacker).HP)}");
                }
            }
            else
            {
                Console.SetCursorPosition(60, startLine + 5);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No damage dealt. Defense successful!");
            }

            Console.ResetColor();
            Console.SetCursorPosition(60, startLine + 6);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("================");
            Console.SetCursorPosition(60, startLine +7);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press button to continue...");


            if ((defender is Player && ((Player)defender).HP <= 0) || (defender is Enemy && ((Enemy)defender).HP <= 0))
            {
                Console.ReadKey();
                Console.SetCursorPosition(60, 17);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{defenderName} is defeated!");
                if (Elements != null && Elements.Contains(defender))
                {
                    Elements.Remove(defender);
                    Console.SetCursorPosition(Enemy.Position.X, Enemy.Position.Y);
                    Console.Write(' ');
                }
                Console.ReadKey();
                ClearCombatText(0, 18);
            }
        }


        private void ClearCombatText(int startLine, int endLine)
        {
            for (int i = startLine; i <= endLine; i++)
            {
                Console.SetCursorPosition(60, i);
                Console.Write(new string(' ', Console.WindowWidth - 60));
            }
        }

        private void ClearArea(int startX, int startY, int height)
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write(new string(' ', Console.WindowWidth - startX));
            }
        }

        private void ShowCombatTex(Positions attacker, Positions target)
        {
            int steps = 5;
            for (int i = 0; i < steps; i++)
            {
                int x = attacker.X + (target.X - attacker.X) * i / steps;
                int y = attacker.Y + (target.Y - attacker.Y) * i / steps;
                Console.SetCursorPosition(x, y);
                Console.Write('*');
                Thread.Sleep(50);
                Console.SetCursorPosition(x, y);
                Console.Write(' ');
            }
        }
    }
}