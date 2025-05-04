namespace ConceptOfRandom.CardGameDir;

public static class CardDisplay
{
    public static ICardDisplayStrategy Current { get; set; } = new WordDisplayStrategy();
}
