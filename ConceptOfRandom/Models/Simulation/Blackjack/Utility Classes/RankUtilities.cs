using ConceptOfRandom.Models.Simulation.Blackjack.Enums;

namespace ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

public static class RankExtensions
{
    public static string ToSymbol(this Rank rank)
    {
        return rank switch
        {
            Rank.Two     => "2",
            Rank.Three   => "3",
            Rank.Four    => "4",
            Rank.Five    => "5",
            Rank.Six     => "6",
            Rank.Seven   => "7",
            Rank.Eight   => "8",
            Rank.Nine    => "9",
            Rank.Ten     => "10",
            Rank.Jack    => "J",
            Rank.Queen   => "Q",
            Rank.King    => "K",
            Rank.Ace     => "A",
            _             => rank.ToString()
        };
    }
}