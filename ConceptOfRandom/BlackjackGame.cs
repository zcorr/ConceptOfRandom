namespace ConceptOfRandom;
public class BlackjackGame
{ 
    static List<Card> playerCards = new List<Card>();
    static List<Card> dealerCards = new();
    static Deck gameDeck = new Deck();
    public void GameStart()
    {
        System.Console.WriteLine("Welcome to Blackjack!");
        BuildBlackjackStartingHands();
    }
    static void BuildBlackjackStartingHands()
    {
        gameDeck.ShuffleDeck();
        
        playerCards.Add(gameDeck.DrawCard());
        playerCards.Add(gameDeck.DrawCard());
        
        dealerCards.Add(gameDeck.DrawCard());
        Card dealerDownCard = gameDeck.DrawCard();
        dealerDownCard.IsFaceUp = false;
        dealerCards.Add(dealerDownCard);
        
        RunGame();
    }
    static void RunGame()
    {
        while (true)
        {
            Console.WriteLine("Type 1 to Hit, Type 0 to Stand");
            Console.WriteLine("Your cards are:");
            HandManipulationFunctions.printHand(playerCards);
        
            Console.WriteLine("Dealers cards are:");
            HandManipulationFunctions.printHand(dealerCards);
            
            string hitOrStand = Console.ReadLine();
        }
    }
    
    bool WinCalculation(List<Card> playerHand,List<Card> dealerHand)
    {
        return true;
    }
    
}