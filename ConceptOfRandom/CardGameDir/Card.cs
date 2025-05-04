namespace ConceptOfRandom.CardGameDir;

public struct Card(Rank rank,Suit suit)
{
    internal Rank Rank { get; } = rank;
    internal Suit Suit { get; } = suit;
    
    public bool IsFaceUp {get; set;} = true;
    
    public int Value => (int)Rank; 

    public override string ToString()
    {
        return CardDisplay.Current.GetDisplay(this);
    }
}