using ConceptOfRandom.Models.Simulation.DiceGames;

namespace ConceptOfRandomTests.SimulationTests;

public class BotTest
{
    [Fact]
    public void AssertBotNameOnCreation()
    {
        string name = "Bot 1";
        
        Bot bot = new Bot(name);
        
        Assert.Equal(name, bot.name);
    }

    [Fact]
    public void AssertBetPlacement()
    {
        Bot bot = new Bot("Bot 1");
        Pot pot = new Pot();

        int betted = bot.PlaceBet(pot, 10);
        
        Assert.Equal(betted, pot.balance);
    }

    [Fact]
    public void AssertReceiveWinnings()
    {
        Bot bot = new Bot("Bot 1");
        
        int startingBalance = bot.GetPlayerBalance();
        
        bot.ReceiveWinnings(100);
        
        Assert.Equal(startingBalance + 100, bot.GetPlayerBalance());
    }

    [Fact]
    public void AssertShowBotBalance()
    {
        Bot bot = new Bot("Bot 1");
        
        int startingBalance = bot.GetPlayerBalance();
        
        Assert.Equal($"Bot 1's balance: ${startingBalance}", bot.ShowBalance());
    }

    [Fact]
    public void AssertRandomBetGeneration()
    {
        Bot bot = new Bot("Bot 1");

        int randomBet = bot.GenerateRandomBet();
        
        Assert.True(randomBet > 187 && randomBet < 1350);
    }
}