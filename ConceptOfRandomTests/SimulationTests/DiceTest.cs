using ConceptOfRandom.Models.Simulation;

namespace ConceptOfRandomTests.SimulationTests;

public class DiceTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(-1)]
    public void AssertThrowsArgumentExceptionWhenLessThan3SidesTest(int sides)
    {
        Assert.Throws<ArgumentException>(() => new Dice(sides));
    }

    [Fact]
    public void AssertProperCreationOfRange()
    {
        Dice dice = new Dice(3);

        Assert.Equal(3, dice.sides);
    }
}