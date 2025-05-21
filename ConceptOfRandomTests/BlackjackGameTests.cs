using System.ComponentModel;
using System.Reflection;
using ConceptOfRandom.Models.Simulation.Blackjack;
using ConceptOfRandom.Models.Simulation.Blackjack.Display_Strategy;
using ConceptOfRandom.Models.Simulation.Blackjack.Enums;
using ConceptOfRandom.Models.Simulation.Blackjack.Objects;

namespace ConceptOfRandomTests;

public class BlackjackGameTests
{
    [Theory]
    [InlineData(new[] { Rank.Ace, Rank.King }, 21)]
    [InlineData(new[] { Rank.Ace, Rank.Six }, 17)]
    [InlineData(new[] { Rank.Ace, Rank.Nine }, 20)]
    [InlineData(new[] { Rank.Ace, Rank.Three, Rank.Five }, 19)]
    [InlineData(new[] { Rank.Ace, Rank.Nine, Rank.Five }, 15)]
    [InlineData(new[] { Rank.Ace, Rank.King, Rank.Nine }, 20)]
    [InlineData(new[] { Rank.Ace, Rank.Ace }, 12)]
    [InlineData(new[] { Rank.Ace, Rank.Ace, Rank.Nine }, 21)]
    [InlineData(new[] { Rank.Ace, Rank.Ace, Rank.Nine, Rank.Nine }, 20)]
    [InlineData(new[] { Rank.Ace, Rank.Ace, Rank.Ace, Rank.Nine }, 12)]
    [InlineData(new[] { Rank.Ace, Rank.Ace, Rank.Ace, Rank.Ace, Rank.Nine }, 13)]
    [InlineData(new[] { Rank.Jack, Rank.Queen }, 20)]
    [InlineData(new[] { Rank.Ten, Rank.Six, Rank.Five }, 21)]
    [InlineData(new[] { Rank.King, Rank.Queen, Rank.Two }, 22)]
    [InlineData(new[] { Rank.Ace, Rank.King, Rank.Queen, Rank.Nine }, 30)]
    public void BjHandValue_ReturnsExpectedTotal(Rank[] ranks, int expected)
    {
        var hand = new Hand();
        foreach (var rank in ranks)
            hand.Add(new Card(rank, Suit.Spades));
        Assert.Equal(expected, BlackjackGame.GetHandValue(hand));
    }

    [Theory]
    [InlineData(new[] { Rank.Ace, Rank.King }, new[] { Rank.Ace, Rank.King }, true)]
    [InlineData(new[] { Rank.Ace, Rank.Two }, new[] { Rank.Ace, Rank.King }, false)]
    public void BjPushCheck_BlackjackVsBlackjack_ReturnsTrue(Rank[] dealerRanks, Rank[] playerRanks, bool expected)
    {
        var dealerCards = MakeSpadeHandsOutOfRankList(dealerRanks);
        var playerCards = MakeSpadeHandsOutOfRankList(playerRanks);
        Assert.Equal(expected, BlackjackGame.Pushed(dealerCards, playerCards));
    }

    [Fact]
    public void DealerBusts_PlayerWins()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();

        var dealer = new Hand
        {
            new Card(Rank.Ten, Suit.Spades),
            new Card(Rank.Six, Suit.Hearts)
        };

        var player = new Hand
        {
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Queen, Suit.Clubs)
        };

        var deck = new Deck
        {
            new Card(Rank.Ten, Suit.Clubs)
        };

        var game = new BlackjackGame(new ConsoleRenderer.ConsoleCanvas());
        var output = CaptureConsoleOut(() =>
            game.BjWinCalculation(dealer, player, deck));

        Assert.Contains("You win!", output);
        CardDisplay.Current = originalStrategy;
    }

    [Fact]
    public void BjWinCalculation_DealerBeatsPlayer()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();

        var dealer = new Hand
        {
            new Card(Rank.Ten, Suit.Spades),
            new Card(Rank.Six, Suit.Hearts)
        };

        var player = new Hand
        {
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Seven, Suit.Clubs)
        };

        var deck = new Deck
        {
            new Card(Rank.Two, Suit.Clubs)
        };
        var game = new BlackjackGame(new ConsoleRenderer.ConsoleCanvas());
        var output = CaptureConsoleOut(() =>
            game.BjWinCalculation(dealer, player, deck));

        Assert.Contains("You lose!", output);
        CardDisplay.Current = originalStrategy;
    }

    [Fact]
    public void BjWinCalculation_IsPushBJvsBJ()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();

        var dealerCards = new Hand
        {
            new Card(Rank.Ace, Suit.Spades),
            new Card(Rank.King, Suit.Hearts)
        };

        var playerCards = new Hand
        {
            new Card(Rank.Ace, Suit.Clubs),
            new Card(Rank.Queen, Suit.Diamonds)
        };

        var deck = new Deck();

        var game = new BlackjackGame(new ConsoleRenderer.ConsoleCanvas());
        var output = CaptureConsoleOut(() =>
            game.BjWinCalculation(dealerCards, playerCards, deck));
        

        Assert.DoesNotContain("win", output, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("lose", output, StringComparison.OrdinalIgnoreCase);
        CardDisplay.Current = originalStrategy;
    }

    [Fact]
    public void BjWinCalculation_PlayerBeatsDealer_PrintsYouWin()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();

        var dealer = new Hand
        {
            new Card(Rank.King, Suit.Hearts),
            new Card(Rank.Seven, Suit.Clubs)
        };

        var player = new Hand
        {
            new Card(Rank.Ten, Suit.Spades),
            new Card(Rank.King, Suit.Diamonds)
        };

        var deck = new Deck();

        var game = new BlackjackGame(new ConsoleRenderer.ConsoleCanvas());
        var output = CaptureConsoleOut(() =>
            game.BjWinCalculation(dealer, player, deck));
        

        Assert.Contains("You win!", output);
        CardDisplay.Current = originalStrategy;
    }

    [Fact]
    public void BjWinCalculation_MidLoopPush_ReturnsWithoutWinOrLose()
    {
        var originalStrategy = CardDisplay.Current;
        CardDisplay.Current = new SymbolDisplayStrategy();

        var dealer = new Hand
        {
            new Card(Rank.Ten, Suit.Spades),
            new Card(Rank.Five, Suit.Hearts)
        };

        var player = new Hand
        {
            new Card(Rank.Ace, Suit.Clubs),
            new Card(Rank.Ten, Suit.Diamonds)
        };

        var deck = new Deck
        {
            new Card(Rank.Six, Suit.Clubs)
        };

        var game = new BlackjackGame(new ConsoleRenderer.ConsoleCanvas());
        var output = CaptureConsoleOut(() =>
            game.BjWinCalculation(dealer, player, deck));

        Assert.DoesNotContain("win", output, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("lose", output, StringComparison.OrdinalIgnoreCase);
        CardDisplay.Current = originalStrategy;
    }

    [Fact]
    public void BjGameRunner_PlayerTotal21_AutomaticallyStands_NoHitPrompt()
    {
        var dealer = new Hand
        {
            new Card(Rank.King, Suit.Hearts),
            new Card(Rank.Seven, Suit.Spades)
        };

        var player = new Hand
        {
            new Card(Rank.Queen, Suit.Clubs),
            new Card(Rank.Two, Suit.Clubs),
            new Card(Rank.Ace, Suit.Hearts),
            new Card(Rank.Eight, Suit.Spades)
        };

        var deck = new Deck();

        using var input = new StringReader(string.Empty);
        using var output = new StringWriter();
        var originalIn = Console.In;
        var originalOut = Console.Out;
        Console.SetIn(input);
        Console.SetOut(output);

        try
        {
            var game = new BlackjackGame(new ConsoleRenderer.ConsoleCanvas());
            var playBlackjackMethod = typeof(BlackjackGame).GetMethod("PlayBlackjack", BindingFlags.NonPublic | BindingFlags.Instance);
            playBlackjackMethod?.Invoke(game, new object[] { dealer, player, deck });

            var text = output.ToString();

            Assert.Contains("21! You stand automatically.", text);
            Assert.DoesNotContain("Type 1 to Hit", text);
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }

    static string CaptureConsoleOut(Action action)
    {
        var original = Console.Out;
        var sw = new StringWriter();
        Console.SetOut(sw);

        try { action(); }
        finally { Console.SetOut(original); }

        return sw.ToString();
    }

    static Hand MakeSpadeHandsOutOfRankList(Rank[] rankList)
    {
        var hand = new Hand();
        foreach (var rank in rankList)
            hand.Add(new Card(rank, Suit.Spades));
        return hand;
    }
}