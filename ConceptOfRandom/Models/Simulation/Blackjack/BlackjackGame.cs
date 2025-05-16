using System.Runtime.CompilerServices;
using ConceptOfRandom.Models.Simulation.Blackjack.Display_Strategy;
using ConceptOfRandom.Models.Simulation.Blackjack.Enums;
using ConceptOfRandom.Models.Simulation.Blackjack.Objects;
using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

[assembly: InternalsVisibleTo("ConceptOfRandomTests")]

namespace ConceptOfRandom.Models.Simulation.Blackjack;

public class BlackjackGame
{
    static readonly Hand PlayerCards = new Hand();
    static readonly Hand DealerCards = new Hand();
    static readonly Deck GameDeck;
    private static TurnOptions turnDecision = TurnOptions.Undefined;
    
    public BlackjackGame()
    {
        StartBlackjackGame(DealerCards, PlayerCards);
    }

    static BlackjackGame()
    {
        GameDeck = new Deck();
    }

    public static void StartBlackjackGame(Hand dealerCards, Hand playerCards)
    {
        do {
            DisplayWelcomeMessage();
            AskUserForDisplayStyle();
            var gameDeck = SetUpNewGameDeck(dealerCards, playerCards);
            DrawPlayerCards(playerCards, gameDeck);
            DrawDealerCard(dealerCards, gameDeck);
            DrawDealerCardFaceDown(dealerCards, gameDeck);
            PlayBlackjack(dealerCards, playerCards, gameDeck);
            DisplayPlayAgainPrompt();
        } while (GameUtilities.IsPlayAgain(Console.ReadLine, Console.WriteLine));
    }

    private static void DisplayPlayAgainPrompt() {
        Console.WriteLine("Would you like to play again? (Y/N)");
    }

    private static void DrawDealerCardFaceDown(Hand dealerCards, Deck gameDeck) {
        Card faceDownDealerCard = gameDeck.DrawCard();
        faceDownDealerCard.IsFaceUp = false;
        dealerCards.Add(faceDownDealerCard);
    }

    private static void DrawDealerCard(Hand dealerCards, Deck gameDeck) {
        dealerCards.Add(gameDeck.DrawCard());
    }

    private static void DrawPlayerCards(Hand playerCards, Deck gameDeck) {
        DrawCardForPlayer(playerCards, gameDeck);
        DrawCardForPlayer(playerCards, gameDeck);
    }

    private static void DrawCardForPlayer(Hand playerCards, Deck gameDeck) {
        playerCards.Add(gameDeck.DrawCard());
    }

    private static Deck SetUpNewGameDeck(Hand dealerCards, Hand playerCards) {
        var gameDeck = new Deck();
        gameDeck.Shuffle();
        playerCards.Clear();
        dealerCards.Clear();
        return gameDeck;
    }

    private static void AskUserForDisplayStyle() {
        Console.WriteLine("Choose your display style:");
        Console.WriteLine("  1) Word Display art");
        Console.WriteLine("  2) Symbol Display art");
        var choice = Console.ReadLine();
        if (choice == "1")
            CardDisplay.Current =
                new WordDisplayStrategy();
        else
            CardDisplay.Current =
                new SymbolDisplayStrategy();
    }

    private static void DisplayWelcomeMessage() {
        Console.WriteLine("Welcome to Blackjack!\n");
    }
    
    public static void PlayBlackjack(Hand dealerCards, Hand playerCards, Deck gameDeck)
    {
        while (true) {
            turnDecision = TurnOptions.Undefined;
            PrintHands(dealerCards, playerCards);
            var playerHandValue = GetHandValue(playerCards);
            bool playerHasBlackjack = (playerHandValue == 21);
            StandIfPlayerHasBlackjack(playerHasBlackjack);

            if (turnDecision == TurnOptions.Stand) break;

            Console.WriteLine("Type 1 to Hit, 0 to Stand");
            var choice = Console.ReadLine();
            if(choice == "1") turnDecision = TurnOptions.Hit;
            else if(turnDecision == TurnOptions.Undefined) turnDecision = TurnOptions.Stand;
            if (turnDecision == TurnOptions.Hit) {
                DrawCardForPlayer(playerCards, gameDeck);
                playerHandValue = GetHandValue(playerCards);
                bool playerHasBusted = (playerHandValue > 21);
                if (playerHasBusted) {
                    EndGameIfPlayerBusted(dealerCards, playerCards);
                    return;
                }

                StandIfPlayerHasBlackjack(playerHasBlackjack);
                if (turnDecision == TurnOptions.Stand) break;
            }
            else break;
        }
        // ---------- dealer’s turn / outcome ----------
        BjWinCalculation(dealerCards, playerCards, gameDeck);
    }

    private static void EndGameIfPlayerBusted(Hand dealerCards, Hand playerCards) {
        Console.WriteLine("BUST!");
        DisplayEndOfGameMessages(dealerCards, playerCards);
    }

    private static void StandIfPlayerHasBlackjack(bool playerHasBlackjack) {
        if (playerHasBlackjack)
        {
            Console.WriteLine("21! You stand automatically.");
            turnDecision = TurnOptions.Stand;
        }
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
        int dealerHandValue = GetHandValue(dealerCards);
        
        while (dealerHandValue < 17)
        {
            DrawDealerCard(dealerCards, gameDeck);
            dealerHandValue = GetHandValue(dealerCards);
            bool dealerHasBlackjackOrBusted = dealerHandValue >= 21;
            if (dealerHasBlackjackOrBusted)
            {
                if (Pushed(dealerCards, playerCards))
                {
                    DisplayEndOfGameMessages(dealerCards, playerCards);
                    return;
                }
            }
        }
        
        if (Pushed(dealerCards, playerCards))
        {
            DisplayEndOfGameMessages(dealerCards, playerCards);
            return;
        }
        
        int playerHandValue = GetHandValue(playerCards);
        bool dealerHasNotBusted = dealerHandValue <= 21;
        bool dealerHasHigherScoreThanPlayer = dealerHandValue > playerHandValue;
        if (dealerHasNotBusted && dealerHasHigherScoreThanPlayer)
            DisplayLossMessage();
        else
            DisplayWinMessage();
        DisplayEndOfGameMessages(dealerCards, playerCards);
    }

    private static void DisplayWinMessage() {
        Console.WriteLine("You win!");
    }

    private static void DisplayLossMessage() {
        Console.WriteLine("You lose!");
    }

    internal static int GetHandValue(Hand hand)
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

    internal static bool Pushed(Hand dealerHand, Hand playerHand)
    {
        if (GetHandValue(dealerHand) == 21 && GetHandValue(playerHand) == 21)
        {
            Console.WriteLine("Push!");
            return true;
        }
        return false;
    }
    static string DisplayEndOfGameMessages(Hand dealerCards, Hand playerCards)
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