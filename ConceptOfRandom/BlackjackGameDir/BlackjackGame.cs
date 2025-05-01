using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace ConceptOfRandom;

public class BlackjackGame
{
    static readonly List<Card> PlayerCards = new List<Card>();
    static readonly List<Card> DealerCards = new();
    static readonly Deck GameDeck = new Deck();
    static bool _isPlayerWin = false;
    static readonly GameFunctions _gameFunctions = new GameFunctions();

    public static void BjGameStart()
    {
        Console.WriteLine("Welcome to Blackjack!");
        BjGameLooper();
    }

    static void BjGameLooper()
    {
        while (true)
        {
            GameDeck.ShuffleDeck();
            PlayerCards.Clear();
            DealerCards.Clear();

            PlayerCards.Add(GameDeck.DrawCard());
            PlayerCards.Add(GameDeck.DrawCard());

            DealerCards.Add(GameDeck.DrawCard());
            Card dealerDownCard = GameDeck.DrawCard();
            dealerDownCard.IsFaceUp = false;
            DealerCards.Add(dealerDownCard);
            BjGameRunner();
            if (!GameFunctions.IsPlayAgain())
                break;
        }
        
    }

    static void BjGameRunner()
    {
        // Hand error check
        if(DealerCards.Count < 2 || PlayerCards.Count < 2)
        {
            Console.WriteLine("Hand error, not enough cards.");
            throw new Exception();
        }
        //

        // Player Natural Blackjack
        if (BjHandValue(PlayerCards) == 21)
        {
            Console.WriteLine("Blackjack!");
            if (BjHandValue(DealerCards) != 21)
            {
                DisplayGameEnd();
                return;
            }
            else if (BjPushCheck(DealerCards,PlayerCards))
            {
                DisplayGameEnd();
                return;
            }
            else
            {
                Console.WriteLine("Something strange happend in a natural blackjack...");
                throw new Exception();
            }
        }
        //
        
        
        // Hit / Stand loop
        while (true)
        {
            Console.WriteLine("Dealers cards are:");
            HandManipulationFunctions.printHand(DealerCards);
            
            Console.WriteLine("Your cards are:");
            HandManipulationFunctions.printHand(PlayerCards);
            
            
            
            Console.WriteLine("Type 1 to Hit, Press ENTER/RETURN to stand");
            var hitOrStand = Console.ReadLine();
            if (hitOrStand == "1")
                PlayerCards.Add(GameDeck.DrawCard());
            else
            {
                break;
            }
            
            // Bust or 21
            if (BjHandValue(PlayerCards) > 21)
            {
                Console.WriteLine("BUST!");
                DisplayGameEnd();
                return;
            }
                //

        }
        //
        
        BjWinCalculation(DealerCards, PlayerCards, GameDeck);
    }
    
    static void BjWinCalculation(List<Card> dealerCards, List<Card> playerCards, Deck gameDeck)
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
                    DisplayGameEnd();
                    return;
                }
            }
        }
        
        if (BjPushCheck(dealerCards, playerCards))
        {
            DisplayGameEnd();
            return;
        }
        
        int playerValue = BjHandValue(playerCards);
        if (dealerValue <= 21 && dealerValue > playerValue)
        {
            Console.WriteLine("You lose!");
        }
        else
        {
            Console.WriteLine("You win!");
        }

        DisplayGameEnd();
    }

    public static int BjHandValue(List<Card> hand)
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

    public static bool BjPushCheck(List<Card> dealerHand, List<Card> playerHand)
    {
        if (BjHandValue(dealerHand) == 21 && BjHandValue(playerHand) == 21)
        {
            Console.WriteLine("Push!");
            return true;
        }
        return false;
    }
    static void DisplayGameEnd()
    {
        Console.WriteLine("Final hands:");
        HandManipulationFunctions.RevealCards(DealerCards);
        Console.WriteLine("Dealers cards were:");
        HandManipulationFunctions.printHand(DealerCards);
            
        Console.WriteLine("Your cards were:");
        HandManipulationFunctions.printHand(PlayerCards);
    }
}