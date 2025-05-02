namespace ConceptOfRandom.CardGameDir;
public  class GameUtilities
{
    public static bool IsPlayAgain()
    {
        Console.WriteLine("Would you like to play again? (Y/N)");
        string? playAgain = Console.ReadLine();

        
        if (playAgain == "Y")
        {
            Console.WriteLine("Game starting...");
            return true;
        }
        else if (playAgain == "N")
        {
            Console.WriteLine("Thanks for playing!");
            return false;
        }
        else
        {
            Console.WriteLine("Incorrect Input!");
            return IsPlayAgain();
        }
    }
}