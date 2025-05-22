using ConceptOfRandom.Models.Simulation.Blackjack.Utility_Classes;
using ConceptOfRandom.view;

namespace ConceptOfRandomTests;

public class GameUtilitiesTest
{
	private class TestConsoleCanvas : IConsoleCanvas
	{
		public List<string> Messages { get; } = new List<string>();

		public void Render() { }
		public void CreateBorder() { }
		public bool AutoResize { get; set; }
		public void Clear() { }
		public void Text(int x, int y, string text) => Messages.Add(text);
		public int Width => 80;
	}

	[Fact]
	public void IsPlayAgain_HandlesInvalidInputThenYes()
	{
		// Arrange
		Queue<ConsoleKey> inputs = new Queue<ConsoleKey>(new[] { ConsoleKey.M, ConsoleKey.Y });
		var canvas = new TestConsoleCanvas();

		bool result = GameUtilities.IsPlayAgain(
			() => inputs.Dequeue(),
			canvas
		);

		// Assert
		Assert.True(result);
		Assert.Contains("Would you like to play again? (Y/N)", canvas.Messages);
	}

	[Fact]
	public void IsPlayAgain_HandlesNoImmediately()
	{
		// Arrange
		Queue<ConsoleKey> inputs = new Queue<ConsoleKey>(new[] { ConsoleKey.N });
		var canvas = new TestConsoleCanvas();

		bool result = GameUtilities.IsPlayAgain(
			() => inputs.Dequeue(),
			canvas
		);

		// Assert
		Assert.False(result);
		Assert.Contains("Would you like to play again? (Y/N)", canvas.Messages);
	}
}