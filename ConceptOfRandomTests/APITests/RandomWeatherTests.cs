using System.Globalization;
using System.Text.Json;
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

    [Fact]
    public async void CanGetRandomWeatherDetails() {
        try {
            string response = await APIClient.Instance.Fetch("https://goweather.xyz/weather/moraga", "");
            RandomWeather.WeatherForecast forecast =
                JsonSerializer.Deserialize<RandomWeather.WeatherForecast>(response)!;
            Assert.NotNull(forecast.Temperature);
            Assert.NotNull(forecast.Wind);
            Assert.NotNull(forecast.Description);
            Assert.NotNull(forecast.Forecast);
            RandomWeather.ForecastDay day = forecast.Forecast[0];
            Assert.NotNull(day.Temperature);
            Assert.NotNull(day.Wind);
            Assert.NotNull(day.Day);
        }
        catch (Exception e) {
            Assert.Fail($"Failed with exception {e.Message}");
        }
    }
}