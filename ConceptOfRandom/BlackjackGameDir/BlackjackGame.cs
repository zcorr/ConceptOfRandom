using System.Runtime.CompilerServices;
using ConceptOfRandom.CardGameDir;

[assembly: InternalsVisibleTo("ConceptOfRandomTests")]
namespace ConceptOfRandom.BlackjackGameDir;

public class BlackjackGame
{
    static readonly Hand PlayerCards = new Hand();
    static readonly Hand DealerCards = new Hand();
    static readonly Deck GameDeck;
    
    public BlackjackGame()
    {
        BjGameStart(DealerCards, PlayerCards);
    }

    static BlackjackGame()
    {
        GameDeck = new Deck();
    }

    static void BjGameStart(Hand dealerCards, Hand playerCards)
    {
        Console.WriteLine("Welcome to Blackjack!\n");
        Console.WriteLine("Choose your display style:");
        Console.WriteLine("  1) Word Display art");
        Console.WriteLine("  2) Symbol Display art");
        var choice = Console.ReadLine();
        CardDisplay.Current = 
            choice == "1" ? new WordDisplayStrategy() : new SymbolDisplayStrategy();
        
        BjGameSetUp(dealerCards, playerCards);
    }

    static void BjGameSetUp(Hand dealerCards, Hand playerCards)
    {
            var gameDeck = new Deck();
            gameDeck.Shuffle();
            playerCards.Clear();
            dealerCards.Clear();

            playerCards.Add(gameDeck.DrawCard());
            playerCards.Add(gameDeck.DrawCard());

            dealerCards.Add(gameDeck.DrawCard());
            Card dealerDownCard = gameDeck.DrawCard();
            dealerDownCard.IsFaceUp = false;
            dealerCards.Add(dealerDownCard);
            BjGameRunner(dealerCards, playerCards, gameDeck);

            Console.WriteLine("Would you like to play again? (Y/N)");
            if (GameUtilities.IsPlayAgain(Console.ReadLine, Console.WriteLine))
                BjGameStart(dealerCards, playerCards);
    }

    // static void BjGameRunner(Hand dealerCards, Hand playerCards, Deck gameDeck)
    // {
    //     // Hand error check
    //     if(dealerCards.Count < 2 || playerCards.Count < 2)
    //         throw new Exception("Hand error, not enough cards.");
    //     //
    //
    //     // Player Natural Blackjack
    //     if (BjHandValue(playerCards) == 21)
    //     {
    //         Console.WriteLine("Blackjack!");
    //         if (BjHandValue(dealerCards) != 21 || BjPushCheck(dealerCards,playerCards))
    //         {
    //             DisplayBjGameEnd(dealerCards, playerCards);
    //             return;
    //         }
    //         else
    //         {
    //             throw new Exception("Something strange happened in a natural blackjack (BjGameRunner)...");
    //         }
    //     }
    //     //
    //     
    //     
    //     // Hit / Stand loop
    //     while (true)
    //     {
    //         PrintHands(dealerCards, playerCards);
    //         Console.WriteLine("Type 1 to Hit, Type 0 to Stand");
    //         var hitOrStand = Console.ReadLine();
    //         while (true)
    //         {
    //             if (hitOrStand == "1") {
    //                 playerCards.Add(gameDeck.DrawCard());
    //                 break;
    //             }
    //             else
    //             {
    //                 goto HitStandLoopBreak;
    //             }
    //         }
    //         // Bust or 21
    //         if (BjHandValue(playerCards) > 21) {
    //             Console.WriteLine("BUST!");
    //             DisplayBjGameEnd(dealerCards, playerCards);
    //             return;
    //         }
    //     }
    //     HitStandLoopBreak:
    //     //
    //     
    //     BjWinCalculation(dealerCards, playerCards, gameDeck);
    // }
    
    internal static void BjGameRunner(Hand dealerCards, Hand playerCards, Deck gameDeck)
    {
        // …Natural-BJ check stays unchanged…

        // ---------- Hit / Stand loop ----------
        while (true)
        {
            PrintHands(dealerCards, playerCards);

            // ⇢  NEW: stop prompting if the player already has 21
            if (BjHandValue(playerCards) == 21)
            {
                Console.WriteLine("21! You stand automatically.");
                break;                          // jump to dealer’s turn
            }

            Console.WriteLine("Type 1 to Hit, 0 to Stand");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                playerCards.Add(gameDeck.DrawCard());

                var value = BjHandValue(playerCards);
                if (value > 21)
                {
                    Console.WriteLine("BUST!");
                    DisplayBjGameEnd(dealerCards, playerCards);
                    return;                     // game over
                }

                if (value == 21)                // hit to exactly 21
                {
                    Console.WriteLine("21! You stand automatically.");
                    break;                      // dealer’s turn
                }

                // otherwise loop again
            }
            else   // user chose Stand (or anything other than "1")
            {
                break;
            }
        }
        // ---------- dealer’s turn / outcome ----------
        BjWinCalculation(dealerCards, playerCards, gameDeck);
    }


    internal static string PrintHands(Hand dealerCards, Hand playerCards)
    {
        // Each helper call writes its own heading + hand to the console
        // and returns the hand text.
        var dealerHandText = PrintSingleHand("Dealer's cards:", dealerCards);
        var playerHandText = PrintSingleHand("Your cards:",    playerCards);

        // Return the concatenation of the two hand strings (no extra newlines).
        return dealerHandText + playerHandText;
    }

    internal static string PrintSingleHand(string heading, Hand hand)
    {
        var handText = hand.ToString();          // e.g. “Two♠  Three♠”

        Console.WriteLine(heading);              // “Dealer's cards:”
        Console.WriteLine(handText);             // “Two♠  Three♠”

        return handText;                         // caller can use it if needed
    }


    internal static void BjWinCalculation(Hand dealerCards, Hand playerCards, Deck gameDeck)
    {
        int dealerValue = BjHandValue(dealerCards);
        
        while (dealerValue < 17)
        {
            dealerCards.Add(gameDeck.DrawCard());
            dealerValue = BjHandValue(dealerCards);
            if (dealerValue >= 21)
            {
                if (BjPushCheck(dealerCards, playerCards))
                {
                    DisplayBjGameEnd(dealerCards, playerCards);
                    return;
                }
            }
        }
        
        if (BjPushCheck(dealerCards, playerCards))
        {
            DisplayBjGameEnd(dealerCards, playerCards);
            return;
        }
        
        int playerValue = BjHandValue(playerCards);
        if (dealerValue <= 21 && dealerValue > playerValue)
            Console.WriteLine("You lose!");
        else
            Console.WriteLine("You win!");
        DisplayBjGameEnd(dealerCards, playerCards);
    }

    internal static int BjHandValue(Hand hand)
    {
        if (hand.Count <= 1) throw new ArgumentException("Hand is too short, error.");
        int handValue = 0;
        int aceCount = 0;
        foreach (Card c in hand)
        {
            if (c.Value == 14)
            {
                aceCount++;
                handValue += 1;  
            }
            else if (c.Value >= 11 && c.Value <= 13)
                handValue += 10;
            else
                handValue += c.Value;
        }
        while (aceCount > 0 && handValue <= 11)
        {
            handValue += 10;   
            aceCount--;
        }
        return handValue;

    }

    internal static bool BjPushCheck(Hand dealerHand, Hand playerHand)
    {
        if (BjHandValue(dealerHand) == 21 && BjHandValue(playerHand) == 21)
        {
            Console.WriteLine("Push!");
            return true;
        }
        return false;
    }
    static string DisplayBjGameEnd(Hand dealerCards, Hand playerCards)
    {
        dealerCards.RevealAll();
        var output = "";
        output += "Final hands:\n";
        output += "Dealer's cards were:\n";
        output += dealerCards+"\n";
        output += "Your cards were:\n";
        output += playerCards+"\n";
        Console.WriteLine(output);
        return output;
    }
}