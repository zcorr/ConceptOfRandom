namespace ConceptOfRandom.CardGameDir;

public abstract class HandManipulationFunctions
{
    
    public static void printHand(List<Card> hand)
    {
        Console.WriteLine(string.Join(", ", hand));
    }
    
}