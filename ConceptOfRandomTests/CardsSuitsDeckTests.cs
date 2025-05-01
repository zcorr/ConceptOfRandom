using System.Diagnostics.CodeAnalysis;
using ConceptOfRandom;
namespace ConceptOfRandomTests;

public class CardsSuitsDeckTests
{
    [Fact]
    public void BuildDeck_LengthIsRanksXSuits()
    {
        Deck deck = new Deck(); // Deck() calls BuildDeck()
        int numRanks = Enum.GetValues(typeof(Rank)).Length;
        int numSuits = Enum.GetValues(typeof(Suit)).Length;
        Assert.Equal(deck.GetLength(),numRanks * numSuits);
    }
    
    [Fact]
    public void DrawCard_DeckRemovesAndReturnsASingleCard()
    {
        var deck = new Deck();
        var initialCount = deck.GetLength();
        Card card = deck.DrawCard();
        
        Assert.Equal(initialCount - 1, deck.GetLength());
        Assert.DoesNotContain(card, deck.CopyOfDeck());
    }
    
    [Fact]
    public void DrawCard_DeckEmpty()
    {
        Deck deck = new Deck();
        while (deck.GetLength() > 0)
            deck.DrawCard();
        Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
    }
    
}