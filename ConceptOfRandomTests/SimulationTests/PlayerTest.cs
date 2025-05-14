using ConceptOfRandom.Models.Simulation.DiceGames;

namespace ConceptOfRandomTests.SimulationTests;

public class PlayerTest
{
    [Fact]
    public void AssertPlayerName()
    {
        string name = "Randomer";
        
        Player player = new Player(name);
        
        Assert.Equal(name, player.name);
    }

    [Fact]
    public void AssertReceiveWinnings()
    {
        Player player = new Player("Randomer");

        int startingBalance = player.GetPlayerBalance();
        
        player.ReceiveWinnings(1000);
        
        Assert.Equal(startingBalance + 1000, player.GetPlayerBalance());
    }

    [Fact]
    public void AssertShowPlayerBalance()
    {
        Player player = new Player("Randomer");
        
        int startingBalance = player.GetPlayerBalance();
        
        Assert.Equal($"Randomer's balance: {startingBalance}", player.ShowBalance());
    }

    [Fact]
    public void AssertPlaceBetFunctionality()
    {
        Pot pot = new Pot();
        Player player = new Player("Randomer");
        
        int startingPlayerBalance = player.GetPlayerBalance();

        int betted = player.PlaceBet(pot, 10);
        
        Assert.Equal(10, betted);
        Assert.Equal(10, pot.balance);
        Assert.Equal(startingPlayerBalance - 10, player.GetPlayerBalance());
    }
}