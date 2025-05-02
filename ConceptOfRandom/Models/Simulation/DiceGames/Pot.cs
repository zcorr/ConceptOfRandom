namespace ConceptOfRandom.Models.Simulation.DiceGames;

public class Pot : IMoneyManagement
{
    public int balance { get; private set; }

    public Pot()
    {
        balance = 0;
    }

    public int Pay(int amount)
    {
        balance -= amount;
        return amount;
    }

    public void Receive(int amount)
    {
        balance += amount;
    }

    public string ShowBalance()
    {
        return $"Pot amount: ${balance}";
    }
}