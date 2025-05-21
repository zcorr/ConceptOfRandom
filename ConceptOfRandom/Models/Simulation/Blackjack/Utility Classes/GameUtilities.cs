namespace ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

public abstract class GameUtilities
{
    public static bool IsPlayAgain(Func<ConsoleKey> getKey, Action<string> displayMessage)
    {
        while (true)
        {
            displayMessage("Would you like to play again? (Y/N)");
            var key = getKey();

            if (key == ConsoleKey.Y)
            {
                return true;
            }
            else if (key == ConsoleKey.N)
            {
                displayMessage("Thank you for playing!");
                Environment.Exit(0);
            }
            else
            {
                displayMessage("Invalid input. Please enter 'Y' or 'N'.");
            }
        }
    }
}