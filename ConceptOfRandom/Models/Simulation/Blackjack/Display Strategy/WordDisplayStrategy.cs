using ConceptOfRandom.Models.Simulation.Blackjack.Objects;

namespace ConceptOfRandom.Models.Simulation.Blackjack.Display_Strategy;
public class WordDisplayStrategy : ICardDisplayStrategy
{
    public string GetDisplay(Card card)
    {
        return card.IsFaceUp ? $"{card.Rank} of {card.Suit}" : "????";
    }
}
