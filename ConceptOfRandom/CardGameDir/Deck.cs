namespace ConceptOfRandom;

public class Deck
{

    static List<Card> _theDeck = new List<Card>();

    public Deck()
    {
        BuildDeck();
    }

    public int GetLength()
    {
        return _theDeck.Count;
    }
    
    public static void BuildDeck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                _theDeck.Add(new Card(rank,suit));
    }

    public void ShuffleDeck()
    {
        var random = new Random();
        _theDeck = _theDeck.OrderBy(_ => random.Next()).ToList();
    }
    
    public Card DrawCard()
    {
        if (GetLength() > 0)
        {
            int last = _theDeck.Count - 1;
            Card card = _theDeck[last];
            _theDeck.RemoveAt(last);
            return card;
        }
        else throw new InvalidOperationException("Deck is empty");
    }

    public List<Card> CopyOfDeck()
    {
        return [.._theDeck];
    }
    public override string ToString()
    {
        return string.Join(", ", _theDeck);
    }
}