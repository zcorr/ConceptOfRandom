using ConceptOfRandom.view;

namespace ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

using ConceptOfRandom.Models.Simulation.Blackjack;

public abstract class GameUtilities
{
    public static bool IsPlayAgain(Func<ConsoleKey> getKey, IConsoleCanvas canvas)
    {
        while (true)
        {
            canvas.Clear();
            
            canvas.Text(2, 2, "Would you like to play again? (Y/N)");
            canvas.Render();
            var key = getKey();

            if (key == ConsoleKey.Y)
            {
                return true;
            }
            else if (key == ConsoleKey.N) {
                canvas.Clear();
                canvas.Render();
                throw new ReturnToMainMenuException();
            }
            else
            {
                canvas.Text(2, 2, "Invalid input. Please enter 'Y' or 'N'.");
            }
        }
    }
    
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