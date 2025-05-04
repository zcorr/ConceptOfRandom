namespace ConceptOfRandom.CardGameDir;
public abstract class GameUtilities
{
    public static bool IsPlayAgain(Func<string?> readLine, Action<string> writeLine)
    {
        string? input = readLine();
        
        if (input == "Y")
        {
            writeLine("Game starting...");
            return true;
        }
        else if (input == "N")
        {
            writeLine("Thanks for playing!");
            return false;
        }
        else
        {
            writeLine("Incorrect Input!");
            writeLine("Would you like to play again? (Y/N)");
            return IsPlayAgain(readLine, writeLine);
        }
    }
}