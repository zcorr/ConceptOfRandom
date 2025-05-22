using ConceptOfRandom.Models.Simulation.Blackjack;
using ConceptOfRandom.View;

namespace ConceptOfRandom.view;

public class BlackjackIntegration : MenuOutline
{
    private IConsoleCanvas _canvas;
    public BlackjackIntegration()
    {
        _canvas = this;
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