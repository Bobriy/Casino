using System;

namespace Casino
{
    class Program
    {
        static void Main(string[] args)
        {
            int cash = 1000;

            while (cash > 0)
            {
                Console.WriteLine("Select game \nr - Roulette \nb - Blackjack \ne - Exit the casino (Coming soon)" +
                " \n\nCash: {0} \n", cash);

                switch (Console.ReadLine())
                {
                    case "r":
                        cash = Roulette.Play(cash);
                        break;
                    case "b":
                        cash = Blackjack.Play(cash);
                        break;
                    case "e":
                        Console.WriteLine("\nCOMING SOON");
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }            
            Console.WriteLine("We are waiting for you again!");
        }
    }
}
