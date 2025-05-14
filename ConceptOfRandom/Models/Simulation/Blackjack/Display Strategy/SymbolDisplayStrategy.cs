namespace ConceptOfRandom.CardGameDir;

public class SymbolDisplayStrategy : ICardDisplayStrategy
{
    public string GetDisplay(Card card)
    {
        return card.IsFaceUp ? $"{card.Rank.ToSymbol()}{card.Suit.ToSymbol()}" : "????";
    }
}
