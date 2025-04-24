namespace ConceptOfRandom;

public class Deck
{

    public static List<Card> theDeck = new List<Card>();

    public Deck()
    {
        BuildDeck();
    }
    
    public static void BuildDeck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                theDeck.Add(new Card(rank,suit));
    }

    public void ShuffleDeck()
    {
        var random = new Random();
        theDeck = theDeck.OrderBy(_ => random.Next()).ToList();
    }
    
    public Card DrawCard()
    {
        int last = theDeck.Count - 1;
        Card card = theDeck[last];
        theDeck.RemoveAt(last);
        return card;
    }

    public override string ToString()
    {
        return string.Join(", ", theDeck);
    }

}