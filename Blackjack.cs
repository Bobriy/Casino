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

        static void GetIntCards(List<object> cards, List<int> cardsInt)
        {
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
            if (playerCardsInt.Sum() > 21)
            {
                for (int i = 0; i < playerCardsInt.Count && playerCardsInt.Sum() > 21; i++)
                {
                    if (playerCardsInt[i] == 11)
                    {
                        playerCardsInt[i] = 1;
                    }
                }
            }
            
            return playerCardsInt;
        }

        static int GetResult(List<int> playerCardsInt, List<int> dealerCardsInt)
        {            
            int dealerCardsSum = dealerCardsInt.Sum();
            int playerCardsSum = playerCardsInt.Sum();

            if (dealerCardsSum == 21)
            {
                return Additional.Defeat();
            }
            else if (playerCardsSum == 21)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nBlackjack!");
                return 2;
            }
            else if (playerCardsSum > 21)
            {                
                return Additional.Defeat();
            }
            else if (dealerCardsSum > 21)
            {
                return Additional.Victory();
            }
            else if (playerCardsSum < dealerCardsSum)
            {
                return Additional.Defeat();
            }            
            else if (playerCardsSum > dealerCardsSum)
            {
                return Additional.Victory();
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
            "Would you like another card? (1 -yes, enter - no)");

            while (cash > 0)
            {
                Random random = new Random();
                List<object> dealerCards = new List<object>();

                int bet = Additional.Bet(cash);
                if (bet == -1)
                {
                    return cash;
                }
                while (bet == 0)
                {
                    bet = Additional.Bet(cash);
                }

                dealerCards.Add(cards[random.Next(0, 13)]);

                Console.Write("\nDealer card: | ");
                PrintCards(dealerCards);

                List<int> dealerCardsInt = new List<int>();
                while (dealerCardsInt.Sum() < 18)
                {
                    dealerCards.Add(cards[random.Next(0, 13)]);
                    GetIntCards(dealerCards, dealerCardsInt);
                }
                GetIntCards(dealerCards, dealerCardsInt);

                List<object> playerCards = new List<object>();
                playerCards.Add(cards[random.Next(0, 13)]);
                playerCards.Add(cards[random.Next(0, 13)]);

                Console.Write("\nYour cards: | ");
                PrintCards(playerCards);

                List<int> playerCardsInt = new List<int>();
                GetIntCards(playerCards,playerCardsInt);
                                               
                while (playerCardsInt.Sum() < 22)
                {
                    Console.WriteLine("\nWould you like another card?");

                    if (Console.ReadLine() == "1")
                    {
                        playerCards.Add(cards[random.Next(0, 13)]);
                        Console.Write("Your cards: | ");
                        PrintCards(playerCards);

                        GetIntCards(playerCards, playerCardsInt);
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
                int victory = GetResult(playerCardsInt, dealerCardsInt);

                Console.ResetColor();

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
                    cash += 2 * bet; 
                }

                Console.WriteLine("Cash: {0}",cash);
            }
            return cash;
        }
    } 
}
