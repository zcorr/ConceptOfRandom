using System.Reflection;
using ConceptOfRandom.Models.Simulation;

namespace ConceptOfRandomTests.SimulationTests;

public class CoinTest
{
    [Fact]
    public void AssertCoinCreation()
    {
        Coin coin = new Coin();
        
        Assert.NotNull(coin);
    }

    [Fact]
    public void AssertRollRange()
    {
        Coin coin = new Coin();

        for (int i = 0; i < 25; i++)
        {   
            int roll = coin.Roll();
            Assert.True(roll == 1 || roll == 2);
        }
    }

    [Fact]
    public void AssertToStringAfterHeadsRoll()
    {
        Coin coin = new Coin();

        coin.lastRoll = 1;
        
        Assert.Equal("Heads.", coin.ToString());
    }
    
    [Fact]
    public void AssertToStringAfterTailsRoll()
    {
        Coin coin = new Coin();

        coin.lastRoll = 2;
        
        Assert.Equal("Tails.", coin.ToString());
    }
    
    [Fact]
    public void AssertToStringNoRoll()
    {
        Coin coin = new Coin();
        
        Assert.Equal("Coin hasn't been flipped yet.", coin.ToString());
    }
}