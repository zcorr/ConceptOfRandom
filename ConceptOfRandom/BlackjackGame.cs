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
    public static void BjGameStart()
    {
        Console.WriteLine("Welcome to Blackjack!");
        BuildBlackjackStartingHands();
    }
    static void BuildBlackjackStartingHands()
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
        BjRunGame();
    }
    static void BjRunGame()
    {
        while (true)
        {
            Console.WriteLine("Dealers cards are:");
            HandManipulationFunctions.printHand(DealerCards);
            
            Console.WriteLine("Your cards are:");
            HandManipulationFunctions.printHand(PlayerCards);
            
            Console.WriteLine("Type 1 to Hit, Type 0 to Stand");
            
            string? hitOrStand = Console.ReadLine();
            if(hitOrStand == "1")
                PlayerCards.Add(GameDeck.DrawCard());
            else // just default to stand
            {
                bool isWin = BjWinCalculation();
                if (isWin)
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("You lose!");
                }

                if (!IsPlayAgain())
                {
                    BuildBlackjackStartingHands();
                    break;
                }
            }
        }
    }

    static bool IsPlayAgain()
    {
        Console.WriteLine("Would you like to play again? (Y/N)");
        string? playAgain = Console.ReadLine();

        if (playAgain == "Y")
        {
            Console.WriteLine("Game starting...");
            return true;
        }
        else
        {
            Console.WriteLine("Thanks for playing!");
            return false;
        }
    }
    static bool BjWinCalculation()
    {
        int dealerValue = BjCalculateHandValue(DealerCards);
        while (dealerValue < 17)
        {
            DealerCards.Add(GameDeck.DrawCard());
            dealerValue = BjCalculateHandValue(DealerCards);
            if (dealerValue > 21)
            {
                _isPlayerWin = true;
                return _isPlayerWin;
            }
        }
        HandManipulationFunctions.RevealCards(DealerCards);
        
        int playerValue = BjCalculateHandValue(PlayerCards);
        
        HandManipulationFunctions.RevealCards(DealerCards);
        Console.WriteLine("Dealer Hand:");
        HandManipulationFunctions.printHand(DealerCards);
            
        Console.WriteLine("Player Hand:");
        HandManipulationFunctions.printHand(PlayerCards);
        
        if (playerValue == 21 && dealerValue == 21 &&! _isPlayerWin)
        {
            Console.WriteLine("21 Tie!");
            return true;
        }
        else if (playerValue > 21)
        {
            _isPlayerWin = false;
        }
        else if (playerValue == 21)
        {
            return _isPlayerWin = true;
        }
        else
        {
            _isPlayerWin = dealerValue <= playerValue;
        }
        return _isPlayerWin;
    }
    static int BjCalculateHandValue(List<Card> hand)
    {
        if(hand.Count <= 1) throw new ArgumentException("Hand is too short, error.");
        int handValue = 0;
        int aceCount=0;
        foreach (Card c in hand) {
            if(c.Value == 14)
            {
                aceCount++;
                handValue += 11;
            }
            else if(c.Value>=11 && c.Value <= 13)
            {
                handValue += 10;
            }
            else
            {
                handValue += c.Value;
            }
        }
        while (aceCount > 0 && handValue > 21)
        {
            aceCount--;
            handValue -= 10;
        }
        return handValue;
    }
}