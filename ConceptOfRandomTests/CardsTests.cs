using System.Diagnostics.CodeAnalysis;
using ConceptOfRandom;
using ConceptOfRandom.CardGameDir;
namespace ConceptOfRandomTests;

public class CardsTests
{
    [Fact]
    public void CardToString_SymbolDisplayStrategy()
    {
        var card = new Card(Rank.Ace, Suit.Spades);
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();
        try
        {
            var text = card.ToString();
            Assert.Equal("A♠", text);
        }
        finally
        {
            CardDisplay.Current = originalStrategy;
        }
    }
    [Fact]
    public void CardToString_WordlDisplayStrategy()
    {
        var card = new Card(Rank.Ace, Suit.Spades);
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new WordDisplayStrategy();
        try
        {
            var text = card.ToString();
            Assert.Equal("Ace of Spades", text);
        }
        finally
        {
            CardDisplay.Current = originalStrategy;
        }
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