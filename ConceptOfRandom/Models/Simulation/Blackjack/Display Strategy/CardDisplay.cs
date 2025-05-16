namespace ConceptOfRandom.Models.Simulation.Blackjack.Display_Strategy;

public static class CardDisplay
{
    public static ICardDisplayStrategy Current { get; set; } = new WordDisplayStrategy();
}
