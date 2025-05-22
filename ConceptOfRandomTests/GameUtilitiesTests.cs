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