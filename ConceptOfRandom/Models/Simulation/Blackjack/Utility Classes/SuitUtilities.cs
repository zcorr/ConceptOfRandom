namespace ConceptOfRandom.CardGameDir;

public static class SuitExtensions
{
    public static string ToSymbol(this Suit suit)
    {
        return suit switch
        {
            Suit.Spades   => "♠",
            Suit.Hearts   => "♥",
            Suit.Diamonds => "♦",
            Suit.Clubs    => "♣",
            _             => suit.ToString()
        };
    }
}