namespace ConceptOfRandom;

public struct Card(Rank rank,Suit suit)
{
    public Rank Rank { get; } = rank;
    public Suit Suit { get; } = suit;
    
    public bool IsFaceUp {get; set;} = true;
    
    public int Value => (int)Rank; 

    public override string ToString()
    {
        return IsFaceUp ? $"{Rank} of {Suit}" : "????";
    }
}