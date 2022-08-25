using System;
using System.Collections.Generic;
using War_CardGame;

public class Program
{
    public static void Main()
    {
        Deck deck = new Deck();

        bool continueToRun = true;
        while (continueToRun)
        {
            Console.Clear();
            Console.WriteLine(
                "WAR - Game Menu\n" +
                "1. View Playing Cards\n" +
                "2. Start Game\n" +
                "0. Exit"
                );

            string selection = Console.ReadLine() ?? "";
            switch (selection)
            {
                case "1":
                    ViewDeck();
                    break;
                case "2":
                    StartGame();
                    break;
                case "0":
                    continueToRun = false;
                    break;
                default:
                    Console.WriteLine("Please enter a valid selection.");
                    PauseAndWaitForKeypress();
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }

    public static void ViewDeck()
    {
        Console.Clear();
        Deck deck = new Deck();
        deck.FillDeck();
        deck.PrintDeck();

        PauseAndWaitForKeypress();
    }

    public static void StartGame()
    {
        Console.Clear();
        Deck deck = new Deck();
        deck.FillDeck();

        // Player Player2 = new Player("Oponent");
        // Console.Write("Enter a name: ");
        // string name = Console.ReadLine() ?? "Unknown Player";
        // Player Player1 = new Player(name);
        
        Console.WriteLine("Welcome, Player 1!\n");

        Console.Write("Enter 'S' to shuffle the cards. ");
        string responseS = Console.ReadLine() ?? "";
        if (responseS == "S" || responseS == "s")
        {
            deck.ShuffleDeck();
        }

        Console.Write("Enter 'D' to deal the cards. ");
        string responseD = Console.ReadLine() ?? "";
        if (responseD == "D" || responseD == "d")
        {
            deck.Deal();
        }

        /*
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Press any key to start the game.");
        Console.ResetColor();
        var key = Console.ReadKey();
        if (key != null)
        {
            deck.PlayWar();
        }
        */

        deck.PlayWar();
        
        PauseAndWaitForKeypress();

        Console.WriteLine("Good game!");
    }


    private static void PauseAndWaitForKeypress()
    {
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
