namespace ConceptOfRandom.Models.Simulation.DiceGames;

public interface IMoneyManagement
{
    int Pay(int amount);
    void Receive(int amount);
    public string ShowBalance();
}