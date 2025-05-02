using ConceptOfRandom.CardGameDir;
using ConceptOfRandom.BlackjackGameDir;
namespace ConceptOfRandomTests;

public class BlackjackGameTests
{
    // Card Calculation tests
    // 
    [Theory]
    [InlineData(new[] { Rank.Ace,Rank.King },21)]
    [InlineData(new[] { Rank.Ace,Rank.Six },17)]
    [InlineData(new[] { Rank.Ace,Rank.Nine },20)]
    [InlineData(new[] { Rank.Ace,Rank.Three,Rank.Five },19)]
    [InlineData(new[] { Rank.Ace,Rank.Nine,Rank.Five },15)]
    [InlineData(new[] { Rank.Ace,Rank.King,Rank.Nine },20)]
    [InlineData(new[] { Rank.Ace,Rank.Ace },12)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Nine },21)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Nine,Rank.Nine },20)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Ace,Rank.Nine },12)]
    [InlineData(new[] { Rank.Ace,Rank.Ace,Rank.Ace,Rank.Ace,Rank.Nine },13)]
    [InlineData(new[] { Rank.Jack,Rank.Queen },20)]
    [InlineData(new[] { Rank.Ten,Rank.Six,Rank.Five },21)]
    [InlineData(new[] { Rank.King,Rank.Queen,Rank.Two },22)]
    [InlineData(new[] { Rank.Ace,Rank.King,Rank.Queen,Rank.Nine },30)]
    public void BjHandValue_ReturnsExpectedTotal(Rank[] ranks,int expected)
    {
        Hand hand = [];
        foreach (var rank in ranks)
            hand.Add(new Card(rank,Suit.Spades));
        Assert.Equal(expected,BlackjackGame.BjHandValue(hand));
    }
    
    [Theory]
    [InlineData(new[] { Rank.Ace,Rank.King },new[] { Rank.Ace,Rank.King },true)]
    [InlineData(new[] { Rank.Ace,Rank.Two },new[] { Rank.Ace,Rank.King },false)]
    public void BjPushCheck_BlackjackVsBlackjack_ReturnsTrue(Rank[] dealerRanks,Rank[] playerRanks, bool expected)
    {
        var dealerCards = MakeSpadeHandsOutOfRankList(dealerRanks);
        var playerCards = MakeSpadeHandsOutOfRankList(playerRanks);
        Assert.Equal(BlackjackGame.BjPushCheck(dealerCards, playerCards),expected);
    }
    
    [Fact]
    public void DealerBusts_PlayerWins()
    {
        // dealer starts with 16 (10♠ + 6♥)
        var dealer = new Deck
        {
            new Card(Rank.Ten,  Suit.Spades),
            new Card(Rank.Six,  Suit.Hearts)
        };
    
        // player holds a hard 20
        var player = new Deck
        {
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Queen,Suit.Clubs)
        };

        // custom mini-deck: dealer will draw 10♣ → bust to 26
        var deck = new Deck([new Card(Rank.Ten,Suit.Clubs)]);

        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealer, player, deck));

        Assert.Contains("You win!", output);
    }

    //Dealer 18, player 17
    [Fact]
    public void BjWinCalculation_DealerBeatsPlayer()
    {
        // dealer 16 10+6 -->+ 2 to reach 18
        var dealer = new Hand
        {
            new Card(Rank.Ten,  Suit.Spades),
            new Card(Rank.Six,  Suit.Hearts)
        };

        // player 17 K+7
        var player = new Hand
        {
            new Card(Rank.King, Suit.Diamonds),
            new Card(Rank.Seven,Suit.Clubs)
        };

        var deck = new Deck([new Card(Rank.Two,Suit.Clubs)]);

        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealer, player, deck));

        Assert.Contains("You lose!", output);
    }
    
    [Fact]
    public void BjWinCalculation_IsPushBJvsBJ()
    {
        Hand dealerCards = [new(Rank.Ace, Suit.Spades), new(Rank.King, Suit.Hearts)];
        
        Hand playerCards = [new(Rank.Ace,Suit.Clubs),new(Rank.Queen,Suit.Diamonds)];

        // no cards will be drawn
        var deck = new Deck(new List<Card>());

        var output = CaptureConsoleOut(() =>
            BlackjackGame.BjWinCalculation(dealerCards, playerCards, deck));

        Assert.DoesNotContain("win",  output, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("lose", output, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData(new[] { Rank.Ace,Rank.King },new[] { Rank.Ace,Rank.King })]
    public void PrintHands_DisplaysHandsCorrectly(Rank[] dealerRanks, Rank[] playerRanks)
    {
        var dealerCards = MakeSpadeHandsOutOfRankList(dealerRanks);
        var playerCards = MakeSpadeHandsOutOfRankList(playerRanks);
    }
    
    
    //Helper Function for getting the console out, used smartbotman helped me 
    static string CaptureConsoleOut(Action action)
    {
        var original = Console.Out;
        var sw       = new StringWriter();
        Console.SetOut(sw);

        try     { action(); }
        finally { Console.SetOut(original); }

        return sw.ToString();
    }

    static Hand MakeSpadeHandsOutOfRankList(Rank[] rankList)
    {
        var hand = new Hand();
        foreach (var rank in rankList)
            hand.Add(new Card(rank,Suit.Spades));
        return hand;
    }
}

