using System.Diagnostics.CodeAnalysis;
using ConceptOfRandom;
using ConceptOfRandom.Models.Simulation.Blackjack.Enums;
using ConceptOfRandom.Models.Simulation.Blackjack.Objects;

namespace ConceptOfRandomTests;

public class CardsSuitsDeckTests
{
    [Fact]
    public void BuildDeck_LengthIsRanksXSuits()
    {
        Deck deck = new Deck(); // Deck() calls BuildDeck()
        int numRanks = Enum.GetValues(typeof(Rank)).Length;
        int numSuits = Enum.GetValues(typeof(Suit)).Length;
        Assert.Equal(deck.Count,numRanks * numSuits);
    }
    
    [Fact]
    public void DrawCard_DeckRemovesAndReturnsASingleCard()
    {
        var deck = new Deck();
        var initialCount = deck.Count;
        
        Card drawnCard = deck.DrawCard();
        
        Assert.Equal(initialCount - 1, deck.Count);
        Assert.DoesNotContain(drawnCard, deck.ToList());
    }
    
    [Fact]
    public void DrawCard_DeckEmpty()
    {
        Deck deck = new Deck();
        while (deck.Count > 0)
            deck.DrawCard();
        Assert.Throws<InvalidOperationException>(() => deck.DrawCard());
    }
    
    [Fact]
    public void Shuffle_PreservesNumCards()
    {
        var deck = new Deck();
        deck.Shuffle();
        Assert.Equal(Enum.GetValues(typeof(Rank)).Length*Enum.GetValues(typeof(Suit)).Length, deck.Count);
    }

    [Fact]
    public void Shuffle_ChangesOrder_MostOfTheTime()
    {
        // Arrange
        var deck = new Deck();
        var original = deck.ToList();

        // Act
        deck.Shuffle();
        var shuffled = deck.ToList();

        // Assert: very unlikely to be the same exact sequence
        Assert.NotEqual(original, shuffled);
    }

    [Fact]
    public void Remove_RemovesCardsFromDeck()
    {
        var deck = new Deck();
        var cardToRemove = deck.DrawCard();
        var initialCount = deck.Count;

        deck.Remove(cardToRemove);

        Assert.DoesNotContain(cardToRemove, deck.ToList());
    }
    
    [Fact]
    public void Clear_RemovesAllCards()
    {
        var deck = new Deck();
        while (deck.Count > 0)
            deck.DrawCard();
        Assert.Equal(0, deck.Count);
    }
    
    [Fact] void HandClear_RemovesAllCards()
    {
        var hand = new Hand();
        var initialCount = hand.Count;
        hand.Clear();
        Assert.Equal(0, hand.Count);
    }
    
    [Fact]
    public void Print_ReturnsDeckString_NewlineSeparated()
    {
        // Arrange
        var deck = new Deck();
        var expected = string.Join(
            Environment.NewLine,
            deck.ToList().Select(c => c.ToString())
        );

        // Act
        var printed = deck.Print();

        // Assert
        Assert.Equal(expected, printed);
    }


    
    
    
}
