using ConceptOfRandom.Models.Simulation.Blackjack.Objects;

namespace ConceptOfRandom.Models.Simulation.Blackjack.Display_Strategy;

public interface ICardDisplayStrategy
{
    string GetDisplay(Card card);
}