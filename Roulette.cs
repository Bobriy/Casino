using System;
using System.Collections.Generic;
using System.Text;

namespace Casino
{
    static class Roulette
    {    
        static void PrintResult(string color, int result, int victory, int cash)
        {
            Console.WriteLine();
            if (result != 0)
            {
                switch (color)
                {
                    case "b":
                        Console.Write("Black, ");
                        break;
                    case "r":
                        Console.Write("Red, ");
                        break;
                }
            }
            Console.WriteLine(result);

            if (victory == 1 || victory == 2)
            {
                Additional.Victory();
            }
            else
            {
                Additional.Defeat();
            }
            Console.ResetColor();
            Console.WriteLine("Cash: {0}", cash);
        }

        static int Set( int result, string color)
        {
            Console.WriteLine("Place:");
            string inputstr = Console.ReadLine();

            if (int.TryParse(inputstr, out int inputint))
            {
                if (inputint == 0 || inputint > 36)
                {
                    return 3;
                }
                if (result == 0)
                {
                    return 0;
                }
                if (inputint == result)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                if (inputstr ! == "b" && inputstr ! == "r")
                {
                    return 3;
                }
                if (inputstr == color)
                {
                    return 1;
                }
                else
                {
                    return 0;                   
                }
            }
        }
        public static int Play(int cash)
        {
            int[] RedValues =
            { 0, 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };

            int[] NigValues =
            { 0, 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

            string color;
            int result;

            Console.WriteLine("\nRules:\nBet value(integer value - place a bet, e - Exit game) \n" +
            "Place of the bet(r - red, b - black, value from 1 to 36)");

            while (cash > 0)
            {
                Random random = new Random();

                if (random.Next(0, 2) == 1)
                {
                    result = RedValues[random.Next(0, 18)];
                    color = "r";
                }
                else
                {
                    result = NigValues[random.Next(0, 18)];
                    color = "b";
                }

                int bet = Additional.Bet(cash);

                if (bet == -1)
                {
                    return cash;
                }
                while (bet == 0)
                {
                    bet = Additional.Bet(cash);
                }                                             

                int victory = 3;
                while (victory == 3)
                {
                    victory = Set(result, color);
                }

                if (victory == 0)
                {
                    cash -= bet;
                }
                else if (victory == 1)
                {
                    cash += bet;
                }
                else if (victory == 2)
                {
                    cash += 36 * bet;
                }


                cash += bet;
                PrintResult(color, result, victory, cash);
            }
            return cash;
        }        
    }
}
