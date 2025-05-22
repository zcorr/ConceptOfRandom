using System;
using ConceptOfRandom.view;

namespace ConceptOfRandom.View.Games
{
    public class FullRandomIntegration
    {
        public void ExecuteRandomGame()
        {
            // List of game types
            var games = new Action[]
            {
                () => new BlackjackIntegration().StartGame(),
                () => new DiceIntegration().StartDice(),
                () => new RandomFactIntegration().StartFacts(),
                () => new WaveIntegration().Wave()
            };

            // Randomly select a game
            var random = new Random();
            int selectedIndex = random.Next(games.Length);

            // Execute the selected game
            games[selectedIndex].Invoke();
        }
    }
}