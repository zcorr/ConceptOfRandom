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

    [Fact]
    public void AssertToStringAfterRoll()
    {
        Dice dice = new Dice(3);
        dice.lastRoll = 4;

        string targetString = $"d3 rolled a {dice.lastRoll}.";
        
        Assert.Equal(dice.ToString(), targetString);
    }

    [Fact]
    public void AssertToStringNoRoll()
    {
        Dice dice = new Dice(6);

        string targetString = $"d6 hasn't been rolled yet.";
        
        Assert.Equal(dice.ToString(), targetString);
    }

    [Fact]
    public void AssertRollsInRange()
    {
        Dice dice = new Dice(10);

        for (int i = 0; i < 25; i++)
        {
            Assert.True(dice.Roll() <= 10);
        }
    }
}