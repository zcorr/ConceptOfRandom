using ConceptOfRandom.Models.Simulation.Blackjack.Enums;

namespace ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

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