namespace ConceptOfRandom.Models.Simulation.DiceGames;

public class Wallet : IMoneyManagement
{
    private Random rand = new Random();
    public int balance { get; private set; }

    public Wallet()
    {
        balance = rand.Next(25, 31) * 50;
    }

    public int Pay(int amount)
    {
        if (amount > balance)
        {
            throw new InvalidOperationException("Not enough money");
        }
        balance -= amount;
        return amount;
    }

    public void Receive(int amount)
    {
        balance += amount;
    }

    public string ShowBalance()
    {
        return $"Wallet balance: {balance}";
    }
}