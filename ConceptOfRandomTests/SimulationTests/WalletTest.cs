using ConceptOfRandom.Models.Simulation.DiceGames;

namespace ConceptOfRandomTests.SimulationTests;

public class WalletTest
{
    [Fact]
    public void AssertBalanceCreationRange()
    {
        for (int i = 0; i < 50; i++)
        {
            Wallet wallet = new Wallet();

            Assert.True(wallet.balance <= 1500 && wallet.balance >= 750);
        }
    }

    [Fact]
    public void AssertThrowsInvalidOperationExceptionWhenPayGreaterThanBalance()
    {
        Wallet wallet = new Wallet();

        int toPay = wallet.balance + 100;
        
        Assert.Throws<InvalidOperationException>(() => wallet.Pay(toPay));
    }

    [Fact]
    public void AssertProperPaymentFunctionality()
    {
        Wallet wallet = new Wallet();

        int toPay = 100;
        
        int startingBalance = wallet.balance;

        wallet.Pay(toPay);
        
        Assert.Equal(startingBalance - toPay, wallet.balance);
    }

    [Fact]
    public void AssertProperReceiveFunctionality()
    {
        Wallet wallet = new Wallet();

        int startingBalance = wallet.balance;

        int toReceive = 200;
        
        wallet.Receive(toReceive);
        
        Assert.Equal(startingBalance + toReceive, wallet.balance);
    }

    [Fact]
    public void AssertProperShowBalanceFunctionality()
    {
        Wallet wallet = new Wallet();
        
        int startingBalance = wallet.balance;
        
        Assert.Equal($"Wallet balance: {startingBalance}", wallet.ShowBalance());
    }

}