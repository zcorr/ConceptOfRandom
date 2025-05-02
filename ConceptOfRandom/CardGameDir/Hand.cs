using System.Collections;

namespace ConceptOfRandom.CardGameDir;

public class Hand : IEnumerable
{
    
    public Hand()
    {
        Cards = new List<Card>();
    }
    protected readonly List<Card> Cards;
    
    public Hand(IEnumerable<Card> cards) => Cards = [..cards]; 
    
    public int Count => Cards.Count;
    
    public void Add(Card card) => Cards.Add(card);
    
    public bool Remove(Card card) => Cards.Remove(card);

    public void Clear() => Cards.Clear();
    
    public override string ToString()
    {
        return string.Join(", ", Cards);
    }
    
    public void RevealAll()
    {
        for (int i = 0; i < Cards.Count; i++)
        {
            var card = Cards[i];
            card.IsFaceUp = true;
            Cards[i] = card;
        }
    }

    public void Print()
    {
        Console.WriteLine(this);
    }

    public IEnumerator GetEnumerator() => Cards.GetEnumerator();
}

