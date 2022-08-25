using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_CardGame
{
    public class Deck
    {
        public List<Card> TheDeck = new List<Card>();
        public List<Card> Player1 = new List<Card>();
        public List<Card> Player2 = new List<Card>();
        public List<Card> Discard1 = new List<Card>();
        public List<Card> Discard2 = new List<Card>();
        public List<Card> Pile = new List<Card>();

        Ascii ascii = new Ascii();
        List<string> player1Card = new List<string>();
        List<string> player2Card = new List<string>();

        List<string> playerCard = new List<string>();

        public void FillDeck()
        {
            //Single loop 
            //Using divition based on 13 cards in a suited
            for (int i = 0; i < 52; i++)
            {
                Suites suite = (Suites)(Math.Floor((decimal)i / 13));
                //Add 2 to val as a cards start at value of 2
                int val = i % 13 + 2;
                TheDeck.Add(new Card(val, suite));
            }
        }

        public void ShuffleDeck()
        {
            Random r = new Random();

            int n = TheDeck.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                Card value = TheDeck[k];
                TheDeck[k] = TheDeck[n];
                TheDeck[n] = value;
            }
        }

        public void PrintDeck()
        {
            Console.Clear();

            // C# program that implements generic Fisher-Yates shuffle
            foreach (Card card in TheDeck)
            {
                Console.WriteLine(card.Name);

                if (card.Suite == Suites.Clubs)
                {
                    playerCard = ascii.Clubs(card.CardValue);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (card.Suite == Suites.Diamonds)
                {
                    playerCard = ascii.Diamonds(card.CardValue);
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (card.Suite == Suites.Hearts)
                {
                    playerCard = ascii.Hearts(card.CardValue);
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (card.Suite == Suites.Spades)
                {
                    playerCard = ascii.Spades(card.CardValue);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine($"{playerCard[i]}");
                }

                Console.ResetColor();

                Console.WriteLine($"has a value of: {card.Value}\n");
            }

            int size = TheDeck.Count;
            Console.WriteLine($"Number of cards: {size}");
        }

        public void Deal()
        {
            Player1 = TheDeck.Where((x, n) => n % 2 == 0).ToList();

            Player2 = TheDeck.Where((x, n) => n % 2 != 0).ToList();

            // Console.WriteLine(Player1[0].Name);
            // Console.WriteLine(Player2[0].Name);

            // Console.WriteLine($"Player 1: {Player1.Count} cards");
            // Console.WriteLine($"Player 2: {Player2.Count} cards");

            
        }

        static int turncount;

        public void PlayWar()
        {
            while (Player1.Count != 0 && Player2.Count != 0)
            {
                Console.WriteLine(String.Format("{0, -26} {1, 15}", $"\nPlayer 1: {Player1.Count} cards;", $"Player 2: {Player2.Count} cards"));
                Console.WriteLine(String.Format("{0, -25} {1, 15} {2, 15}", $"Player 1: {Discard1.Count} discard;", $"Player 2: {Discard2.Count} discard;", $"Pile: {Pile.Count}"));
                PauseAndWaitForKeypress();

                // Console.WriteLine($"Player 1 draws a(n) {Player1[0].Name} and Player 2 draws a(n) {Player2[0].Name}.");
                FaceCard();


                if (Player1[0].Value > Player2[0].Value)
                {
                    Player1WinsHand();

                    Discard1.Add(Player1[0]);
                    Discard1.Add(Player2[0]);

                    Player1.Remove(Player1[0]);
                    Player2.Remove(Player2[0]);

                    Discard1.AddRange(Pile);
                    Pile.Clear();
                }

                else if (Player1[0].Value < Player2[0].Value)
                {
                    Player2WinsHand();

                    Discard2.Add(Player2[0]);
                    Discard2.Add(Player1[0]);

                    Player2.Remove(Player2[0]);
                    Player1.Remove(Player1[0]);

                    Discard2.AddRange(Pile);
                    Pile.Clear();
                }

                else if (Player1[0].Value == Player2[0].Value)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"War has been declared!\n");
                    Console.ResetColor();

                    if (Player1.Count < 5)
                    {
                        Console.WriteLine("Player 1 reloads hand...");
                        Discard1.AddRange(Pile);
                        Pile.Clear();
                        Player1.AddRange(Discard1);
                        Discard1.Clear();

                        if (Player1.Count < 5)
                        {
                            Player1Loses();
                            return;
                        }
                    }

                    if (Player2.Count < 5)
                    {
                        Console.WriteLine("Player 2 reloads hand...");
                        Discard2.AddRange(Pile);
                        Pile.Clear();
                        Player2.AddRange(Discard2);
                        Discard2.Clear();

                        if (Player2.Count < 5)
                        {
                            Player2Loses();
                            return;
                        }
                    }

                    Console.WriteLine($"Players place 4 additional cards into pile showing last card.");
                    PauseAndWaitForKeypress();
                    // Console.WriteLine($"Player 1 draws a(n) {Player1[4].Name} and Player 2 draws a(n) {Player2[4].Name} into the pile.");
                    FaceWarCard();

                    if (Player1[4].Value > Player2[4].Value)
                    {
                        Player1WinsWar();
                        Discard1.Add(Player1[0]);
                        Discard1.Add(Player1[1]);
                        Discard1.Add(Player1[2]);
                        Discard1.Add(Player1[3]);
                        Discard1.Add(Player1[4]);
                        Discard1.Add(Player2[0]);
                        Discard1.Add(Player2[1]);
                        Discard1.Add(Player2[2]);
                        Discard1.Add(Player2[3]);
                        Discard1.Add(Player2[4]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);

                        Discard1.AddRange(Pile);
                        Pile.Clear();
                    }

                    else if (Player1[4].Value < Player2[4].Value)
                    {
                        Player2WinsWar();
                        Discard2.Add(Player1[0]);
                        Discard2.Add(Player1[1]);
                        Discard2.Add(Player1[2]);
                        Discard2.Add(Player1[3]);
                        Discard2.Add(Player1[4]);
                        Discard2.Add(Player2[0]);
                        Discard2.Add(Player2[1]);
                        Discard2.Add(Player2[2]);
                        Discard2.Add(Player2[3]);
                        Discard2.Add(Player2[4]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);

                        Discard2.AddRange(Pile);
                        Pile.Clear();
                    }

                    else if (Player1[4].Value == Player2[4].Value)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Another War has been declared!");
                        Console.ResetColor();

                        Pile.Add(Player1[0]);
                        Pile.Add(Player1[1]);
                        Pile.Add(Player1[2]);
                        Pile.Add(Player1[3]);
                        Pile.Add(Player1[4]);

                        Pile.Add(Player2[0]);
                        Pile.Add(Player2[1]);
                        Pile.Add(Player2[2]);
                        Pile.Add(Player2[3]);
                        Pile.Add(Player2[4]);

                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);

                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);

                        // Console.WriteLine(String.Format("{0, -26} {1, 15}", $"Player 1: {Player1.Count} cards;", $"Player 2: {Player2.Count} cards"));
                        // Console.WriteLine(String.Format("{0, -25} {1, 15} {2, 15}", $"Player 1: {Discard1.Count} discard;", $"Player 2: {Discard2.Count} discard;", $"Pile: {Pile.Count}"));

                        if (Player1.Count <= 4)
                        {
                            Console.WriteLine("Player 1 reloads hand...");
                            Player1.AddRange(Discard1);
                            Discard1.Clear();

                            if (Player1.Count < 4)
                            {
                                Player1Loses();
                                return;
                            }
                        }

                        if (Player2.Count <= 4)
                        {
                            Console.WriteLine("Player 2 reloads hand...");
                            Player2.AddRange(Discard2);
                            Discard2.Clear();

                            if (Player2.Count < 4)
                            {
                                Player2Loses();
                                return;
                            }
                        }

                        Pile.Add(Player1[0]);
                        Pile.Add(Player1[1]);
                        Pile.Add(Player1[2]);
                        Pile.Add(Player1[3]);

                        Pile.Add(Player2[0]);
                        Pile.Add(Player2[1]);
                        Pile.Add(Player2[2]);
                        Pile.Add(Player2[3]);

                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);
                        Player1.Remove(Player1[0]);

                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);
                        Player2.Remove(Player2[0]);

                        // Console.WriteLine(String.Format("{0, -26} {1, 15}", $"Player 1: {Player1.Count} cards;", $"Player 2: {Player2.Count} cards"));
                        // Console.WriteLine(String.Format("{0, -25} {1, 15} {2, 15}", $"Player 1: {Discard1.Count} discard;", $"Player 2: {Discard2.Count} discard;", $"Pile: {Pile.Count}"));
                    }

                    turncount++;
                }

                if (Player1.Count == 0)
                {
                    Console.WriteLine("Player 1 reloads hand...");
                    Player1.AddRange(Discard1);
                    Discard1.Clear();
                }

                if (Player2.Count == 0)
                {
                    Console.WriteLine("Player 2 reloads hand...");
                    Player2.AddRange(Discard2);
                    Discard2.Clear();
                }

                turncount++;
            }
            if (Player1.Count == 0)
            {
                Player2Wins();
            }

            if (Player2.Count == 0)
            {
                Player1Wins();
            }
        }

        private static void PauseAndWaitForKeypress()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            System.Console.WriteLine("Press any key to draw a card.\n");
            Console.ResetColor();
            Console.ReadKey();
        }
        
        private static void Player1WinsHand()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Player 1 wins the hand!");
            Console.ResetColor();
        }

        private static void Player2WinsHand()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Player 2 wins the hand!");
            Console.ResetColor();
        }

        private static void Player1Loses()
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine($"\nAfter {turncount} turns, Player 1 has lost the game.");
            Console.ResetColor();
        }

        private static void Player2Loses()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nAfter {turncount} turns, Player 2 has lost the game.");
            Console.ResetColor();
        }

        private static void Player1WinsWar()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Player 1 wins the hand of War!");
            Console.ResetColor();
        }

        private static void Player2WinsWar()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Player 2 wins the hand of War!");
            Console.ResetColor();
        }

        private static void Player1Wins()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nAfter {turncount} turns, Player 1 wins the game!");
            Console.ResetColor();
        }

        private static void Player2Wins()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nAfter {turncount} turns, Player 2 wins the game!");
            Console.ResetColor();
        }

        //Take in two cards and print out to variable
        public void FaceCard()
        {
            // something to show the correct card = Player1[0].Name
            if (Player1[0].Suite == Suites.Clubs)
            {
                player1Card = ascii.Clubs(Player1[0].CardValue);
            }
            else if (Player1[0].Suite == Suites.Diamonds)
            {
                player1Card = ascii.Diamonds(Player1[0].CardValue);
            }
            else if (Player1[0].Suite == Suites.Spades)
            {
                player1Card = ascii.Spades(Player1[0].CardValue);
            }
            else if (Player1[0].Suite == Suites.Hearts)
            {
                player1Card = ascii.Hearts(Player1[0].CardValue);
            }
            if (Player2[0].Suite == Suites.Clubs)
            {
                player2Card = ascii.Clubs(Player2[0].CardValue);
            }
            else if (Player2[0].Suite == Suites.Diamonds)
            {
                player2Card = ascii.Diamonds(Player2[0].CardValue);
            }
            else if (Player2[0].Suite == Suites.Spades)
            {
                player2Card = ascii.Spades(Player2[0].CardValue);
            }
            else if (Player2[0].Suite == Suites.Hearts)
            {
                player2Card = ascii.Hearts(Player2[0].CardValue);
            }
            
            for (int i = 0; i < 6; i++)
            {
                if (Player1[0].Suite == Suites.Clubs && Player2[0].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Clubs && Player2[0].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Spades && Player2[0].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Spades && Player2[0].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Diamonds && Player2[0].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Diamonds && Player2[0].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Hearts && Player2[0].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Hearts && Player2[0].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Clubs && Player2[0].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Clubs && Player2[0].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Spades && Player2[0].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Spades && Player2[0].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Diamonds && Player2[0].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Diamonds && Player2[0].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Hearts && Player2[0].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[0].Suite == Suites.Hearts && Player2[0].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
            }
            
            Console.ResetColor();
        }

        public void FaceWarCard()
        {
            // something to show the correct card = Player1[0].Name
            if (Player1[4].Suite == Suites.Clubs)
            {
                player1Card = ascii.Clubs(Player1[4].CardValue);
            }
            else if (Player1[4].Suite == Suites.Diamonds)
            {
                player1Card = ascii.Diamonds(Player1[4].CardValue);
            }
            else if (Player1[4].Suite == Suites.Spades)
            {
                player1Card = ascii.Spades(Player1[4].CardValue);
            }
            else if (Player1[4].Suite == Suites.Hearts)
            {
                player1Card = ascii.Hearts(Player1[4].CardValue);
            }
            if (Player2[4].Suite == Suites.Clubs)
            {
                player2Card = ascii.Clubs(Player2[4].CardValue);
            }
            else if (Player2[4].Suite == Suites.Diamonds)
            {
                player2Card = ascii.Diamonds(Player2[4].CardValue);
            }
            else if (Player2[4].Suite == Suites.Spades)
            {
                player2Card = ascii.Spades(Player2[4].CardValue);
            }
            else if (Player2[4].Suite == Suites.Hearts)
            {
                player2Card = ascii.Hearts(Player2[4].CardValue);
            }

            for (int i = 0; i < 6; i++)
            {
                if (Player1[4].Suite == Suites.Clubs && Player2[4].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Clubs && Player2[4].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Spades && Player2[4].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Spades && Player2[4].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Diamonds && Player2[4].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Diamonds && Player2[4].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Hearts && Player2[4].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Hearts && Player2[4].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Clubs && Player2[4].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Clubs && Player2[4].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Spades && Player2[4].Suite == Suites.Spades)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Spades && Player2[4].Suite == Suites.Clubs)
                {
                    ColoredConsoleWrite(ConsoleColor.White, $"{player1Card[i]}   ", ConsoleColor.White, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Diamonds && Player2[4].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Diamonds && Player2[4].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Hearts && Player2[4].Suite == Suites.Hearts)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
                else if (Player1[4].Suite == Suites.Hearts && Player2[4].Suite == Suites.Diamonds)
                {
                    ColoredConsoleWrite(ConsoleColor.Red, $"{player1Card[i]}   ", ConsoleColor.Red, $"{player2Card[i]}");
                }
            }

            Console.ResetColor();
        }

        public void ColoredConsoleWrite(ConsoleColor firstColor, string firstText, ConsoleColor secondColor, string secondText)
        {
            Console.ForegroundColor = firstColor;
            Console.Write(firstText);
            Console.ForegroundColor = secondColor;
            Console.WriteLine(secondText);
        }
    }
}


/*
public void CheckForCards()
        {
            if (Player1.Count < 5)
            {
                Console.WriteLine("Player 1 reloads hand");
                Discard1.AddRange(Pile);
                Pile.Clear();
                Player1.AddRange(Discard1);
                Discard1.Clear();
                if (Player1.Count < 5)
                {
                    Console.WriteLine("Player 1 has lost the game");
                    return;
                }

            }
            if (Player2.Count < 5)
            {
                Console.WriteLine("Player 2 reloads hand");
                Discard2.AddRange(Pile);
                Pile.Clear();
                Player2.AddRange(Discard2);
                Discard2.Clear();
                if (Player2.Count < 5)
                {
                    Console.WriteLine("Player 2 has lost the game");
                    return;
                }
            }
*/