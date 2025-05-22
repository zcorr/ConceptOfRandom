using System.Runtime.CompilerServices;
using ConceptOfRandom.Models.Simulation.Blackjack.Display_Strategy;
using ConceptOfRandom.Models.Simulation.Blackjack.Enums;
using ConceptOfRandom.Models.Simulation.Blackjack.Objects;
using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;
using ConceptOfRandom.view;
using ConceptOfRandom.View;
using ConsoleRenderer;

[assembly: InternalsVisibleTo("ConceptOfRandomTests")]

namespace ConceptOfRandom.Models.Simulation.Blackjack;

public class BlackjackGame
{
    private readonly IConsoleCanvas _canvas;
    private static readonly Hand PlayerCards = new Hand();
    private static readonly Hand DealerCards = new Hand();
    private static readonly Deck GameDeck;
    private static TurnOptions turnDecision = TurnOptions.Undefined;

    public BlackjackGame(IConsoleCanvas canvas)
    {
        
        _canvas = canvas;
        StartBlackjackGame(DealerCards, PlayerCards);
    }

    static BlackjackGame()
    {
        GameDeck = new Deck();
    }

    public void StartBlackjackGame(Hand dealerCards, Hand playerCards) {
        try {
            do {
                DisplayWelcomeMessage();
                AskUserForDisplayStyle();
                var gameDeck = SetUpNewGameDeck(dealerCards, playerCards);
                DrawPlayerCards(playerCards, gameDeck);
                DrawDealerCard(dealerCards, gameDeck);
                DrawDealerCardFaceDown(dealerCards, gameDeck);
                PlayBlackjack(dealerCards, playerCards, gameDeck);
                DisplayPlayAgainPrompt();
            } while (GameUtilities.IsPlayAgain(() => Console.ReadKey(intercept: true).Key, _canvas));
        } catch (ReturnToMainMenuException) {
            // Clear canvas and exit gracefully
            _canvas.Clear();
            _canvas.CreateBorder();
            _canvas.Render();
            throw; // Re-throw to propagate to the main menu
        }
    }
    private void DisplayWelcomeMessage()
    {
        _canvas.Clear();
        _canvas.CreateBorder();
        _canvas.Text((_canvas.Width - 21) / 2, 2, "Welcome to Blackjack!");
        _canvas.Render();
    }

    private void DisplayPlayAgainPrompt()
    {
        _canvas.Clear();
        _canvas.CreateBorder();
        _canvas.Text((_canvas.Width - 34) / 2, 4, "Would you like to play again? (Y/N)");
        _canvas.Render();
    }

    private void AskUserForDisplayStyle()
    {
        _canvas.Clear();
        _canvas.CreateBorder();
        _canvas.Text((_canvas.Width - 26) / 2, 2, "Choose your display style:");
        _canvas.Text((_canvas.Width - 21) / 2, 3, "  1) Word Display art");
        _canvas.Text((_canvas.Width - 23) / 2, 4, "  2) Symbol Display art");
        _canvas.Render();

        // Use Console.ReadKey with intercept: true to prevent input from showing
        var key = Console.ReadKey(intercept: true).Key;
        CardDisplay.Current = key == ConsoleKey.D1 ? new WordDisplayStrategy() : new SymbolDisplayStrategy();
    }

    private void PlayBlackjack(Hand dealerCards, Hand playerCards, Deck gameDeck)
    {
        while (true)
        {
            _canvas.Clear();
            turnDecision = TurnOptions.Undefined;
            PrintHands(dealerCards, playerCards);

            var playerHandValue = GetHandValue(playerCards);
            if (playerHandValue == 21)
            {
                StandIfPlayerHasBlackjack(true);
                break;
            }

            _canvas.Text((_canvas.Width / 2 - 25), 9, "Type 1 to Hit, 0 to Stand");
            _canvas.Render();

            var choice = Console.ReadKey(intercept: true).Key;
            if (choice == ConsoleKey.D1)
            {
                turnDecision = TurnOptions.Hit;
                DrawCardForPlayer(playerCards, gameDeck);

                playerHandValue = GetHandValue(playerCards);
                if (playerHandValue > 21)
                {
                    EndGameIfPlayerBusted(dealerCards, playerCards);
                    return;
                }
            }
            else if (choice == ConsoleKey.D0)
            {
                turnDecision = TurnOptions.Stand;
                break;
            }
        }

        BjWinCalculation(dealerCards, playerCards, gameDeck);
    }

    internal void BjWinCalculation(Hand dealerCards, Hand playerCards, Deck gameDeck)
    {
        var dealerHandValue = GetHandValue(dealerCards);

        while (dealerHandValue < 17)
        {
            DrawDealerCard(dealerCards, gameDeck);
            dealerHandValue = GetHandValue(dealerCards);
        }

        var playerHandValue = GetHandValue(playerCards);
        string resultMessage;

        if (dealerHandValue > 21 || playerHandValue > dealerHandValue)
        {
            resultMessage = "You Win!";
        }
        else if (dealerHandValue == playerHandValue)
        {
            resultMessage = "It's a Push!";
        }
        else
        {
            resultMessage = "You Lose!";
        }

        DisplayEndOfGameMessages(dealerCards, playerCards, resultMessage);
    }

    private void PrintHands(Hand dealerCards, Hand playerCards)
    {
        _canvas.Clear();
        _canvas.Render();
        _canvas.CreateBorder();
        _canvas.Text((_canvas.Width / 2 - 15), 2, "Dealer's cards:");
        _canvas.Text((_canvas.Width - dealerCards.ToString().Length) / 2 , 3, dealerCards.ToString());
        _canvas.Text((_canvas.Width - 11) / 2, 5, "Your cards:");
        _canvas.Text((_canvas.Width - playerCards.ToString().Length) / 2, 6, playerCards.ToString());
        _canvas.Render();
    }

    private void EndGameIfPlayerBusted(Hand dealerCards, Hand playerCards)
    {
        _canvas.Clear();
        _canvas.CreateBorder();
        _canvas.Text((_canvas.Width - 5) / 2, 8, "BUST!");
        DisplayEndOfGameMessages(dealerCards, playerCards, "You busted!");
    }

    private void DisplayEndOfGameMessages(Hand dealerCards, Hand playerCards, string result)
    {
        _canvas.Clear();
        _canvas.CreateBorder();
        dealerCards.RevealAll();
        _canvas.Text((_canvas.Width - 12) / 2, 5, "Final hands:");
        _canvas.Text((_canvas.Width - 20) / 2, 6, "Dealer's cards were:");
        _canvas.Text((_canvas.Width - dealerCards.ToString().Length) / 2, 7, dealerCards.ToString());
        _canvas.Text((_canvas.Width - 16) / 2, 8, "Your cards were:");
        _canvas.Text((_canvas.Width - playerCards.ToString().Length) / 2, 9, playerCards.ToString());
        _canvas.Text((_canvas.Width - result.Length) / 2, 10, "");
        _canvas.Text((_canvas.Width - result.Length) / 2, 11, result);
        _canvas.Text((_canvas.Width - 34) / 2, 12, "Press any key to continue...");
        _canvas.Render();

        Console.ReadKey(intercept: true);
    }
    
    private static Deck SetUpNewGameDeck(Hand dealerCards, Hand playerCards) {
        var gameDeck = new Deck();
        gameDeck.Shuffle();
        playerCards.Clear();
        dealerCards.Clear();
        return gameDeck;
    }
    
    private static void DrawPlayerCards(Hand playerCards, Deck gameDeck) {
        DrawCardForPlayer(playerCards, gameDeck);
        DrawCardForPlayer(playerCards, gameDeck);
    }
    
    private static void DrawCardForPlayer(Hand playerCards, Deck gameDeck) {
        playerCards.Add(gameDeck.DrawCard());
    }
    
    private static void DrawDealerCard(Hand dealerCards, Deck gameDeck) {
        dealerCards.Add(gameDeck.DrawCard());
    }
    
    private static void DrawDealerCardFaceDown(Hand dealerCards, Deck gameDeck) {
        var faceDownDealerCard = gameDeck.DrawCard();
        faceDownDealerCard.IsFaceUp = false;
        dealerCards.Add(faceDownDealerCard);
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
    
    private void StandIfPlayerHasBlackjack(bool playerHasBlackjack) {
        if (playerHasBlackjack)
        {
            _canvas.Text((_canvas.Width - 34) / 2, 8, "BLACKJACK! Automatically standing.");
            turnDecision = TurnOptions.Stand;
        }
    }
    
   
    
    internal static bool Pushed(Hand dealerHand, Hand playerHand)
    {
        if (GetHandValue(dealerHand) == 21 && GetHandValue(playerHand) == 21)
        {
            return true;
        }
        return false;
    }
    


    
}