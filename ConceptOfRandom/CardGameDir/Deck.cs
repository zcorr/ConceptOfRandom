namespace ConceptOfRandom.CardGameDir;

public class Deck : Hand
{
    private static readonly Random _rng = new Random();

    public Deck() : base() => BuildDeck();

    public Deck(IEnumerable<Card> cards) : base(cards)
    {
    }
    void BuildDeck()
    {
        Cards.Clear();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            Cards.Add(new Card(rank,suit));
    }


    public void Shuffle()
    {
        var shuffled = Cards.OrderBy(_ => _rng.Next()).ToList();
        Cards.Clear();
        Cards.AddRange(shuffled);
    }

    public Card DrawCard()
    {
        if (Count == 0)
            throw new InvalidOperationException("Deck is empty");

        int lastIndex = Count - 1;
        var card = Cards[lastIndex];
        Cards.RemoveAt(lastIndex);

        return card;
    }

    public List<Card> ToList() => [..Cards];
}