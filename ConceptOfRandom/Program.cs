namespace ConceptOfRandom;
using ConceptOfRandom.view;
using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

class Program
{
    static void Main(string[] args) {
        Console.CursorVisible = true;
        var program = new MenuOutline();
        program.AutoResize = true;
        program.CreateBorder();

        while (true)
        {
            try
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    program.HandleInput(key);
                }
                program.Tick();
            }
            catch (ReturnToMainMenuException)
            {
                // Return to the main menu by continuing the loop
                Console.Clear();
                program.Clear();
                program.CreateBorder();
                program.Render();
                continue;
            }
        }
    }
}