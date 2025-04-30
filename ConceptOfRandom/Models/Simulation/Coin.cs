namespace ConceptOfRandom.Models.Simulation;

public class Coin : IRandomGenerator
{
    private Random rand = new Random();
    
    public Coin() { }
    private int lastRoll;

    public int Roll()
    {
        lastRoll = rand.Next(1, 3);
        return lastRoll;
    }

    public override string ToString()
    {
        if (lastRoll == 1)
        {
            return "Heads.";
        }
        else if (lastRoll == 2)
        {
            return "Tails.";
        }
        
        return "Coin hasn't been flipped yet.";
    }
}