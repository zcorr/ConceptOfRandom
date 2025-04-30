using System.Runtime.CompilerServices;

namespace ConceptOfRandom.Models.Simulation;

public class Dice
{
    private Random rand = new Random();
    public int sides { get; }

    public Dice(int sides)
    {   
        if (sides < 3)
            throw new ArgumentException("Dice must have at least 3 sides.");
        
        this.sides = sides;
    }

    public int Roll()
    {
        return this.rand.Next(1, this.sides + 1);
    }
}