using System.Diagnostics.CodeAnalysis;
using ConceptOfRandom;
using ConceptOfRandom.CardGameDir;

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
    public void CardToString_SymbolDisplayStategy()
    {
        Card card = new Card(Rank.Ace, Suit.Spades);
        Assert.Equal("A♠",card.ToString());
    }
    [Fact]
    public void CardToString_UsesCurrentDisplayStrategy()
    {
        var card = new Card(Rank.Ace, Suit.Spades);
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new DummyStrategy();
        try
        {
            var text = card.ToString();
            Assert.Equal("DUMMY:Ace-Spades", text);
        }
        finally
        {
            CardDisplay.Current = originalStrategy;
        }
    }
    
    // Fake Display Strat to make sure strat is working (recommended by bot)
    class DummyStrategy : ICardDisplayStrategy
    {
        public string GetDisplay(Card c)
        {
            // include rank & suit so we know it actually got the Card
            return $"DUMMY:{c.Rank}-{c.Suit}";
        }
    }
}
