using ConceptOfRandom.Models.API;
using Xunit.Abstractions;

namespace ConceptOfRandomTests;

public class RandomWeatherTests(ITestOutputHelper output) {
    private readonly RandomWeather randomWeather = new();

    [Fact]
    public void CanConstructNewWeatherObject() {
        RandomWeather newWeather = new RandomWeather();
        Assert.NotNull(newWeather);
    }

    [Fact]
    public void RandomWeatherIsNamed() {
        bool hasName = randomWeather.Name.Length > 0;
        Assert.True(hasName);
    }

    [Fact]
    public async void CanGetRandomWeather() {
        try {
            string weather = await randomWeather.Get();
            Assert.NotNull(weather);
            output.WriteLine(weather);
        }
        catch (Exception e) {
            Assert.Fail($"Failed with exception {e.Message}");
        }
    }

    [Fact]
    public void CanGetRandomWeatherLocationFromFile() {
        output.WriteLine(LocationReader.GetRandomLocationFromFile());
        Assert.NotNull(LocationReader.GetRandomLocationFromFile());
    }
}