using ConceptOfRandom.Models.Simulation.Blackjack.Objects;
using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

namespace ConceptOfRandom.Models.Simulation.Blackjack.Display_Strategy;

public class SymbolDisplayStrategy : ICardDisplayStrategy
{
    public string GetDisplay(Card card)
    {
        return card.IsFaceUp ? $"{card.Rank.ToSymbol()}{card.Suit.ToSymbol()}" : "????";
    }
}
