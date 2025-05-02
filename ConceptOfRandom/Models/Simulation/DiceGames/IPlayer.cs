namespace ConceptOfRandom.Models.Simulation.DiceGames;

public interface IPlayer
{
    int PlaceBet(Pot pot, int amount);
    string ShowBalance();
    void ReceiveWinnings(int amount);
    int GetPlayerBalance();
}