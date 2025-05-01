namespace ConceptOfRandom;

public class HandManipulationFunctions
{
    public static void RevealCards(List<Card> hand)
    {
        for (int i = 0; i < hand.Count; i++)
        {
            var card = hand[i];
            card.IsFaceUp = true;
            hand[i] = card;
        }
    }
    public static void printHand(List<Card> hand)
    {
        Console.WriteLine(string.Join(", ", hand));
    }
    
}