using ConceptOfRandom.Models.Simulation.DiceGames;

namespace ConceptOfRandomTests.SimulationTests;

public class PotTest
{
    [Fact]
    public void AssertProperPotCreation()
    {
        Pot pot = new Pot();

        int expectedBalance = 0;
        
        Assert.Equal(expectedBalance, pot.balance);
    }

    [Fact]
    public void AssertReceiveFunctionality()
    {
        Pot pot = new Pot();

        int toReceive = 1000;
        
        pot.Receive(toReceive);
        
        Assert.Equal(toReceive, pot.balance);
    }

    [Fact]
    public void AssertPayFunctionality()
    {
        Pot pot = new Pot();
        
        pot.Receive(1500);
        pot.Pay(500);
            
        int expectedBalance = 1000;
        
        Assert.Equal(expectedBalance, pot.balance);
    }

    [Fact]
    public void AssertShowBalanceFunctionality()
    {
        Pot pot = new Pot();
        
        Assert.Equal("Pot amount: $0", pot.ShowBalance());
    }
}