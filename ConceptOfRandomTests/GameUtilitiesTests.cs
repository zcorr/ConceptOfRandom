using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

namespace ConceptOfRandomTests;

public class GameUtilitiesTest
{
    [Fact]
    public void IsPlayAgain_HandlesInvalidInputThenYes()
    {
        // Arrange
        Queue<ConsoleKey> inputs = new Queue<ConsoleKey>(new[] { ConsoleKey.M, ConsoleKey.Y });
        List<string> outputs = new List<string>();

        bool result = GameUtilities.IsPlayAgain(
            () => inputs.Dequeue(),
            message => outputs.Add(message)
        );

        // Assert
        Assert.True(result);
        Assert.Contains("Would you like to play again? (Y/N)", outputs);
    }

    [Fact]
    public void IsPlayAgain_HandlesNoImmediately()
    {
        // Arrange
        Queue<ConsoleKey> inputs = new Queue<ConsoleKey>(new[] { ConsoleKey.N });
        List<string> outputs = new List<string>();

        bool result = GameUtilities.IsPlayAgain(
            () => inputs.Dequeue(),
            message => outputs.Add(message)
        );

        // Assert
        Assert.False(result);
        Assert.Contains("Would you like to play again? (Y/N)", outputs);
    }
}