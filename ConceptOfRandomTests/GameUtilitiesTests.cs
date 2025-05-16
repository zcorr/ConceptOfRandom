using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;

namespace ConceptOfRandomTests;

public class GameUtilitiesTest
{
    [Fact]
    public void IsPlayAgain_HandlesInvalidInputThenYes()
    {
        // Arrange
        Queue<string> inputs = new Queue<string>(new[] { "maybe", "Y" });
        List<string> outputs = new List<string>();

        bool result = GameUtilities.IsPlayAgain(
            () => inputs.Dequeue(),
            message => outputs.Add(message)
        );

        // Assert
        Assert.True(result);
        Assert.Contains("Incorrect Input!", outputs);
        Assert.Contains("Game starting...", outputs);
    }

    [Fact]
    public void IsPlayAgain_HandlesNoImmediately()
    {
        // Arrange
        Queue<string> inputs = new Queue<string>(new[] { "N" });
        List<string> outputs = new List<string>();

        bool result = GameUtilities.IsPlayAgain(
            () => inputs.Dequeue(),
            message => outputs.Add(message)
        );

        // Assert
        Assert.False(result);
        Assert.Contains("Thanks for playing!", outputs);
    }

}


