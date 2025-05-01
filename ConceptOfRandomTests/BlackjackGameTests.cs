using ConceptOfRandom;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;


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
        List<Card> hand = [];
        foreach (var rank in ranks)
            hand.Add(new Card(rank,Suit.Spades));
        Assert.Equal(expected,BlackjackGame.BjHandValue(hand));
    }
    
    [Theory]
    [InlineData(new[] { Rank.Ace,Rank.King },new[] { Rank.Ace,Rank.King },true)]
    [InlineData(new[] { Rank.Ace,Rank.Two },new[] { Rank.Ace,Rank.King },false)]
    public void BjPushCheck_BlackjackVsBlackjack_ReturnsTrue(Rank[] dealerRanks,Rank[] playerRanks, bool expected)
    {
        var dealer = new List<Card>();
        var player = new List<Card>();
        foreach (var rank in dealerRanks)
            dealer.Add(new Card(rank,Suit.Spades));
        foreach (var rank in playerRanks)
            player.Add(new Card(rank,Suit.Spades));
        Assert.Equal(BlackjackGame.BjPushCheck(dealer, player),expected);
    }
    
}
