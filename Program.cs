using System;
using System.IO;
using System.Text;

namespace Casino
{
    class Program
    {
        static void Main(string[] args)
        {            
            int cash = 1000;

            while (cash > 0)
            {
                Console.WriteLine("Select game \nr - Roulette \nb - Blackjack \ne - Exit the casino" +
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
                        End(cash);
                        break;                       
                    default:
                        Console.WriteLine();
                        break;
                }
            }
            Console.WriteLine("LOH!");
        }

        public static int Record { get; set; }
        static void End(int cash)
        {
            using (var sw = new StreamWriter("record.txt", true)){}
            
            using (var sr = new StreamReader("record.txt"))
            {
                if (int.TryParse(sr.ReadLine(), out int record))
                {
                    Record = record;
                }
            }

            using (var sw = new StreamWriter("record.txt"))
            {
                if (cash > Record)
                {
                    sw.Flush();
                    sw.WriteLine(cash);
                    Console.WriteLine("New record!");
                }
                else
                {
                    sw.WriteLine(Record);
                    Console.WriteLine($"\nRecord: {Record}");
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nRestart\n");
            Console.ResetColor();
        }
    }
}
