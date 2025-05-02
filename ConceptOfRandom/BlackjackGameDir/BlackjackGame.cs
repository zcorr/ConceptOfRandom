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
            if (GameUtilities.IsPlayAgain())
                BjGameStart(dealerCards, playerCards);
            else
                Console.WriteLine("Thanks for playing!");
    }

    static void BjGameRunner(Hand dealerCards, Hand playerCards, Deck gameDeck)
    {
        // Hand error check
        if(dealerCards.Count < 2 || playerCards.Count < 2)
            throw new Exception("Hand error, not enough cards.");
        //

        // Player Natural Blackjack
        if (BjHandValue(playerCards) == 21)
        {
            Console.WriteLine("Blackjack!");
            if (BjHandValue(dealerCards) != 21 || BjPushCheck(dealerCards,playerCards))
            {
                DisplayBjGameEnd(dealerCards, playerCards);
                return;
            }
            else
            {
                throw new Exception("Something strange happened in a natural blackjack (BjGameRunner)...");
            }
        }
        //
        
        
        // Hit / Stand loop
        while (true)
        {
            PrintHands(dealerCards, playerCards);
            Console.WriteLine("Type 1 to Hit, Type 0 to Stand");
            var hitOrStand = Console.ReadLine();
            while (true)
            {
                if (hitOrStand == "1") {
                    playerCards.Add(gameDeck.DrawCard());
                    break;
                }
                else if (hitOrStand == "0")
                    goto HitStandLoopBreak;
                else
                    Console.WriteLine("Incorrect Input!");
            }
            // Bust or 21
            if (BjHandValue(playerCards) > 21) {
                Console.WriteLine("BUST!");
                DisplayBjGameEnd(dealerCards, playerCards);
                return;
            }
        }
        HitStandLoopBreak:
        //
        
        BjWinCalculation(dealerCards, playerCards, gameDeck);
    }

    internal static void PrintHands(Hand dealerCards, Hand playerCards)
    {
        PrintSingleHand("Dealer's cards:", dealerCards);
        PrintSingleHand("Your cards:", playerCards);
    }

    static void PrintSingleHand(string heading, Hand hand)
    {
        Console.WriteLine(heading);
        hand.Print();
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
    private static void DisplayBjGameEnd(Hand dealerCards, Hand playerCards)
    {
        Console.WriteLine("Final hands:");
        dealerCards.RevealAll();
        Console.WriteLine("Dealer's cards were:");
        dealerCards.Print();
        Console.WriteLine("Your cards were:");
        playerCards.Print();
    }

}