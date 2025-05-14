namespace ConceptOfRandom.Models.Simulation.DiceGames;

public class Bot : IPlayer
{
    private Random rand = new Random();
    private Wallet wallet = new Wallet();

    public string name { get; }

    public Bot(string name)
    {
        this.name = name;
    }

    public int PlaceBet(Pot pot, int amount)
    {
        pot.Receive(wallet.Pay(amount));

        return amount;
    }

    public string ShowBalance()
    {
        return $"{name}'s balance: ${wallet.balance}";
    }
    
    public void ReceiveWinnings(int amount)
    {
        wallet.Receive(amount);
    }

    public int GetPlayerBalance()
    {
        return wallet.balance;
    }

    public int GenerateRandomBet()
    {
        int bet = (int)Math.Floor(wallet.balance * (rand.NextDouble() * (0.9 - 0.25)  + 0.25));

        return bet;
    }
}