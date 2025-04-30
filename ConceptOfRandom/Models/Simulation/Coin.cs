namespace ConceptOfRandom.Models.Simulation;

public class Coin : Dice
{
    public Coin() : base(2) { }

    public string Flip()
    {
        return Roll() == 1 ? "Heads" : "Tails";
    }
}