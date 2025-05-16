using ConceptOfRandom.Models.Simulation.Blackjack.Enums;
using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

namespace ConceptOfRandomTests;

public class SuitsRanksTests
{
    [Fact]
    public void ToSymbol_WithUndefinedRank_ReturnsEnumToString()
    {
        // Arrange: pick a value outside the defined Rank enum members
        var undefinedRank = (Rank)999;
        var expected = undefinedRank.ToString();

        // Act
        var symbol = undefinedRank.ToSymbol();

        // Assert
        Assert.Equal(expected, symbol);
    }
    [Theory]
    [InlineData(Suit.Spades,   "♠")]
    [InlineData(Suit.Hearts,   "♥")]
    [InlineData(Suit.Diamonds, "♦")]
    [InlineData(Suit.Clubs,    "♣")]
    public void ToSymbol_KnownSuits_ReturnsCorrectSymbol(Suit suit, string expected)
    {
        // Act
        var symbol = suit.ToSymbol();

        // Assert
        Assert.Equal(expected, symbol);
    }

    [Fact]
    public void ToSymbol_UndefinedSuit_ReturnsEnumName()
    {
        // Arrange
        var undefined = (Suit)999;
        var expected  = undefined.ToString();

        // Act
        var symbol = undefined.ToSymbol();

        // Assert
        Assert.Equal(expected, symbol);
    }

}