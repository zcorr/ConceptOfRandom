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
    private bool usingConsoleRenderer = false;
    public Hand PlayerCards { get; private set; } = new Hand();
    public Hand DealerCards { get; private set; } = new Hand();
    private static readonly Deck GameDeck;
    public TurnOptions TurnDecision = TurnOptions.Undefined;

    public BlackjackGame(IConsoleCanvas canvas)
    {
        
        _canvas = canvas;
        usingConsoleRenderer = true;
        StartBlackjackGameConsoleRenderer(DealerCards, PlayerCards);
    }

    public BlackjackGame() {
        usingConsoleRenderer = false;
    }

    static BlackjackGame()
    {
        GameDeck = new Deck();
    }
    
    public void StartBlackjackGameConsoleRenderer(Hand dealerCards, Hand playerCards) {
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
    
    public void StartBlackjackGame(Hand dealerCards, Hand playerCards)
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
    
    private void DisplayWelcomeMessage()
    {
        if (usingConsoleRenderer) {
            _canvas.Clear();
            _canvas.CreateBorder();
            _canvas.Text((_canvas.Width - 21) / 2, 2, "Welcome to Blackjack!");
            _canvas.Render();
        }
        else {
            Console.WriteLine("Welcome to Blackjack!\n");
        }
    }

    private void DisplayPlayAgainPrompt()
    {
        if (usingConsoleRenderer) {
            _canvas.Clear();
            _canvas.CreateBorder();
            _canvas.Text((_canvas.Width - 34) / 2, 4, "Would you like to play again? (Y/N)");
            _canvas.Render();
        }
        else {
            Console.WriteLine("Would you like to play again? (Y/N)");
        }
    }

    private void AskUserForDisplayStyle()
    {
        if (usingConsoleRenderer) {
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
        else {
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
    }

    public void PlayBlackjack(Hand dealerCards, Hand playerCards, Deck gameDeck)
    {
        while (true)
        {
            if(usingConsoleRenderer) _canvas.Clear();
            TurnDecision = TurnOptions.Undefined;
            PrintHands(dealerCards, playerCards);
            var playerHandValue = GetHandValue(playerCards);
            bool playerHasBlackjack = (playerHandValue == 21);
            if (playerHasBlackjack)  StandIfPlayerHasBlackjack(playerHasBlackjack);
            if (TurnDecision == TurnOptions.Stand) break;

            if (usingConsoleRenderer) {
                _canvas.Text((_canvas.Width / 2 - 25), 9, "Type 1 to Hit, 0 to Stand");
                _canvas.Render();

                var choice = Console.ReadKey(intercept: true).Key;
                if (choice == ConsoleKey.D1) {
                    TurnDecision = TurnOptions.Hit;
                    DrawCardForPlayer(playerCards, gameDeck);

                    playerHandValue = GetHandValue(playerCards);
                    if (playerHandValue > 21) {
                        EndGameIfPlayerBusted(dealerCards, playerCards);
                        return;
                    }
                }
                else if (choice == ConsoleKey.D0) {
                    TurnDecision = TurnOptions.Stand;
                    break;
                }
            }
            
            else {
                Console.WriteLine("Type 1 to Hit, 0 to Stand");
                var choice = Console.ReadLine();
                if(choice == "1") TurnDecision = TurnOptions.Hit;
                else if(TurnDecision == TurnOptions.Undefined) TurnDecision = TurnOptions.Stand;
                if (TurnDecision == TurnOptions.Hit) {
                    DrawCardForPlayer(playerCards, gameDeck);
                    playerHandValue = GetHandValue(playerCards);
                    bool playerHasBusted = (playerHandValue > 21);
                    if (playerHasBusted) {
                        EndGameIfPlayerBusted(dealerCards, playerCards);
                        return;
                    }

                    StandIfPlayerHasBlackjack(playerHasBlackjack);
                    if (TurnDecision == TurnOptions.Stand) break;
                }
                else break;
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

    public string PrintHands(Hand dealerCards, Hand playerCards)
    {
        if (usingConsoleRenderer) {
            _canvas.Clear();
            _canvas.Render();
            _canvas.CreateBorder();
            _canvas.Text((_canvas.Width / 2 - 15), 2, "Dealer's cards:");
            _canvas.Text((_canvas.Width - dealerCards.ToString().Length) / 2, 3, dealerCards.ToString());
            _canvas.Text((_canvas.Width - 11) / 2, 5, "Your cards:");
            _canvas.Text((_canvas.Width - playerCards.ToString().Length) / 2, 6, playerCards.ToString());
            _canvas.Render();
            return "Used Canvas Renderer";
        }
        else {
            var dealerHandText = PrintSingleHand("Dealer's cards:", dealerCards);
            var playerHandText = PrintSingleHand("Your cards:",    playerCards);
            return dealerHandText + playerHandText; 
        }
    }

    public string PrintSingleHand(string heading, Hand hand) {
        var handText = hand.ToString();
        Console.WriteLine(heading);
        Console.WriteLine(handText);
        return handText;
    }

    private void EndGameIfPlayerBusted(Hand dealerCards, Hand playerCards)
    {
        if (usingConsoleRenderer) {
        _canvas.Clear();
        _canvas.CreateBorder();
        _canvas.Text((_canvas.Width - 5) / 2, 8, "BUST!");
        }
        else {
            Console.WriteLine("BUST!");
        }
            
        DisplayEndOfGameMessages(dealerCards, playerCards, "You busted!");
    }

    private void DisplayEndOfGameMessages(Hand dealerCards, Hand playerCards, string result)
    {
        if (usingConsoleRenderer) {
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
        else {
            dealerCards.RevealAll();
            var output = result;
            output += "Final hands:\n";
            output += "Dealer's cards were:\n";
            output += dealerCards+"\n";
            output += "Your cards were:\n";
            output += playerCards+"\n";
            Console.WriteLine(output);
        }
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
            if(usingConsoleRenderer) 
                _canvas.Text((_canvas.Width - 34) / 2, 8, "BLACKJACK! Automatically standing.");
            else Console.WriteLine("21! You stand automatically.");
            TurnDecision = TurnOptions.Stand;
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