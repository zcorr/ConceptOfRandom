namespace ConceptOfRandom.CardGameDir;
public class WordDisplayStrategy : ICardDisplayStrategy
{
    public string GetDisplay(Card card)
    {
        return card.IsFaceUp ? $"{card.Rank} of {card.Suit}" : "????";
    }
}
