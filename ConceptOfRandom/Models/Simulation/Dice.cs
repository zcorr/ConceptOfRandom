namespace ConceptOfRandom.Models.Simulation;

public class Dice : IRandomGenerator
{
    private Random rand = new Random();
    public int sides { get; }
    public int lastRoll { get; set; }

    public Dice(int sides)
    {   
        if (sides < 3)
            throw new ArgumentException("Dice must have at least 3 sides.");
        
        this.sides = sides;
    }

    public int Roll()
    {
        lastRoll = rand.Next(1, this.sides + 1);
        return lastRoll;
    }

    public override string ToString()
    {
        if (lastRoll == 0)
        {
            return $"d{sides} hasn't been rolled yet.";
        }

        return $"d{sides} rolled a {lastRoll}.";
    }   
}