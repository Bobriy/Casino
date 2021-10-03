using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Casino
{
    static class Blackjack
    {
        static void PrintCards(List<object> cards)
        {
            foreach (var item in cards)
            {
                Console.Write(item + " | ");
            }
        }

        static void NewCard(object[] defaultCards,List<object> cards, List<int> cardsInt)
        {
            cards.Add(defaultCards[new Random().Next(0, 13)]);
            object newcard = cards.Last();

            if (newcard == "jack" || newcard == "queen" || newcard == "king")
            {
                cardsInt.Add(10);
            }
            else if (newcard == "ace")
            {
                cardsInt.Add(11);
            }
            else
            {
                cardsInt.Add((int)newcard);
            }
        }

        static List<int> AceCheck(List<int> playerCardsInt)
        {
            for (int i = 0; i < playerCardsInt.Count && playerCardsInt.Sum() > 21; i++)
            {
                if (playerCardsInt[i] == 11)
                {
                    playerCardsInt[i] = 1;
                }
            }
            return playerCardsInt;
        }

        static int Result(List<int> playerCardsInt, List<int> dealerCardsInt, int bet)
        {            
            int dealerCardsSum = dealerCardsInt.Sum();
            int playerCardsSum = playerCardsInt.Sum();

            if (dealerCardsSum == 21)
            {
                Additional.Defeat();
                return -bet;
            }
            else if (playerCardsSum == 21)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nBlackjack!");
                return bet * 2;
            }
            else if (playerCardsSum > 21)
            {                
                Additional.Defeat();
                return -bet;
            }
            else if (dealerCardsSum > 21)
            {
                Additional.Victory();
                return bet;
            }
            else if (playerCardsSum < dealerCardsSum)
            {
                Additional.Defeat();
                return -bet;
            }            
            else if (playerCardsSum > dealerCardsSum)
            {
                Additional.Victory();
                return bet;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nDraw!");
                return 3;
            }
        }               
        
        public static int Play(int cash)
        {
            object[] cards = { 2, 3, 4, 5, 6, 7, 8, 9, 10, "jack", "queen", "king", "ace" };

            Console.WriteLine("\nRules:\nBet value(integer value - place a bet, e - Exit game) \n" +
            "Would you like another card? (a -yes, enter - no)");

            while (cash > 0)
            {                
                List<object> dealerCards = new List<object>();
                List<int> dealerCardsInt = new List<int>();

                int bet = Additional.Bet(cash);
                if (bet == -1)
                {
                    return cash;
                }
                while (bet == 0)
                {
                    bet = Additional.Bet(cash);
                }

                NewCard(cards, dealerCards, dealerCardsInt);

                Console.Write("\nDealer card: | ");
                PrintCards(dealerCards);

                while (dealerCardsInt.Sum() < 18)
                {
                    NewCard(cards, dealerCards, dealerCardsInt);
                }

                List<object> playerCards = new List<object>();
                List<int> playerCardsInt = new List<int>();

                NewCard(cards, playerCards, playerCardsInt);
                NewCard(cards, playerCards, playerCardsInt);

                Console.Write("\nYour cards: | ");
                PrintCards(playerCards);             
                
                while (playerCardsInt.Sum() < 22)
                {
                    Console.WriteLine("\nWant more?");

                    if (Console.ReadLine() == "a")
                    {
                        NewCard(cards, playerCards, playerCardsInt);
                        Console.Write("\nYour cards: | \n");
                        PrintCards(playerCards);         
                        
                        AceCheck(playerCardsInt);
                    }
                    else
                    {
                        Console.WriteLine();
                        break;
                    }
                }

                Console.Write("Dealer cards: | ");
                PrintCards(dealerCards);

                cash += Result(playerCardsInt, dealerCardsInt, bet);

                Console.ResetColor();               
                Console.WriteLine("Cash: {0}",cash);
            }
            return cash;
        }
    } 
}
