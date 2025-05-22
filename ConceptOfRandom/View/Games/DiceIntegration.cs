using ConceptOfRandom.Models.Simulation;
using ConceptOfRandom.View;

namespace ConceptOfRandom.view;

public class DiceIntegration : MenuOutline
{
    public DiceIntegration()
    {
        var canvas = this;
    }
    public void StartDice()
    {
        // Clearing canvas to set up visuals for dice game
        Clear();
        CreateBorder();

        // User input asking for how many sided die they want to roll
        Text((Width - 61) / 2, 2, "How many sides does the die have? (e.g. 6 for a standard die)");
        Render();
        
        
        if (!int.TryParse(Console.ReadLine(), out int sides) || sides <= 0)
        {
            Text((Width - 46) / 2, 4, "Invalid input. Please enter a positive number.");
            Render();
            Console.ReadKey(true);
            return;
        }
        
        
        // Calling game -
        if (sides > 2)
        {
            var game = new Dice(sides);
            int result = game.Roll();
            Text((Width - (result.ToString().Length + sides.ToString().Length + 30)) / 2, 4, $"You rolled a {result} on a {sides}-sided die.");
            Text((Width - 28) / 2, 6, "Press any key to continue...");
            Render();
        }
        else
        {
            var game = new Coin();
            int result = game.Roll();
            Text((Width - 32) / 2, 4, $"You flipped a coin and got {(result == 1 ? "Heads" : "Tails")}.");
            Text((Width - 28) / 2, 6, "Press any key to continue...");
            Render();
        }
        
        Console.ReadKey(true);
        Clear();
        CreateBorder();
        RenderMenu();
    }
}