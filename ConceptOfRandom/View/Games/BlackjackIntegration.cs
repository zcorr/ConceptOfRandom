using ConsoleRenderer;
using ConceptOfRandom.Models.Simulation.Blackjack;

namespace ConceptOfRandom.view;

public class BlackjackIntegration : MenuOutline
{
    private ConsoleCanvas _canvas;
    public BlackjackIntegration()
    {
        _canvas = GetCanvas();
    }

    public void StartGame()
    {
        // Clearing canvas to set up visuals for blackjack game
        _canvas.Clear();
        _canvas.CreateBorder();
     
        // Calling game - modified entire blackjack game to use console renderer (yay refactoring)
        var game = new BlackjackGame(_canvas);
        
    }

    private void ExitGame()
    {
        
    }
    
}