using System;
using System.Collections.Generic;
using System.Text;

namespace Casino
{
    static class Additional
    {
        public static int Bet(int cash)
        {
            Console.WriteLine("\nBet value:");

            string betinput = Console.ReadLine();

            if (betinput == "e")
            {
                Console.WriteLine("\nExit\n");
                return -1;
            }
            if (!int.TryParse(betinput, out int bet))
            {
                return 0;
            }                      
            if (cash < bet || bet <= 0)
            {
                return 0;
            }
            return bet;
        }

        public static int Defeat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nYou lose!");
            return 0;
        }

        public static int Victory()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nYou won!");
            return 1;
        }
    }
}
